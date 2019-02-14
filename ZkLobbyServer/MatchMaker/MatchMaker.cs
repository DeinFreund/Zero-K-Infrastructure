﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using LobbyClient;
using PlasmaShared;
using ZkData;

namespace ZkLobbyServer
{
    public partial class MatchMaker
    {
        private const int TimerSeconds = 20;
        private const int MapModChangePauseSeconds = 30;

        private int BanSecondsIncrease => DynamicConfig.Instance.MmBanSecondsIncrease;
        private int BanSecondsMax => DynamicConfig.Instance.MmBanSecondsMax;
        private int BanReset => DynamicConfig.Instance.MmBanReset;


        private struct QueueConfig
        {
            public string Name, Description;
            public Func<Resource, bool> MapSelector;
            public int MaxPartySize, MaxSize, MinSize;
            public double EloCutOffExponent;
            public AutohostMode Mode;
        }

        public class BanInfo
        {
            public DateTime BannedTime;
            public int BanCounter;
            public int BanSeconds;
        }


        private ConcurrentDictionary<string, BanInfo> bannedPlayers = new ConcurrentDictionary<string, BanInfo>();
        private Dictionary<string, int> ingameCounts = new Dictionary<string, int>();

        private List<ProposedBattle> invitationBattles = new List<ProposedBattle>();
        private ConcurrentDictionary<string, PlayerEntry> players = new ConcurrentDictionary<string, PlayerEntry>();
        public List<MatchMakerSetup.Queue> PossibleQueues { get; private set; } = new List<MatchMakerSetup.Queue>();
        private List<QueueConfig> queueConfigs = new List<QueueConfig>();

        private Dictionary<string, int> queuesCounts = new Dictionary<string, int>();

        private ZkLobbyServer server;


        private object tickLock = new object();
        private Timer timer;
        private int totalQueued;
        private DateTime lastQueueUpdate = DateTime.Now;

        public MatchMaker(ZkLobbyServer server)
        {
            this.server = server;

            Func<Resource, bool> IsTeamsMap = x => (x.MapSupportLevel >= MapSupportLevel.MatchMaker) && (x.MapIsTeams != false) && (x.TypeID == ResourceType.Map) && x.MapIsSpecial != true;
            Func<Resource, bool> IsCoopMap = x => (x.MapSupportLevel >= MapSupportLevel.MatchMaker) && (x.TypeID == ResourceType.Map) && x.MapIsSpecial != true;
            Func<Resource, bool> Is1v1Map = x => (x.MapSupportLevel >= MapSupportLevel.MatchMaker) && (x.TypeID == ResourceType.Map) && x.MapIs1v1 == true && x.MapIsSpecial != true;

            queueConfigs.Add(new QueueConfig()
            {
                Name = "Teams",
                Description = "Play 2v2 to 4v4 with players of similar skill.",
                MinSize = 4,
                MaxSize = 8,
                MaxPartySize = 4,
                EloCutOffExponent = 0.96,
                Mode = AutohostMode.Teams,
                MapSelector = IsTeamsMap,
            });

            queueConfigs.Add(new QueueConfig()
            {
                Name = "Coop",
                Description = "Play together, against AI or chickens",
                MinSize = 2,
                MaxSize = 5,
                MaxPartySize = 5,
                EloCutOffExponent = 0,
                Mode = AutohostMode.GameChickens,
                MapSelector = IsCoopMap,
            });

            queueConfigs.Add(new QueueConfig()
            {
                Name = "1v1",
                Description = "1v1 with opponent of similar skill",
                MinSize = 2,
                MaxSize = 2,
                EloCutOffExponent = 0.97,
                MaxPartySize = 1,
                Mode = AutohostMode.Game1v1,
                MapSelector = Is1v1Map,
            });

            UpdateQueues();

            timer = new Timer(TimerSeconds * 1000);
            timer.AutoReset = true;
            timer.Elapsed += TimerTick;
            timer.Start();

            queuesCounts = CountQueuedPeople(players.Values);
            ingameCounts = CountIngamePeople();
        }

        private void UpdateQueues()
        {
            lastQueueUpdate = DateTime.Now;
            using (var db = new ZkDataContext())
            {
                var oldQueues = PossibleQueues;
                PossibleQueues = queueConfigs.Select(x =>
                {
                    MatchMakerSetup.Queue queue = new MatchMakerSetup.Queue();
                    if (oldQueues.Exists(y => y.Name == x.Name))
                    {
                        queue = oldQueues.Find(y => y.Name == x.Name);
                    }
                    var oldmaps = queue.Maps;
                    queue.Name = x.Name;
                    queue.Description = x.Description;
                    queue.MinSize = x.MinSize;
                    queue.MaxSize = x.MaxSize;
                    queue.MaxPartySize = x.MaxPartySize;
                    queue.EloCutOffExponent = x.EloCutOffExponent;
                    queue.Game = server.Game;
                    queue.Mode = x.Mode;
                    queue.Maps =
                        db.Resources
                            .Where(x.MapSelector)
                            .Select(y => y.InternalName)
                            .ToList();
                    queue.SafeMaps = queue.Maps.Where(y => oldmaps.Contains(y)).ToList();
                    return queue;
                }).ToList();
            }
        }


        public async Task AreYouReadyResponse(ConnectedUser user, AreYouReadyResponse response)
        {
            PlayerEntry entry;
            if (players.TryGetValue(user.Name, out entry))
                if (entry.InvitedToPlay)
                {
                    if (response.Ready)
                    {
                        entry.LastReadyResponse = true;
                    }
                    else
                    {
                        entry.LastReadyResponse = false;
                        await RemoveUser(user.Name, true);
                    }

                    var invitedPeople = players.Values.Where(x => x?.InvitedToPlay == true).ToList();

                    if (invitedPeople.Count <= 1)
                    {
                        foreach (var p in invitedPeople) p.LastReadyResponse = true;
                        // if we are doing tick because too few people, make sure we count remaining people as readied to not ban them 
                        OnTick();
                    }
                    else if (invitedPeople.All(x => x.LastReadyResponse)) OnTick();
                    else
                    {
                        var readyCounts = CountQueuedPeople(invitedPeople.Where(x => x.LastReadyResponse));

                        var proposedBattles = ProposeBattles(invitedPeople.Where(x => x.LastReadyResponse), false);

                        await Task.WhenAll(invitedPeople.Where(x => !x.QuickPlay).Select(async (p) =>
                        {
                            var invitedBattle = invitationBattles?.FirstOrDefault(x => x.Players.Contains(p));
                            await server.SendToUser(p.Name,
                                new AreYouReadyUpdate()
                                {
                                    QueueReadyCounts = readyCounts,
                                    ReadyAccepted = p.LastReadyResponse == true,
                                    LikelyToPlay = proposedBattles.Any(y => y.Players.Contains(p)),
                                    YourBattleSize = invitedBattle?.Size,
                                    YourBattleReady = invitedPeople.Count(x => x.LastReadyResponse && (invitedBattle?.Players.Contains(x) == true))
                                });
                        }));
                    }
                }

        }

        public int GetTotalWaiting() => totalQueued;


        public async Task OnLoginAccepted(ConnectedUser conus)
        {
            await conus.SendCommand(new MatchMakerSetup() { PossibleQueues = PossibleQueues });
            await UpdatePlayerStatus(conus.Name);
        }

        public async Task OnServerGameChanged(string game)
        {
            UpdateQueues();
            await server.Broadcast(new MatchMakerSetup() { PossibleQueues = PossibleQueues });
        }

        public async Task OnServerMapsChanged()
        {
            UpdateQueues();
            await server.Broadcast(new MatchMakerSetup() { PossibleQueues = PossibleQueues });
        }

        public async Task QueueRequest(ConnectedUser user, MatchMakerQueueRequest cmd)
        {
            var banTime = BannedSeconds(user.Name);
            if (banTime != null)
            {
                await UpdatePlayerStatus(user.Name);
                await user.Respond($"Please rest and wait for {banTime}s because you refused previous match");
                return;
            }

            //assure people don't rejoin (possibly accidentally) directly after starting a game
            if (server.Battles.Values.Any(x => x.IsInGame && DateTime.UtcNow.Subtract(x.RunningSince ?? DateTime.UtcNow).TotalMinutes < DynamicConfig.Instance.MmMinimumMinutesBetweenGames && x.spring.LobbyStartContext.Players.Count(p => !p.IsSpectator) > 1 && x.spring.LobbyStartContext.Players.Any(p => !p.IsSpectator && p.Name == user.Name)))
            {
                await UpdatePlayerStatus(user.Name);
                await user.Respond($"You have recently started a match. Please play for at least {DynamicConfig.Instance.MmMinimumMinutesBetweenGames} minutes before starting another match");
                return;
            }


            var wantedQueueNames = cmd.Queues?.ToList() ?? new List<string>();
            var wantedQueues = PossibleQueues.Where(x => wantedQueueNames.Contains(x.Name)).ToList();
            
            await AddOrUpdateUser(user, wantedQueues);
        }

        public async Task MassJoin(List<ConnectedUser> users, List<MatchMakerSetup.Queue> wantedQueues)
        {
            for (int i = 0; i < users.Count; i++) {
                bool alreadyJoined = players.ContainsKey(users[i].Name);
                //join all users without running tick
                await AddOrUpdateUser(users[i], wantedQueues, true); 

                //set width for every user to maximum to speed up MM
                PlayerEntry entry;
                if (players.TryGetValue(users[i].Name, out entry)) entry.SetQuickPlay();
            }

            // if nobody is invited, we can do tick now to speed up things
            if (invitationBattles?.Any() != true) OnTick();
            else await UpdateAllPlayerStatuses(); // else we just send statuses
        }

        /// <summary>
        /// Removes user (and his party) from MM queues, doesnt broadcast changes
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task RemoveUser(string name, bool broadcastChanges)
        {
            var party = server.PartyManager.GetParty(name);
            var anyRemoved = false;

            if (party != null)
            {
                foreach (var n in party.UserNames) if (await RemoveSingleUser(n)) anyRemoved = true;
            }
            else
            {
                anyRemoved = await RemoveSingleUser(name);
            }
            if (broadcastChanges && anyRemoved) await UpdateAllPlayerStatuses();
        }

        public async Task UpdateAllPlayerStatuses()
        {
            ingameCounts = CountIngamePeople();
            queuesCounts = CountQueuedPeople(players.Values);

            await Task.WhenAll(server.ConnectedUsers.Keys.Where(x => x != null).Select(UpdatePlayerStatus));
        }

        private async Task AddOrUpdateUser(ConnectedUser user, List<MatchMakerSetup.Queue> wantedQueues, bool massJoin = false)
        {
            // already invited ignore requests
            PlayerEntry entry;
            if (players.TryGetValue(user.Name, out entry) && entry.InvitedToPlay)
            {
                await UpdatePlayerStatus(user.Name);
                return;
            }


            var party = server.PartyManager.GetParty(user.Name);
            if (party != null) wantedQueues = wantedQueues.Where(x => x.MaxSize / 2 >= party.UserNames.Count).ToList(); // if is in party keep only queues where party fits

            if (wantedQueues.Count == 0) // delete
            {
                await RemoveUser(user.Name, true);
                return;
            }

            if (party != null)
                foreach (var p in party.UserNames)
                {
                    var conUs = server.ConnectedUsers.Get(p);
                    if (conUs != null)
                        players.AddOrUpdate(p,
                            (str) => new PlayerEntry(conUs.User, wantedQueues, party),
                            (str, usr) =>
                            {
                                usr.UpdateTypes(wantedQueues);
                                usr.Party = party;
                                return usr;
                            });
                }
            else
                players.AddOrUpdate(user.Name,
                    (str) => new PlayerEntry(user.User, wantedQueues, null),
                    (str, usr) =>
                    {
                        usr.UpdateTypes(wantedQueues);
                        usr.Party = null;
                        return usr;
                    });


            //if many people are joined simultaneously, wait until join is completed before sending updates or trying to create battles.
            if (massJoin) return;

            // if nobody is invited, we can do tick now to speed up things
            if (invitationBattles?.Any() != true) OnTick();
            else await UpdateAllPlayerStatuses(); // else we just send statuses
        }


        private int? BannedSeconds(string name)
        {
            BanInfo banEntry;
            if (bannedPlayers.TryGetValue(name, out banEntry) && (DateTime.UtcNow.Subtract(banEntry.BannedTime).TotalSeconds < banEntry.BanSeconds)) return (int)(banEntry.BanSeconds - DateTime.UtcNow.Subtract(banEntry.BannedTime).TotalSeconds);

            // remove old
            if (banEntry != null && DateTime.UtcNow.Subtract(banEntry.BannedTime).TotalSeconds > BanReset) bannedPlayers.TryRemove(name, out banEntry);

            return null;
        }

        private Dictionary<string, int> CountIngamePeople()
        {
            var ncounts = PossibleQueues.ToDictionary(x => x.Name, x => 0);
            foreach (var bat in server.Battles.Values.OfType<MatchMakerBattle>().Where(x => (x != null) && x.IsMatchMakerBattle && x.IsInGame))
            {
                var plrs = bat.spring?.Context?.LobbyStartContext?.Players.Count(x => !x.IsSpectator) ?? 0;
                if (plrs > 0)
                {
                    var type = bat.Prototype?.QueueType;
                    if (type != null) ncounts[type.Name] += plrs;
                }
            }
            return ncounts;
        }

        private Dictionary<string, int> CountQueuedPeople(IEnumerable<PlayerEntry> sumPlayers)
        {
            int total = 0;
            var ncounts = PossibleQueues.ToDictionary(x => x.Name, x => 0);
            foreach (var plr in sumPlayers.Where(x => x != null))
            {
                total++;
                foreach (var jq in plr.QueueTypes) ncounts[jq.Name]++;
            }
            totalQueued = total; // ugly to both return and set class property, refactor for nicer
            return ncounts;
        }

        public Dictionary<string, int> GetQueueCounts() => queuesCounts;
        

        private void OnTick()
        {
            lock (tickLock)
            {
                try
                {
                    timer.Stop();
                    var realBattles = ResolveToRealBattles();

                    UpdateAllPlayerStatuses();

                    foreach (var bat in realBattles) StartBattle(bat);

                    ResetAndSendMmInvitations();
                }
                catch (Exception ex)
                {
                    Trace.TraceError("MatchMaker tick error: {0}", ex);
                }
                finally
                {
                    timer.Start();
                }
            }
        }

        private static List<ProposedBattle> ProposeBattles(IEnumerable<PlayerEntry> users, bool ignoreSizeLimit)
        {
            var proposedBattles = new List<ProposedBattle>();

            var usersByWaitTime = users.OrderBy(x => x.JoinedTime).ToList();
            var remainingPlayers = usersByWaitTime.ToList();

            foreach (var user in usersByWaitTime)
                if (remainingPlayers.Contains(user)) // consider only those not yet assigned
                {
                    var battle = TryToMakeBattle(user, remainingPlayers, ignoreSizeLimit);
                    if (battle != null)
                    {
                        proposedBattles.Add(battle);
                        remainingPlayers.RemoveAll(x => battle.Players.Contains(x));
                    }
                }

            return proposedBattles;
        }

        public void BanPlayer(string name)
        {

            var banEntry = bannedPlayers.GetOrAdd(name, (n) => new BanInfo());
            banEntry.BannedTime = DateTime.UtcNow;
            banEntry.BanCounter++;
            banEntry.BanSeconds = Math.Min(BanSecondsMax, BanSecondsIncrease * banEntry.BanCounter);
        }


        private async Task<bool> RemoveSingleUser(string name)
        {
            PlayerEntry entry;
            if (players.TryRemove(name, out entry))
            {
                if (entry.InvitedToPlay)
                {
                    // was invited but he is gone now (whatever reason), ban!
                    BanPlayer(name);
                }


                ConnectedUser conUser;
                if (server.ConnectedUsers.TryGetValue(name, out conUser) && (conUser != null)) if (entry?.InvitedToPlay == true) await conUser.SendCommand(new AreYouReadyResult() { AreYouBanned = true, IsBattleStarting = false, });
                return true;
            }
            return false;
        }


        private void ResetAndSendMmInvitations()
        {
            // generate next battles and send inviatation
            invitationBattles = ProposeBattles(players.Values.Where(x => x != null), false);
            var toInvite = invitationBattles.SelectMany(x => x.Players).ToHashSet();
            foreach (var usr in players.Values.Where(x => x != null))
                if (toInvite.Contains(usr) || usr.QuickPlay) //invite all quickplay players, there will be lots of declines so don't care about making battles yet
                {
                    usr.InvitedToPlay = true;
                    usr.LastReadyResponse = usr.QuickPlay; //quickplay users just said they want to play
                }
                else
                {
                    usr.InvitedToPlay = false;
                    usr.LastReadyResponse = false;
                }

            //send out invites to players in battles
            //don't send out invites to QuickPlayers they just said they want to play
            server.Broadcast(toInvite.Where(p => !p.QuickPlay).Select(p => p.Name),
                new AreYouReady() {
                    SecondsRemaining = TimerSeconds,
                    MinimumWinChance = -1,
                    QuickPlay = false
                });
            
            if (invitationBattles.Count > 0 && toInvite.Count(p => !p.QuickPlay) == 0) //everyone is quickplay, don't wait
            {
                OnTick();
            }
        }

        private List<ProposedBattle> ResolveToRealBattles()
        {
            var lastMatchedUsers = players.Values.Where(x => x?.InvitedToPlay == true).ToList();

            // force leave those not ready
            foreach (var pl in lastMatchedUsers.Where(x => !x.LastReadyResponse)) RemoveUser(pl.Name, false);

            var readyUsers = lastMatchedUsers.Where(x => x.LastReadyResponse).ToList();
            var realBattles = ProposeBattles(readyUsers, true);

            var readyAndStarting = readyUsers.Where(x => realBattles.Any(y => y.Players.Contains(x))).ToList();
            var readyAndFailed = readyUsers.Where(x => !realBattles.Any(y => y.Players.Contains(x))).ToList();

            server.Broadcast(readyAndFailed.Where(x => !x.QuickPlay).Select(x => x.Name), new AreYouReadyResult() { IsBattleStarting = false });

            server.Broadcast(readyAndStarting.Where(x => !x.QuickPlay).Select(x => x.Name), new AreYouReadyResult() { IsBattleStarting = true });

            foreach (var usr in readyAndStarting)
            {
                PlayerEntry entry;
                players.TryRemove(usr.Name, out entry);
            }

            foreach (var usr in readyAndFailed.Where(x => x.QuickPlay)) //quickplay didn't find a game in one tick, resign
            {
                usr.InvitedToPlay = false; //don't ban
                RemoveUser(usr.Name, false); //properly remove in case some party members don't use quickplay
            }

            return realBattles;
        }

        private string PickMap(MatchMakerSetup.Queue queue)
        {
            Random r = new Random();
            List<string> candidates;
            if (DateTime.Now.Subtract(lastQueueUpdate).TotalSeconds > MapModChangePauseSeconds)
            {
                candidates = queue.Maps;
            }
            else
            {
                candidates = queue.SafeMaps;
            }
            return candidates.Count == 0 ? "" : candidates[r.Next(candidates.Count)];
        }

        private async Task StartBattle(ProposedBattle bat)
        {
            var battle = new MatchMakerBattle(server, bat, PickMap(bat.QueueType));
            await server.AddBattle(battle);

            // also join in lobby
            foreach (var usr in bat.Players) await server.ForceJoinBattle(usr.Name, battle);

            if (!await battle.StartGame()) await server.RemoveBattle(battle);
        }


        private void TimerTick(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            OnTick();
        }


        private static ProposedBattle TryToMakeBattle(PlayerEntry player, IList<PlayerEntry> otherPlayers, bool ignoreSizeLimit)
        {
            var allPlayers = new List<PlayerEntry>();
            allPlayers.AddRange(otherPlayers);
            allPlayers.Add(player);

            var playersByElo =
                otherPlayers.Where(x => x != player)
                    .OrderBy(x => Math.Abs(x.LobbyUser.EffectiveMmElo - player.LobbyUser.EffectiveMmElo))
                    .ThenBy(x => x.JoinedTime)
                    .ToList();

            var testedBattles = player.GenerateWantedBattles(allPlayers, ignoreSizeLimit);

            foreach (var other in playersByElo)
                foreach (var bat in testedBattles)
                {
                    if (bat.CanBeAdded(other, allPlayers, ignoreSizeLimit)) bat.AddPlayer(other, allPlayers);
                    if (bat.Players.Count == bat.Size && bat.VerifyBalance(DynamicConfig.Instance.MmTeamsMinimumWinChance)) return bat;
                }
            return null;
        }


        private async Task UpdatePlayerStatus(string name)
        {
            ConnectedUser conus;
            if (server.ConnectedUsers.TryGetValue(name, out conus))
            {
                PlayerEntry entry;
                players.TryGetValue(name, out entry);
                if (entry?.QuickPlay == true) entry = null; //don't confuse quickplay users with MM
                var ret = new MatchMakerStatus()
                {
                    QueueCounts = queuesCounts,
                    IngameCounts = ingameCounts,
                    JoinedQueues = entry?.QueueTypes.Select(x => x.Name).ToList(),
                    CurrentEloWidth = entry?.EloWidth,
                    JoinedTime = entry?.JoinedTime,
                    BannedSeconds = BannedSeconds(name),
                    UserCount = server.ConnectedUsers.Count,
                    UserCountDiscord = server.GetDiscordUserCount()
                };


                // check for instant battle start - only non partied people
                if ((invitationBattles?.Any() != true) && (players.Count > 0) && (server.PartyManager.GetParty(name) == null))
                // nobody invited atm and some in queue
                {
                    ret.InstantStartQueues = new List<string>();
                    // iterate each queue to check all possible instant starts
                    foreach (var queue in PossibleQueues)
                    {
                        // get all currently queued players except for self
                        var testPlayers = players.Values.Where(x => (x != null) && (x.Name != name)).ToList();
                        var testSelf = new PlayerEntry(conus.User, new List<MatchMakerSetup.Queue> { queue }, null);
                        testPlayers.Add(testSelf);
                        var testBattles = ProposeBattles(testPlayers, false);
                        ret.InstantStartQueues.AddRange(testBattles.Where(x => x.Players.Contains(testSelf)).Select(x => x.QueueType.Name).Distinct().ToList());
                    }
                }

                await conus.SendCommand(ret);
            }
        }
    }
}
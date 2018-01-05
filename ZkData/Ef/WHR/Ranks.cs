﻿using System;
using ZkData;

namespace Ratings
{
    public class Ranks
    {
        public static readonly float[] Percentiles = {float.MaxValue, 0.8f, 0.6f, 0.4f, 0.2f, 0.1f, 0.05f, 0.01f};

        
        public static string[] RankBackgroundImages = new string[] { "infrared", "brown", "red", "orange", "yellow", "blue", "neutron", "black" };
        public static string[] RankNames = new string[] { "Nebulous", "Brown Dwarf", "Red Dwarf", "Subgiant", "Giant", "Supergiant", "Neutron Star", "Singularity", "Space Lobster" };

        public static string GetRankBackgroundImagePath(Account acc)
        {
            return string.Format("/img/rankbg/{0}.png", RankBackgroundImages[acc.Rank]);
        }

        public static float GetRankProgress(Account acc)
        {
            var rating = acc.GetBestRating();
            var stdev = Math.Min(10000, rating.Uncertainty);
            var rankCeil = Brackets[acc.Rank + 1] + stdev;
            var rankFloor = Brackets[acc.Rank] - stdev;
            return Math.Min(1, Math.Max(0, (rating.RealElo - rankFloor) / (rankCeil - rankFloor)));
        }

        public static void UpdateRank(Account acc, bool allowUprank, bool allowDownrank, ZkDataContext db)
        {
            var progress = GetRankProgress(acc);
            if (progress > 0.99999 && allowUprank)
            {
                acc.Rank++;
            } 
            if (progress < 0.00001 && allowDownrank)
            {
                acc.Rank--;
            }
        }
    }
    
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MissionEditor2.ContentServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProgramType", Namespace="http://tempuri.org/")]
    public enum ProgramType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MissionEditor = 0,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ContentServiceReference.ContentServiceSoap")]
    public interface ContentServiceSoap {
        
        // CODEGEN: Generating message contract since element name login from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/NotifyMissionRun", ReplyAction="*")]
        MissionEditor2.ContentServiceReference.NotifyMissionRunResponse NotifyMissionRun(MissionEditor2.ContentServiceReference.NotifyMissionRunRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/NotifyMissionRun", ReplyAction="*")]
        System.IAsyncResult BeginNotifyMissionRun(MissionEditor2.ContentServiceReference.NotifyMissionRunRequest request, System.AsyncCallback callback, object asyncState);
        
        MissionEditor2.ContentServiceReference.NotifyMissionRunResponse EndNotifyMissionRun(System.IAsyncResult result);
        
        // CODEGEN: Generating message contract since element name login from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SubmitMissionScore", ReplyAction="*")]
        MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse SubmitMissionScore(MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/SubmitMissionScore", ReplyAction="*")]
        System.IAsyncResult BeginSubmitMissionScore(MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest request, System.AsyncCallback callback, object asyncState);
        
        MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse EndSubmitMissionScore(System.IAsyncResult result);
        
        // CODEGEN: Generating message contract since element name playerName from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SubmitStackTrace", ReplyAction="*")]
        MissionEditor2.ContentServiceReference.SubmitStackTraceResponse SubmitStackTrace(MissionEditor2.ContentServiceReference.SubmitStackTraceRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/SubmitStackTrace", ReplyAction="*")]
        System.IAsyncResult BeginSubmitStackTrace(MissionEditor2.ContentServiceReference.SubmitStackTraceRequest request, System.AsyncCallback callback, object asyncState);
        
        MissionEditor2.ContentServiceReference.SubmitStackTraceResponse EndSubmitStackTrace(System.IAsyncResult result);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class NotifyMissionRunRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="NotifyMissionRun", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.NotifyMissionRunRequestBody Body;
        
        public NotifyMissionRunRequest() {
        }
        
        public NotifyMissionRunRequest(MissionEditor2.ContentServiceReference.NotifyMissionRunRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class NotifyMissionRunRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string missionName;
        
        public NotifyMissionRunRequestBody() {
        }
        
        public NotifyMissionRunRequestBody(string login, string missionName) {
            this.login = login;
            this.missionName = missionName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class NotifyMissionRunResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="NotifyMissionRunResponse", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.NotifyMissionRunResponseBody Body;
        
        public NotifyMissionRunResponse() {
        }
        
        public NotifyMissionRunResponse(MissionEditor2.ContentServiceReference.NotifyMissionRunResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class NotifyMissionRunResponseBody {
        
        public NotifyMissionRunResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SubmitMissionScoreRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SubmitMissionScore", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.SubmitMissionScoreRequestBody Body;
        
        public SubmitMissionScoreRequest() {
        }
        
        public SubmitMissionScoreRequest(MissionEditor2.ContentServiceReference.SubmitMissionScoreRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SubmitMissionScoreRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string passwordHash;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string missionName;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int score;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int gameSeconds;
        
        public SubmitMissionScoreRequestBody() {
        }
        
        public SubmitMissionScoreRequestBody(string login, string passwordHash, string missionName, int score, int gameSeconds) {
            this.login = login;
            this.passwordHash = passwordHash;
            this.missionName = missionName;
            this.score = score;
            this.gameSeconds = gameSeconds;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SubmitMissionScoreResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SubmitMissionScoreResponse", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.SubmitMissionScoreResponseBody Body;
        
        public SubmitMissionScoreResponse() {
        }
        
        public SubmitMissionScoreResponse(MissionEditor2.ContentServiceReference.SubmitMissionScoreResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class SubmitMissionScoreResponseBody {
        
        public SubmitMissionScoreResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SubmitStackTraceRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SubmitStackTrace", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.SubmitStackTraceRequestBody Body;
        
        public SubmitStackTraceRequest() {
        }
        
        public SubmitStackTraceRequest(MissionEditor2.ContentServiceReference.SubmitStackTraceRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SubmitStackTraceRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public MissionEditor2.ContentServiceReference.ProgramType programType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string playerName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string exception;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string extraData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string programVersion;
        
        public SubmitStackTraceRequestBody() {
        }
        
        public SubmitStackTraceRequestBody(MissionEditor2.ContentServiceReference.ProgramType programType, string playerName, string exception, string extraData, string programVersion) {
            this.programType = programType;
            this.playerName = playerName;
            this.exception = exception;
            this.extraData = extraData;
            this.programVersion = programVersion;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SubmitStackTraceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SubmitStackTraceResponse", Namespace="http://tempuri.org/", Order=0)]
        public MissionEditor2.ContentServiceReference.SubmitStackTraceResponseBody Body;
        
        public SubmitStackTraceResponse() {
        }
        
        public SubmitStackTraceResponse(MissionEditor2.ContentServiceReference.SubmitStackTraceResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class SubmitStackTraceResponseBody {
        
        public SubmitStackTraceResponseBody() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ContentServiceSoapChannel : MissionEditor2.ContentServiceReference.ContentServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContentServiceSoapClient : System.ServiceModel.ClientBase<MissionEditor2.ContentServiceReference.ContentServiceSoap>, MissionEditor2.ContentServiceReference.ContentServiceSoap {
        
        private BeginOperationDelegate onBeginNotifyMissionRunDelegate;
        
        private EndOperationDelegate onEndNotifyMissionRunDelegate;
        
        private System.Threading.SendOrPostCallback onNotifyMissionRunCompletedDelegate;
        
        private BeginOperationDelegate onBeginSubmitMissionScoreDelegate;
        
        private EndOperationDelegate onEndSubmitMissionScoreDelegate;
        
        private System.Threading.SendOrPostCallback onSubmitMissionScoreCompletedDelegate;
        
        private BeginOperationDelegate onBeginSubmitStackTraceDelegate;
        
        private EndOperationDelegate onEndSubmitStackTraceDelegate;
        
        private System.Threading.SendOrPostCallback onSubmitStackTraceCompletedDelegate;
        
        public ContentServiceSoapClient() {
        }
        
        public ContentServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContentServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContentServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContentServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> NotifyMissionRunCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SubmitMissionScoreCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SubmitStackTraceCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.NotifyMissionRunResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.NotifyMissionRun(MissionEditor2.ContentServiceReference.NotifyMissionRunRequest request) {
            return base.Channel.NotifyMissionRun(request);
        }
        
        public void NotifyMissionRun(string login, string missionName) {
            MissionEditor2.ContentServiceReference.NotifyMissionRunRequest inValue = new MissionEditor2.ContentServiceReference.NotifyMissionRunRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.NotifyMissionRunRequestBody();
            inValue.Body.login = login;
            inValue.Body.missionName = missionName;
            MissionEditor2.ContentServiceReference.NotifyMissionRunResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).NotifyMissionRun(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult MissionEditor2.ContentServiceReference.ContentServiceSoap.BeginNotifyMissionRun(MissionEditor2.ContentServiceReference.NotifyMissionRunRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginNotifyMissionRun(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginNotifyMissionRun(string login, string missionName, System.AsyncCallback callback, object asyncState) {
            MissionEditor2.ContentServiceReference.NotifyMissionRunRequest inValue = new MissionEditor2.ContentServiceReference.NotifyMissionRunRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.NotifyMissionRunRequestBody();
            inValue.Body.login = login;
            inValue.Body.missionName = missionName;
            return ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).BeginNotifyMissionRun(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.NotifyMissionRunResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.EndNotifyMissionRun(System.IAsyncResult result) {
            return base.Channel.EndNotifyMissionRun(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndNotifyMissionRun(System.IAsyncResult result) {
            MissionEditor2.ContentServiceReference.NotifyMissionRunResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).EndNotifyMissionRun(result);
        }
        
        private System.IAsyncResult OnBeginNotifyMissionRun(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string login = ((string)(inValues[0]));
            string missionName = ((string)(inValues[1]));
            return this.BeginNotifyMissionRun(login, missionName, callback, asyncState);
        }
        
        private object[] OnEndNotifyMissionRun(System.IAsyncResult result) {
            this.EndNotifyMissionRun(result);
            return null;
        }
        
        private void OnNotifyMissionRunCompleted(object state) {
            if ((this.NotifyMissionRunCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.NotifyMissionRunCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void NotifyMissionRunAsync(string login, string missionName) {
            this.NotifyMissionRunAsync(login, missionName, null);
        }
        
        public void NotifyMissionRunAsync(string login, string missionName, object userState) {
            if ((this.onBeginNotifyMissionRunDelegate == null)) {
                this.onBeginNotifyMissionRunDelegate = new BeginOperationDelegate(this.OnBeginNotifyMissionRun);
            }
            if ((this.onEndNotifyMissionRunDelegate == null)) {
                this.onEndNotifyMissionRunDelegate = new EndOperationDelegate(this.OnEndNotifyMissionRun);
            }
            if ((this.onNotifyMissionRunCompletedDelegate == null)) {
                this.onNotifyMissionRunCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnNotifyMissionRunCompleted);
            }
            base.InvokeAsync(this.onBeginNotifyMissionRunDelegate, new object[] {
                        login,
                        missionName}, this.onEndNotifyMissionRunDelegate, this.onNotifyMissionRunCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.SubmitMissionScore(MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest request) {
            return base.Channel.SubmitMissionScore(request);
        }
        
        public void SubmitMissionScore(string login, string passwordHash, string missionName, int score, int gameSeconds) {
            MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest inValue = new MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.SubmitMissionScoreRequestBody();
            inValue.Body.login = login;
            inValue.Body.passwordHash = passwordHash;
            inValue.Body.missionName = missionName;
            inValue.Body.score = score;
            inValue.Body.gameSeconds = gameSeconds;
            MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).SubmitMissionScore(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult MissionEditor2.ContentServiceReference.ContentServiceSoap.BeginSubmitMissionScore(MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSubmitMissionScore(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSubmitMissionScore(string login, string passwordHash, string missionName, int score, int gameSeconds, System.AsyncCallback callback, object asyncState) {
            MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest inValue = new MissionEditor2.ContentServiceReference.SubmitMissionScoreRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.SubmitMissionScoreRequestBody();
            inValue.Body.login = login;
            inValue.Body.passwordHash = passwordHash;
            inValue.Body.missionName = missionName;
            inValue.Body.score = score;
            inValue.Body.gameSeconds = gameSeconds;
            return ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).BeginSubmitMissionScore(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.EndSubmitMissionScore(System.IAsyncResult result) {
            return base.Channel.EndSubmitMissionScore(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndSubmitMissionScore(System.IAsyncResult result) {
            MissionEditor2.ContentServiceReference.SubmitMissionScoreResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).EndSubmitMissionScore(result);
        }
        
        private System.IAsyncResult OnBeginSubmitMissionScore(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string login = ((string)(inValues[0]));
            string passwordHash = ((string)(inValues[1]));
            string missionName = ((string)(inValues[2]));
            int score = ((int)(inValues[3]));
            int gameSeconds = ((int)(inValues[4]));
            return this.BeginSubmitMissionScore(login, passwordHash, missionName, score, gameSeconds, callback, asyncState);
        }
        
        private object[] OnEndSubmitMissionScore(System.IAsyncResult result) {
            this.EndSubmitMissionScore(result);
            return null;
        }
        
        private void OnSubmitMissionScoreCompleted(object state) {
            if ((this.SubmitMissionScoreCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SubmitMissionScoreCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SubmitMissionScoreAsync(string login, string passwordHash, string missionName, int score, int gameSeconds) {
            this.SubmitMissionScoreAsync(login, passwordHash, missionName, score, gameSeconds, null);
        }
        
        public void SubmitMissionScoreAsync(string login, string passwordHash, string missionName, int score, int gameSeconds, object userState) {
            if ((this.onBeginSubmitMissionScoreDelegate == null)) {
                this.onBeginSubmitMissionScoreDelegate = new BeginOperationDelegate(this.OnBeginSubmitMissionScore);
            }
            if ((this.onEndSubmitMissionScoreDelegate == null)) {
                this.onEndSubmitMissionScoreDelegate = new EndOperationDelegate(this.OnEndSubmitMissionScore);
            }
            if ((this.onSubmitMissionScoreCompletedDelegate == null)) {
                this.onSubmitMissionScoreCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSubmitMissionScoreCompleted);
            }
            base.InvokeAsync(this.onBeginSubmitMissionScoreDelegate, new object[] {
                        login,
                        passwordHash,
                        missionName,
                        score,
                        gameSeconds}, this.onEndSubmitMissionScoreDelegate, this.onSubmitMissionScoreCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.SubmitStackTraceResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.SubmitStackTrace(MissionEditor2.ContentServiceReference.SubmitStackTraceRequest request) {
            return base.Channel.SubmitStackTrace(request);
        }
        
        public void SubmitStackTrace(MissionEditor2.ContentServiceReference.ProgramType programType, string playerName, string exception, string extraData, string programVersion) {
            MissionEditor2.ContentServiceReference.SubmitStackTraceRequest inValue = new MissionEditor2.ContentServiceReference.SubmitStackTraceRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.SubmitStackTraceRequestBody();
            inValue.Body.programType = programType;
            inValue.Body.playerName = playerName;
            inValue.Body.exception = exception;
            inValue.Body.extraData = extraData;
            inValue.Body.programVersion = programVersion;
            MissionEditor2.ContentServiceReference.SubmitStackTraceResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).SubmitStackTrace(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult MissionEditor2.ContentServiceReference.ContentServiceSoap.BeginSubmitStackTrace(MissionEditor2.ContentServiceReference.SubmitStackTraceRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSubmitStackTrace(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSubmitStackTrace(MissionEditor2.ContentServiceReference.ProgramType programType, string playerName, string exception, string extraData, string programVersion, System.AsyncCallback callback, object asyncState) {
            MissionEditor2.ContentServiceReference.SubmitStackTraceRequest inValue = new MissionEditor2.ContentServiceReference.SubmitStackTraceRequest();
            inValue.Body = new MissionEditor2.ContentServiceReference.SubmitStackTraceRequestBody();
            inValue.Body.programType = programType;
            inValue.Body.playerName = playerName;
            inValue.Body.exception = exception;
            inValue.Body.extraData = extraData;
            inValue.Body.programVersion = programVersion;
            return ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).BeginSubmitStackTrace(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MissionEditor2.ContentServiceReference.SubmitStackTraceResponse MissionEditor2.ContentServiceReference.ContentServiceSoap.EndSubmitStackTrace(System.IAsyncResult result) {
            return base.Channel.EndSubmitStackTrace(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndSubmitStackTrace(System.IAsyncResult result) {
            MissionEditor2.ContentServiceReference.SubmitStackTraceResponse retVal = ((MissionEditor2.ContentServiceReference.ContentServiceSoap)(this)).EndSubmitStackTrace(result);
        }
        
        private System.IAsyncResult OnBeginSubmitStackTrace(object[] inValues, System.AsyncCallback callback, object asyncState) {
            MissionEditor2.ContentServiceReference.ProgramType programType = ((MissionEditor2.ContentServiceReference.ProgramType)(inValues[0]));
            string playerName = ((string)(inValues[1]));
            string exception = ((string)(inValues[2]));
            string extraData = ((string)(inValues[3]));
            string programVersion = ((string)(inValues[4]));
            return this.BeginSubmitStackTrace(programType, playerName, exception, extraData, programVersion, callback, asyncState);
        }
        
        private object[] OnEndSubmitStackTrace(System.IAsyncResult result) {
            this.EndSubmitStackTrace(result);
            return null;
        }
        
        private void OnSubmitStackTraceCompleted(object state) {
            if ((this.SubmitStackTraceCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SubmitStackTraceCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SubmitStackTraceAsync(MissionEditor2.ContentServiceReference.ProgramType programType, string playerName, string exception, string extraData, string programVersion) {
            this.SubmitStackTraceAsync(programType, playerName, exception, extraData, programVersion, null);
        }
        
        public void SubmitStackTraceAsync(MissionEditor2.ContentServiceReference.ProgramType programType, string playerName, string exception, string extraData, string programVersion, object userState) {
            if ((this.onBeginSubmitStackTraceDelegate == null)) {
                this.onBeginSubmitStackTraceDelegate = new BeginOperationDelegate(this.OnBeginSubmitStackTrace);
            }
            if ((this.onEndSubmitStackTraceDelegate == null)) {
                this.onEndSubmitStackTraceDelegate = new EndOperationDelegate(this.OnEndSubmitStackTrace);
            }
            if ((this.onSubmitStackTraceCompletedDelegate == null)) {
                this.onSubmitStackTraceCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSubmitStackTraceCompleted);
            }
            base.InvokeAsync(this.onBeginSubmitStackTraceDelegate, new object[] {
                        programType,
                        playerName,
                        exception,
                        extraData,
                        programVersion}, this.onEndSubmitStackTraceDelegate, this.onSubmitStackTraceCompletedDelegate, userState);
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEServerGUI.ServerServiceReference {
	[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServerProxy", Namespace="http://schemas.datacontract.org/2004/07/SEModAPIExtensions.API", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class ServerProxy : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double AutosaveIntervalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SEServerGUI.ServerServiceReference.CommandLineArgs CommandLineArgsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InstanceNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsRunningField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double AutosaveInterval {
            get {
                return this.AutosaveIntervalField;
            }
            set {
                if ((this.AutosaveIntervalField.Equals(value) != true)) {
                    this.AutosaveIntervalField = value;
                    this.RaisePropertyChanged("AutosaveInterval");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SEServerGUI.ServerServiceReference.CommandLineArgs CommandLineArgs {
            get {
                return this.CommandLineArgsField;
            }
            set {
                if ((this.CommandLineArgsField.Equals(value) != true)) {
                    this.CommandLineArgsField = value;
                    this.RaisePropertyChanged("CommandLineArgs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InstanceName {
            get {
                return this.InstanceNameField;
            }
            set {
                if ((object.ReferenceEquals(this.InstanceNameField, value) != true)) {
                    this.InstanceNameField = value;
                    this.RaisePropertyChanged("InstanceName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsRunning {
            get {
                return this.IsRunningField;
            }
            set {
                if ((this.IsRunningField.Equals(value) != true)) {
                    this.IsRunningField = value;
                    this.RaisePropertyChanged("IsRunning");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CommandLineArgs", Namespace="http://schemas.datacontract.org/2004/07/SEModAPIExtensions.API")]
    [System.SerializableAttribute()]
    public partial struct CommandLineArgs : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool autoStartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool debugField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string gamePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string instanceNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool noConsoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool noGUIField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string worldNameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool autoStart {
            get {
                return this.autoStartField;
            }
            set {
                if ((this.autoStartField.Equals(value) != true)) {
                    this.autoStartField = value;
                    this.RaisePropertyChanged("autoStart");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool debug {
            get {
                return this.debugField;
            }
            set {
                if ((this.debugField.Equals(value) != true)) {
                    this.debugField = value;
                    this.RaisePropertyChanged("debug");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string gamePath {
            get {
                return this.gamePathField;
            }
            set {
                if ((object.ReferenceEquals(this.gamePathField, value) != true)) {
                    this.gamePathField = value;
                    this.RaisePropertyChanged("gamePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string instanceName {
            get {
                return this.instanceNameField;
            }
            set {
                if ((object.ReferenceEquals(this.instanceNameField, value) != true)) {
                    this.instanceNameField = value;
                    this.RaisePropertyChanged("instanceName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool noConsole {
            get {
                return this.noConsoleField;
            }
            set {
                if ((this.noConsoleField.Equals(value) != true)) {
                    this.noConsoleField = value;
                    this.RaisePropertyChanged("noConsole");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool noGUI {
            get {
                return this.noGUIField;
            }
            set {
                if ((this.noGUIField.Equals(value) != true)) {
                    this.noGUIField = value;
                    this.RaisePropertyChanged("noGUI");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string worldName {
            get {
                return this.worldNameField;
            }
            set {
                if ((object.ReferenceEquals(this.worldNameField, value) != true)) {
                    this.worldNameField = value;
                    this.RaisePropertyChanged("worldName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServerServiceReference.IServerServiceContract")]
    public interface IServerServiceContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/GetServer", ReplyAction="http://tempuri.org/IServerServiceContract/GetServerResponse")]
        SEServerGUI.ServerServiceReference.ServerProxy GetServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/StartServer", ReplyAction="http://tempuri.org/IServerServiceContract/StartServerResponse")]
        void StartServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/StopServer", ReplyAction="http://tempuri.org/IServerServiceContract/StopServerResponse")]
        void StopServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/LoadServerConfig", ReplyAction="http://tempuri.org/IServerServiceContract/LoadServerConfigResponse")]
        void LoadServerConfig();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/SaveServerConfig", ReplyAction="http://tempuri.org/IServerServiceContract/SaveServerConfigResponse")]
        void SaveServerConfig();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerServiceContract/SetAutosaveInterval", ReplyAction="http://tempuri.org/IServerServiceContract/SetAutosaveIntervalResponse")]
        void SetAutosaveInterval(double interval);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServerServiceContractChannel : SEServerGUI.ServerServiceReference.IServerServiceContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServerServiceContractClient : System.ServiceModel.ClientBase<SEServerGUI.ServerServiceReference.IServerServiceContract>, SEServerGUI.ServerServiceReference.IServerServiceContract {
        
        public ServerServiceContractClient() {
        }
        
        public ServerServiceContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServerServiceContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerServiceContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerServiceContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SEServerGUI.ServerServiceReference.ServerProxy GetServer() {
            return base.Channel.GetServer();
        }
        
        public void StartServer() {
            base.Channel.StartServer();
        }
        
        public void StopServer() {
            base.Channel.StopServer();
        }
        
        public void LoadServerConfig() {
            base.Channel.LoadServerConfig();
        }
        
        public void SaveServerConfig() {
            base.Channel.SaveServerConfig();
        }
        
        public void SetAutosaveInterval(double interval) {
            base.Channel.SetAutosaveInterval(interval);
        }
    }
}

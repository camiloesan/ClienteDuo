﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClienteDuo.DataService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/CommunicationService")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
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
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FriendRequest", Namespace="http://schemas.datacontract.org/2004/07/CommunicationService")]
    [System.SerializableAttribute()]
    public partial class FriendRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FriendRequestIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ReceiverIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReceiverUsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SenderIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderUsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StatusField;
        
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
        public int FriendRequestID {
            get {
                return this.FriendRequestIDField;
            }
            set {
                if ((this.FriendRequestIDField.Equals(value) != true)) {
                    this.FriendRequestIDField = value;
                    this.RaisePropertyChanged("FriendRequestID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ReceiverID {
            get {
                return this.ReceiverIDField;
            }
            set {
                if ((this.ReceiverIDField.Equals(value) != true)) {
                    this.ReceiverIDField = value;
                    this.RaisePropertyChanged("ReceiverID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReceiverUsername {
            get {
                return this.ReceiverUsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.ReceiverUsernameField, value) != true)) {
                    this.ReceiverUsernameField = value;
                    this.RaisePropertyChanged("ReceiverUsername");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SenderID {
            get {
                return this.SenderIDField;
            }
            set {
                if ((this.SenderIDField.Equals(value) != true)) {
                    this.SenderIDField = value;
                    this.RaisePropertyChanged("SenderID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SenderUsername {
            get {
                return this.SenderUsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderUsernameField, value) != true)) {
                    this.SenderUsernameField = value;
                    this.RaisePropertyChanged("SenderUsername");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Friendship", Namespace="http://schemas.datacontract.org/2004/07/CommunicationService")]
    [System.SerializableAttribute()]
    public partial class Friendship : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Friend1IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Friend1UsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Friend2IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Friend2UsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FriendshipIDField;
        
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
        public int Friend1ID {
            get {
                return this.Friend1IDField;
            }
            set {
                if ((this.Friend1IDField.Equals(value) != true)) {
                    this.Friend1IDField = value;
                    this.RaisePropertyChanged("Friend1ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Friend1Username {
            get {
                return this.Friend1UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.Friend1UsernameField, value) != true)) {
                    this.Friend1UsernameField = value;
                    this.RaisePropertyChanged("Friend1Username");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Friend2ID {
            get {
                return this.Friend2IDField;
            }
            set {
                if ((this.Friend2IDField.Equals(value) != true)) {
                    this.Friend2IDField = value;
                    this.RaisePropertyChanged("Friend2ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Friend2Username {
            get {
                return this.Friend2UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.Friend2UsernameField, value) != true)) {
                    this.Friend2UsernameField = value;
                    this.RaisePropertyChanged("Friend2Username");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FriendshipID {
            get {
                return this.FriendshipIDField;
            }
            set {
                if ((this.FriendshipIDField.Equals(value) != true)) {
                    this.FriendshipIDField = value;
                    this.RaisePropertyChanged("FriendshipID");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataService.IUsersManager")]
    public interface IUsersManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AddUserToDatabase", ReplyAction="http://tempuri.org/IUsersManager/AddUserToDatabaseResponse")]
        bool AddUserToDatabase(ClienteDuo.DataService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AddUserToDatabase", ReplyAction="http://tempuri.org/IUsersManager/AddUserToDatabaseResponse")]
        System.Threading.Tasks.Task<bool> AddUserToDatabaseAsync(ClienteDuo.DataService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsLoginValid", ReplyAction="http://tempuri.org/IUsersManager/IsLoginValidResponse")]
        ClienteDuo.DataService.User IsLoginValid(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsLoginValid", ReplyAction="http://tempuri.org/IUsersManager/IsLoginValidResponse")]
        System.Threading.Tasks.Task<ClienteDuo.DataService.User> IsLoginValidAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsUsernameTaken", ReplyAction="http://tempuri.org/IUsersManager/IsUsernameTakenResponse")]
        bool IsUsernameTaken(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsUsernameTaken", ReplyAction="http://tempuri.org/IUsersManager/IsUsernameTakenResponse")]
        System.Threading.Tasks.Task<bool> IsUsernameTakenAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsEmailTaken", ReplyAction="http://tempuri.org/IUsersManager/IsEmailTakenResponse")]
        bool IsEmailTaken(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsEmailTaken", ReplyAction="http://tempuri.org/IUsersManager/IsEmailTakenResponse")]
        System.Threading.Tasks.Task<bool> IsEmailTakenAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/SendFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/SendFriendRequestResponse")]
        bool SendFriendRequest(int senderID, int receiverID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/SendFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/SendFriendRequestResponse")]
        System.Threading.Tasks.Task<bool> SendFriendRequestAsync(int senderID, int receiverID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AcceptFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/AcceptFriendRequestResponse")]
        bool AcceptFriendRequest(ClienteDuo.DataService.FriendRequest friendRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AcceptFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/AcceptFriendRequestResponse")]
        System.Threading.Tasks.Task<bool> AcceptFriendRequestAsync(ClienteDuo.DataService.FriendRequest friendRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/RejectFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/RejectFriendRequestResponse")]
        bool RejectFriendRequest(int friendRequestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/RejectFriendRequest", ReplyAction="http://tempuri.org/IUsersManager/RejectFriendRequestResponse")]
        System.Threading.Tasks.Task<bool> RejectFriendRequestAsync(int friendRequestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetFriendRequestsList", ReplyAction="http://tempuri.org/IUsersManager/GetFriendRequestsListResponse")]
        ClienteDuo.DataService.FriendRequest[] GetFriendRequestsList(int userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetFriendRequestsList", ReplyAction="http://tempuri.org/IUsersManager/GetFriendRequestsListResponse")]
        System.Threading.Tasks.Task<ClienteDuo.DataService.FriendRequest[]> GetFriendRequestsListAsync(int userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetFriendsList", ReplyAction="http://tempuri.org/IUsersManager/GetFriendsListResponse")]
        ClienteDuo.DataService.Friendship[] GetFriendsList(int userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetFriendsList", ReplyAction="http://tempuri.org/IUsersManager/GetFriendsListResponse")]
        System.Threading.Tasks.Task<ClienteDuo.DataService.Friendship[]> GetFriendsListAsync(int userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetOnlineFriends", ReplyAction="http://tempuri.org/IUsersManager/GetOnlineFriendsResponse")]
        string[] GetOnlineFriends(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/GetOnlineFriends", ReplyAction="http://tempuri.org/IUsersManager/GetOnlineFriendsResponse")]
        System.Threading.Tasks.Task<string[]> GetOnlineFriendsAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersManagerChannel : ClienteDuo.DataService.IUsersManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersManagerClient : System.ServiceModel.ClientBase<ClienteDuo.DataService.IUsersManager>, ClienteDuo.DataService.IUsersManager {
        
        public UsersManagerClient() {
        }
        
        public UsersManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool AddUserToDatabase(ClienteDuo.DataService.User user) {
            return base.Channel.AddUserToDatabase(user);
        }
        
        public System.Threading.Tasks.Task<bool> AddUserToDatabaseAsync(ClienteDuo.DataService.User user) {
            return base.Channel.AddUserToDatabaseAsync(user);
        }
        
        public ClienteDuo.DataService.User IsLoginValid(string username, string password) {
            return base.Channel.IsLoginValid(username, password);
        }
        
        public System.Threading.Tasks.Task<ClienteDuo.DataService.User> IsLoginValidAsync(string username, string password) {
            return base.Channel.IsLoginValidAsync(username, password);
        }
        
        public bool IsUsernameTaken(string username) {
            return base.Channel.IsUsernameTaken(username);
        }
        
        public System.Threading.Tasks.Task<bool> IsUsernameTakenAsync(string username) {
            return base.Channel.IsUsernameTakenAsync(username);
        }
        
        public bool IsEmailTaken(string email) {
            return base.Channel.IsEmailTaken(email);
        }
        
        public System.Threading.Tasks.Task<bool> IsEmailTakenAsync(string email) {
            return base.Channel.IsEmailTakenAsync(email);
        }
        
        public bool SendFriendRequest(int senderID, int receiverID) {
            return base.Channel.SendFriendRequest(senderID, receiverID);
        }
        
        public System.Threading.Tasks.Task<bool> SendFriendRequestAsync(int senderID, int receiverID) {
            return base.Channel.SendFriendRequestAsync(senderID, receiverID);
        }
        
        public bool AcceptFriendRequest(ClienteDuo.DataService.FriendRequest friendRequest) {
            return base.Channel.AcceptFriendRequest(friendRequest);
        }
        
        public System.Threading.Tasks.Task<bool> AcceptFriendRequestAsync(ClienteDuo.DataService.FriendRequest friendRequest) {
            return base.Channel.AcceptFriendRequestAsync(friendRequest);
        }
        
        public bool RejectFriendRequest(int friendRequestID) {
            return base.Channel.RejectFriendRequest(friendRequestID);
        }
        
        public System.Threading.Tasks.Task<bool> RejectFriendRequestAsync(int friendRequestID) {
            return base.Channel.RejectFriendRequestAsync(friendRequestID);
        }
        
        public ClienteDuo.DataService.FriendRequest[] GetFriendRequestsList(int userID) {
            return base.Channel.GetFriendRequestsList(userID);
        }
        
        public System.Threading.Tasks.Task<ClienteDuo.DataService.FriendRequest[]> GetFriendRequestsListAsync(int userID) {
            return base.Channel.GetFriendRequestsListAsync(userID);
        }
        
        public ClienteDuo.DataService.Friendship[] GetFriendsList(int userID) {
            return base.Channel.GetFriendsList(userID);
        }
        
        public System.Threading.Tasks.Task<ClienteDuo.DataService.Friendship[]> GetFriendsListAsync(int userID) {
            return base.Channel.GetFriendsListAsync(userID);
        }
        
        public string[] GetOnlineFriends(string username) {
            return base.Channel.GetOnlineFriends(username);
        }
        
        public System.Threading.Tasks.Task<string[]> GetOnlineFriendsAsync(string username) {
            return base.Channel.GetOnlineFriendsAsync(username);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataService.IPartyManager", CallbackContract=typeof(ClienteDuo.DataService.IPartyManagerCallback))]
    public interface IPartyManager {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/NewParty")]
        void NewParty(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/NewParty")]
        System.Threading.Tasks.Task NewPartyAsync(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/JoinParty")]
        void JoinParty(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/JoinParty")]
        System.Threading.Tasks.Task JoinPartyAsync(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/SendMessage")]
        void SendMessage(int partyCode, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(int partyCode, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/LeaveParty")]
        void LeaveParty(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/LeaveParty")]
        System.Threading.Tasks.Task LeavePartyAsync(int partyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/StartGame")]
        void StartGame(int partyCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/StartGame")]
        System.Threading.Tasks.Task StartGameAsync(int partyCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/IsPlayerActive")]
        void IsPlayerActive();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartyManager/IsPlayerActive")]
        System.Threading.Tasks.Task IsPlayerActiveAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPartyManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyManager/PartyCreated", ReplyAction="http://tempuri.org/IPartyManager/PartyCreatedResponse")]
        void PartyCreated(System.Collections.Generic.Dictionary<string, object> playersInLobby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyManager/PlayerJoined", ReplyAction="http://tempuri.org/IPartyManager/PlayerJoinedResponse")]
        void PlayerJoined(System.Collections.Generic.Dictionary<string, object> playersInLobby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyManager/MessageReceived", ReplyAction="http://tempuri.org/IPartyManager/MessageReceivedResponse")]
        void MessageReceived(string messageSent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyManager/PlayerLeft", ReplyAction="http://tempuri.org/IPartyManager/PlayerLeftResponse")]
        void PlayerLeft(System.Collections.Generic.Dictionary<string, object> playersInLobby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyManager/GameStarted", ReplyAction="http://tempuri.org/IPartyManager/GameStartedResponse")]
        void GameStarted();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPartyManagerChannel : ClienteDuo.DataService.IPartyManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PartyManagerClient : System.ServiceModel.DuplexClientBase<ClienteDuo.DataService.IPartyManager>, ClienteDuo.DataService.IPartyManager {
        
        public PartyManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public PartyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public PartyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PartyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PartyManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void NewParty(int partyCode, string username) {
            base.Channel.NewParty(partyCode, username);
        }
        
        public System.Threading.Tasks.Task NewPartyAsync(int partyCode, string username) {
            return base.Channel.NewPartyAsync(partyCode, username);
        }
        
        public void JoinParty(int partyCode, string username) {
            base.Channel.JoinParty(partyCode, username);
        }
        
        public System.Threading.Tasks.Task JoinPartyAsync(int partyCode, string username) {
            return base.Channel.JoinPartyAsync(partyCode, username);
        }
        
        public void SendMessage(int partyCode, string message) {
            base.Channel.SendMessage(partyCode, message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(int partyCode, string message) {
            return base.Channel.SendMessageAsync(partyCode, message);
        }
        
        public void LeaveParty(int partyCode, string username) {
            base.Channel.LeaveParty(partyCode, username);
        }
        
        public System.Threading.Tasks.Task LeavePartyAsync(int partyCode, string username) {
            return base.Channel.LeavePartyAsync(partyCode, username);
        }
        
        public void StartGame(int partyCode) {
            base.Channel.StartGame(partyCode);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(int partyCode) {
            return base.Channel.StartGameAsync(partyCode);
        }
        
        public void IsPlayerActive() {
            base.Channel.IsPlayerActive();
        }
        
        public System.Threading.Tasks.Task IsPlayerActiveAsync() {
            return base.Channel.IsPlayerActiveAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataService.IPartyValidator")]
    public interface IPartyValidator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyValidator/IsPartyExistent", ReplyAction="http://tempuri.org/IPartyValidator/IsPartyExistentResponse")]
        bool IsPartyExistent(int partyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyValidator/IsPartyExistent", ReplyAction="http://tempuri.org/IPartyValidator/IsPartyExistentResponse")]
        System.Threading.Tasks.Task<bool> IsPartyExistentAsync(int partyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyValidator/IsSpaceAvailable", ReplyAction="http://tempuri.org/IPartyValidator/IsSpaceAvailableResponse")]
        bool IsSpaceAvailable(int partyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartyValidator/IsSpaceAvailable", ReplyAction="http://tempuri.org/IPartyValidator/IsSpaceAvailableResponse")]
        System.Threading.Tasks.Task<bool> IsSpaceAvailableAsync(int partyCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPartyValidatorChannel : ClienteDuo.DataService.IPartyValidator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PartyValidatorClient : System.ServiceModel.ClientBase<ClienteDuo.DataService.IPartyValidator>, ClienteDuo.DataService.IPartyValidator {
        
        public PartyValidatorClient() {
        }
        
        public PartyValidatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PartyValidatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PartyValidatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PartyValidatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsPartyExistent(int partyCode) {
            return base.Channel.IsPartyExistent(partyCode);
        }
        
        public System.Threading.Tasks.Task<bool> IsPartyExistentAsync(int partyCode) {
            return base.Channel.IsPartyExistentAsync(partyCode);
        }
        
        public bool IsSpaceAvailable(int partyCode) {
            return base.Channel.IsSpaceAvailable(partyCode);
        }
        
        public System.Threading.Tasks.Task<bool> IsSpaceAvailableAsync(int partyCode) {
            return base.Channel.IsSpaceAvailableAsync(partyCode);
        }
    }
}

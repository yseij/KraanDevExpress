﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KraanDevExpress.Module.Sales31 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", ConfigurationName="Sales31.MessageServiceSoap")]
    public interface MessageServiceSoap {
        
        // CODEGEN: Generating message contract since the operation GetAvailableMessages is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/GetAvailableMessages", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse GetAvailableMessages(KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/GetAvailableMessages", ReplyAction="*")]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse> GetAvailableMessagesAsync(KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest request);
        
        // CODEGEN: Generating message contract since the operation GetMessage is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/GetMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        KraanDevExpress.Module.Sales31.GetMessageResponse GetMessage(KraanDevExpress.Module.Sales31.GetMessageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/GetMessage", ReplyAction="*")]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetMessageResponse> GetMessageAsync(KraanDevExpress.Module.Sales31.GetMessageRequest request);
        
        // CODEGEN: Generating message contract since the operation DeleteMessage is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/DeleteMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        KraanDevExpress.Module.Sales31.DeleteMessageResponse DeleteMessage(KraanDevExpress.Module.Sales31.DeleteMessageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/DeleteMessage", ReplyAction="*")]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> DeleteMessageAsync(KraanDevExpress.Module.Sales31.DeleteMessageRequest request);
        
        // CODEGEN: Generating message contract since the operation PostMessage is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/PostMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        KraanDevExpress.Module.Sales31.DeleteMessageResponse PostMessage(KraanDevExpress.Module.Sales31.PostMessageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="messageservice/PostMessage", ReplyAction="*")]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> PostMessageAsync(KraanDevExpress.Module.Sales31.PostMessageRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class CustomInfoType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool isTestMessageField;
        
        private string languageCodeField;
        
        private System.Nullable<bool> isContentCompressedField;
        
        private string applicationIdField;
        
        private string versionIdField;
        
        private string relationIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool IsTestMessage {
            get {
                return this.isTestMessageField;
            }
            set {
                this.isTestMessageField = value;
                this.RaisePropertyChanged("IsTestMessage");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string LanguageCode {
            get {
                return this.languageCodeField;
            }
            set {
                this.languageCodeField = value;
                this.RaisePropertyChanged("LanguageCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<bool> IsContentCompressed {
            get {
                return this.isContentCompressedField;
            }
            set {
                this.isContentCompressedField = value;
                this.RaisePropertyChanged("IsContentCompressed");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string ApplicationId {
            get {
                return this.applicationIdField;
            }
            set {
                this.applicationIdField = value;
                this.RaisePropertyChanged("ApplicationId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string VersionId {
            get {
                return this.versionIdField;
            }
            set {
                this.versionIdField = value;
                this.RaisePropertyChanged("VersionId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string RelationId {
            get {
                return this.relationIdField;
            }
            set {
                this.relationIdField = value;
                this.RaisePropertyChanged("RelationId");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class MessageResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MessageType messageField;
        
        private bool messageResultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MessageType Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public bool MessageResult {
            get {
                return this.messageResultField;
            }
            set {
                this.messageResultField = value;
                this.RaisePropertyChanged("MessageResult");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class MessageType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MessagePropertiesType msgPropertiesField;
        
        private string msgContentField;
        
        private AttachmentType[] attachmentField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public MessagePropertiesType MsgProperties {
            get {
                return this.msgPropertiesField;
            }
            set {
                this.msgPropertiesField = value;
                this.RaisePropertyChanged("MsgProperties");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MsgContent {
            get {
                return this.msgContentField;
            }
            set {
                this.msgContentField = value;
                this.RaisePropertyChanged("MsgContent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attachment", Order=2)]
        public AttachmentType[] Attachment {
            get {
                return this.attachmentField;
            }
            set {
                this.attachmentField = value;
                this.RaisePropertyChanged("Attachment");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class MessagePropertiesType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msgIdField;
        
        private System.DateTime msgDateTimeField;
        
        private string msgFormatField;
        
        private string msgVersionField;
        
        private string msgTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string MsgId {
            get {
                return this.msgIdField;
            }
            set {
                this.msgIdField = value;
                this.RaisePropertyChanged("MsgId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime MsgDateTime {
            get {
                return this.msgDateTimeField;
            }
            set {
                this.msgDateTimeField = value;
                this.RaisePropertyChanged("MsgDateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string MsgFormat {
            get {
                return this.msgFormatField;
            }
            set {
                this.msgFormatField = value;
                this.RaisePropertyChanged("MsgFormat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string MsgVersion {
            get {
                return this.msgVersionField;
            }
            set {
                this.msgVersionField = value;
                this.RaisePropertyChanged("MsgVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string MsgType {
            get {
                return this.msgTypeField;
            }
            set {
                this.msgTypeField = value;
                this.RaisePropertyChanged("MsgType");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class AttachmentType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string uRLField;
        
        private string documentTypeField;
        
        private string fileTypeField;
        
        private string fileNameField;
        
        private byte[] attachedDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string URL {
            get {
                return this.uRLField;
            }
            set {
                this.uRLField = value;
                this.RaisePropertyChanged("URL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string DocumentType {
            get {
                return this.documentTypeField;
            }
            set {
                this.documentTypeField = value;
                this.RaisePropertyChanged("DocumentType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string FileType {
            get {
                return this.fileTypeField;
            }
            set {
                this.fileTypeField = value;
                this.RaisePropertyChanged("FileType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("FileName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=4)]
        public byte[] AttachedData {
            get {
                return this.attachedDataField;
            }
            set {
                this.attachedDataField = value;
                this.RaisePropertyChanged("AttachedData");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class MessageRequestResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MessageType messageRequestResultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MessageType MessageRequestResult {
            get {
                return this.messageRequestResultField;
            }
            set {
                this.messageRequestResultField = value;
                this.RaisePropertyChanged("MessageRequestResult");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class MessageRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msgIdField;
        
        private string msgFormatField;
        
        private string msgVersionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string MsgId {
            get {
                return this.msgIdField;
            }
            set {
                this.msgIdField = value;
                this.RaisePropertyChanged("MsgId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string MsgFormat {
            get {
                return this.msgFormatField;
            }
            set {
                this.msgFormatField = value;
                this.RaisePropertyChanged("MsgFormat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string MsgVersion {
            get {
                return this.msgVersionField;
            }
            set {
                this.msgVersionField = value;
                this.RaisePropertyChanged("MsgVersion");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
    public partial class AvailableMessagesRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msgTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string MsgType {
            get {
                return this.msgTypeField;
            }
            set {
                this.msgTypeField = value;
                this.RaisePropertyChanged("MsgType");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAvailableMessagesRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
        public KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.AvailableMessagesRequestType AvailableMessagesRequest;
        
        public GetAvailableMessagesRequest() {
        }
        
        public GetAvailableMessagesRequest(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.AvailableMessagesRequestType AvailableMessagesRequest) {
            this.CustomInfo = CustomInfo;
            this.AvailableMessagesRequest = AvailableMessagesRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAvailableMessagesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MessageList")]
        public KraanDevExpress.Module.Sales31.MessagePropertiesType[] AvailableMessagesResponse;
        
        public GetAvailableMessagesResponse() {
        }
        
        public GetAvailableMessagesResponse(KraanDevExpress.Module.Sales31.MessagePropertiesType[] AvailableMessagesResponse) {
            this.AvailableMessagesResponse = AvailableMessagesResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMessageRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
        public KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest;
        
        public GetMessageRequest() {
        }
        
        public GetMessageRequest(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            this.CustomInfo = CustomInfo;
            this.MessageRequest = MessageRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMessageResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.MessageRequestResponseType MessageRequestResponse;
        
        public GetMessageResponse() {
        }
        
        public GetMessageResponse(KraanDevExpress.Module.Sales31.MessageRequestResponseType MessageRequestResponse) {
            this.MessageRequestResponse = MessageRequestResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DeleteMessageRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
        public KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest;
        
        public DeleteMessageRequest() {
        }
        
        public DeleteMessageRequest(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            this.CustomInfo = CustomInfo;
            this.MessageRequest = MessageRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DeleteMessageResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.MessageResponseType MessageResponse;
        
        public DeleteMessageResponse() {
        }
        
        public DeleteMessageResponse(KraanDevExpress.Module.Sales31.MessageResponseType MessageResponse) {
            this.MessageResponse = MessageResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PostMessageRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1")]
        public KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www.ketenstandaard.nl/WS/MessageService/3.1", Order=0)]
        public KraanDevExpress.Module.Sales31.MessageType Message;
        
        public PostMessageRequest() {
        }
        
        public PostMessageRequest(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageType Message) {
            this.CustomInfo = CustomInfo;
            this.Message = Message;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MessageServiceSoapChannel : KraanDevExpress.Module.Sales31.MessageServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessageServiceSoapClient : System.ServiceModel.ClientBase<KraanDevExpress.Module.Sales31.MessageServiceSoap>, KraanDevExpress.Module.Sales31.MessageServiceSoap {
        
        public MessageServiceSoapClient() {
        }
        
        public MessageServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessageServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse KraanDevExpress.Module.Sales31.MessageServiceSoap.GetAvailableMessages(KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest request) {
            return base.Channel.GetAvailableMessages(request);
        }
        
        public KraanDevExpress.Module.Sales31.MessagePropertiesType[] GetAvailableMessages(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.AvailableMessagesRequestType AvailableMessagesRequest) {
            KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest inValue = new KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.AvailableMessagesRequest = AvailableMessagesRequest;
            KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse retVal = ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).GetAvailableMessages(inValue);
            return retVal.AvailableMessagesResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse> KraanDevExpress.Module.Sales31.MessageServiceSoap.GetAvailableMessagesAsync(KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest request) {
            return base.Channel.GetAvailableMessagesAsync(request);
        }
        
        public System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetAvailableMessagesResponse> GetAvailableMessagesAsync(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.AvailableMessagesRequestType AvailableMessagesRequest) {
            KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest inValue = new KraanDevExpress.Module.Sales31.GetAvailableMessagesRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.AvailableMessagesRequest = AvailableMessagesRequest;
            return ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).GetAvailableMessagesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        KraanDevExpress.Module.Sales31.GetMessageResponse KraanDevExpress.Module.Sales31.MessageServiceSoap.GetMessage(KraanDevExpress.Module.Sales31.GetMessageRequest request) {
            return base.Channel.GetMessage(request);
        }
        
        public KraanDevExpress.Module.Sales31.MessageRequestResponseType GetMessage(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            KraanDevExpress.Module.Sales31.GetMessageRequest inValue = new KraanDevExpress.Module.Sales31.GetMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.MessageRequest = MessageRequest;
            KraanDevExpress.Module.Sales31.GetMessageResponse retVal = ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).GetMessage(inValue);
            return retVal.MessageRequestResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetMessageResponse> KraanDevExpress.Module.Sales31.MessageServiceSoap.GetMessageAsync(KraanDevExpress.Module.Sales31.GetMessageRequest request) {
            return base.Channel.GetMessageAsync(request);
        }
        
        public System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.GetMessageResponse> GetMessageAsync(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            KraanDevExpress.Module.Sales31.GetMessageRequest inValue = new KraanDevExpress.Module.Sales31.GetMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.MessageRequest = MessageRequest;
            return ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).GetMessageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        KraanDevExpress.Module.Sales31.DeleteMessageResponse KraanDevExpress.Module.Sales31.MessageServiceSoap.DeleteMessage(KraanDevExpress.Module.Sales31.DeleteMessageRequest request) {
            return base.Channel.DeleteMessage(request);
        }
        
        public KraanDevExpress.Module.Sales31.MessageResponseType DeleteMessage(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            KraanDevExpress.Module.Sales31.DeleteMessageRequest inValue = new KraanDevExpress.Module.Sales31.DeleteMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.MessageRequest = MessageRequest;
            KraanDevExpress.Module.Sales31.DeleteMessageResponse retVal = ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).DeleteMessage(inValue);
            return retVal.MessageResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> KraanDevExpress.Module.Sales31.MessageServiceSoap.DeleteMessageAsync(KraanDevExpress.Module.Sales31.DeleteMessageRequest request) {
            return base.Channel.DeleteMessageAsync(request);
        }
        
        public System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> DeleteMessageAsync(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageRequestType MessageRequest) {
            KraanDevExpress.Module.Sales31.DeleteMessageRequest inValue = new KraanDevExpress.Module.Sales31.DeleteMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.MessageRequest = MessageRequest;
            return ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).DeleteMessageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        KraanDevExpress.Module.Sales31.DeleteMessageResponse KraanDevExpress.Module.Sales31.MessageServiceSoap.PostMessage(KraanDevExpress.Module.Sales31.PostMessageRequest request) {
            return base.Channel.PostMessage(request);
        }
        
        public KraanDevExpress.Module.Sales31.MessageResponseType PostMessage(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageType Message) {
            KraanDevExpress.Module.Sales31.PostMessageRequest inValue = new KraanDevExpress.Module.Sales31.PostMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.Message = Message;
            KraanDevExpress.Module.Sales31.DeleteMessageResponse retVal = ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).PostMessage(inValue);
            return retVal.MessageResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> KraanDevExpress.Module.Sales31.MessageServiceSoap.PostMessageAsync(KraanDevExpress.Module.Sales31.PostMessageRequest request) {
            return base.Channel.PostMessageAsync(request);
        }
        
        public System.Threading.Tasks.Task<KraanDevExpress.Module.Sales31.DeleteMessageResponse> PostMessageAsync(KraanDevExpress.Module.Sales31.CustomInfoType CustomInfo, KraanDevExpress.Module.Sales31.MessageType Message) {
            KraanDevExpress.Module.Sales31.PostMessageRequest inValue = new KraanDevExpress.Module.Sales31.PostMessageRequest();
            inValue.CustomInfo = CustomInfo;
            inValue.Message = Message;
            return ((KraanDevExpress.Module.Sales31.MessageServiceSoap)(this)).PostMessageAsync(inValue);
        }
    }
}
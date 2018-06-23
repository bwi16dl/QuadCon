﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopClient.TestService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TestData", Namespace="http://schemas.datacontract.org/2004/07/Test")]
    [System.SerializableAttribute()]
    public partial class TestData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TemperatureField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WindField;
        
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
        public double Temperature {
            get {
                return this.TemperatureField;
            }
            set {
                if ((this.TemperatureField.Equals(value) != true)) {
                    this.TemperatureField = value;
                    this.RaisePropertyChanged("Temperature");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Wind {
            get {
                return this.WindField;
            }
            set {
                if ((object.ReferenceEquals(this.WindField, value) != true)) {
                    this.WindField = value;
                    this.RaisePropertyChanged("Wind");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TestService.ITestService")]
    public interface ITestService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetName", ReplyAction="http://tempuri.org/ITestService/GetNameResponse")]
        string GetName(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetName", ReplyAction="http://tempuri.org/ITestService/GetNameResponse")]
        System.Threading.Tasks.Task<string> GetNameAsync(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetOneDay", ReplyAction="http://tempuri.org/ITestService/GetOneDayResponse")]
        DesktopClient.TestService.TestData GetOneDay(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetOneDay", ReplyAction="http://tempuri.org/ITestService/GetOneDayResponse")]
        System.Threading.Tasks.Task<DesktopClient.TestService.TestData> GetOneDayAsync(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetThreeDays", ReplyAction="http://tempuri.org/ITestService/GetThreeDaysResponse")]
        System.Collections.Generic.List<DesktopClient.TestService.TestData> GetThreeDays(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/GetThreeDays", ReplyAction="http://tempuri.org/ITestService/GetThreeDaysResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<DesktopClient.TestService.TestData>> GetThreeDaysAsync(string sourceName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetName", ReplyAction="http://tempuri.org/ITestService/SetNameResponse")]
        void SetName(string sourceName, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetName", ReplyAction="http://tempuri.org/ITestService/SetNameResponse")]
        System.Threading.Tasks.Task SetNameAsync(string sourceName, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetOneDay", ReplyAction="http://tempuri.org/ITestService/SetOneDayResponse")]
        void SetOneDay(string sourceName, DesktopClient.TestService.TestData oneDay);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetOneDay", ReplyAction="http://tempuri.org/ITestService/SetOneDayResponse")]
        System.Threading.Tasks.Task SetOneDayAsync(string sourceName, DesktopClient.TestService.TestData oneDay);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetThreeDays", ReplyAction="http://tempuri.org/ITestService/SetThreeDaysResponse")]
        void SetThreeDays(string sourceName, System.Collections.Generic.List<DesktopClient.TestService.TestData> threeDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/SetThreeDays", ReplyAction="http://tempuri.org/ITestService/SetThreeDaysResponse")]
        System.Threading.Tasks.Task SetThreeDaysAsync(string sourceName, System.Collections.Generic.List<DesktopClient.TestService.TestData> threeDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/Trigger", ReplyAction="http://tempuri.org/ITestService/TriggerResponse")]
        void Trigger(string sourceName, string printWhat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestService/Trigger", ReplyAction="http://tempuri.org/ITestService/TriggerResponse")]
        System.Threading.Tasks.Task TriggerAsync(string sourceName, string printWhat);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestServiceChannel : DesktopClient.TestService.ITestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestServiceClient : System.ServiceModel.ClientBase<DesktopClient.TestService.ITestService>, DesktopClient.TestService.ITestService {
        
        public TestServiceClient() {
        }
        
        public TestServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetName(string sourceName) {
            return base.Channel.GetName(sourceName);
        }
        
        public System.Threading.Tasks.Task<string> GetNameAsync(string sourceName) {
            return base.Channel.GetNameAsync(sourceName);
        }
        
        public DesktopClient.TestService.TestData GetOneDay(string sourceName) {
            return base.Channel.GetOneDay(sourceName);
        }
        
        public System.Threading.Tasks.Task<DesktopClient.TestService.TestData> GetOneDayAsync(string sourceName) {
            return base.Channel.GetOneDayAsync(sourceName);
        }
        
        public System.Collections.Generic.List<DesktopClient.TestService.TestData> GetThreeDays(string sourceName) {
            return base.Channel.GetThreeDays(sourceName);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<DesktopClient.TestService.TestData>> GetThreeDaysAsync(string sourceName) {
            return base.Channel.GetThreeDaysAsync(sourceName);
        }
        
        public void SetName(string sourceName, string name) {
            base.Channel.SetName(sourceName, name);
        }
        
        public System.Threading.Tasks.Task SetNameAsync(string sourceName, string name) {
            return base.Channel.SetNameAsync(sourceName, name);
        }
        
        public void SetOneDay(string sourceName, DesktopClient.TestService.TestData oneDay) {
            base.Channel.SetOneDay(sourceName, oneDay);
        }
        
        public System.Threading.Tasks.Task SetOneDayAsync(string sourceName, DesktopClient.TestService.TestData oneDay) {
            return base.Channel.SetOneDayAsync(sourceName, oneDay);
        }
        
        public void SetThreeDays(string sourceName, System.Collections.Generic.List<DesktopClient.TestService.TestData> threeDays) {
            base.Channel.SetThreeDays(sourceName, threeDays);
        }
        
        public System.Threading.Tasks.Task SetThreeDaysAsync(string sourceName, System.Collections.Generic.List<DesktopClient.TestService.TestData> threeDays) {
            return base.Channel.SetThreeDaysAsync(sourceName, threeDays);
        }
        
        public void Trigger(string sourceName, string printWhat) {
            base.Channel.Trigger(sourceName, printWhat);
        }
        
        public System.Threading.Tasks.Task TriggerAsync(string sourceName, string printWhat) {
            return base.Channel.TriggerAsync(sourceName, printWhat);
        }
    }
}
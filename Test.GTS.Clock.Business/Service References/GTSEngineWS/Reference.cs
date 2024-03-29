﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTSTestUnit.Clock.Business.GTSEngineWS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GTSEngineWS.ITotalWebService")]
    public interface ITotalWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITotalWebService/GTS_ExecuteByPersonID", ReplyAction="http://tempuri.org/ITotalWebService/GTS_ExecuteByPersonIDResponse")]
        void GTS_ExecuteByPersonID(decimal PersonId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITotalWebService/GTS_ExecuteByPersonIdAndToDate", ReplyAction="http://tempuri.org/ITotalWebService/GTS_ExecuteByPersonIdAndToDateResponse")]
        bool GTS_ExecuteByPersonIdAndToDate(decimal PersonId, System.DateTime Date);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITotalWebService/GTS_ExecuteAll")]
        void GTS_ExecuteAll();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITotalWebService/GTS_ExecuteAllByToDate")]
        void GTS_ExecuteAllByToDate(System.DateTime Date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITotalWebService/Clock_FillByPersonID", ReplyAction="http://tempuri.org/ITotalWebService/Clock_FillByPersonIDResponse")]
        void Clock_FillByPersonID(decimal PersonId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITotalWebService/Clock_ExecuteByToDate")]
        void Clock_ExecuteByToDate(string toDate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITotalWebServiceChannel : GTSTestUnit.Clock.Business.GTSEngineWS.ITotalWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TotalWebServiceClient : System.ServiceModel.ClientBase<GTSTestUnit.Clock.Business.GTSEngineWS.ITotalWebService>, GTSTestUnit.Clock.Business.GTSEngineWS.ITotalWebService {
        
        public TotalWebServiceClient() {
        }
        
        public TotalWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TotalWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TotalWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TotalWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void GTS_ExecuteByPersonID(decimal PersonId) {
            base.Channel.GTS_ExecuteByPersonID(PersonId);
        }
        
        public bool GTS_ExecuteByPersonIdAndToDate(decimal PersonId, System.DateTime Date) {
            return base.Channel.GTS_ExecuteByPersonIdAndToDate(PersonId, Date);
        }
        
        public void GTS_ExecuteAll() {
            base.Channel.GTS_ExecuteAll();
        }
        
        public void GTS_ExecuteAllByToDate(System.DateTime Date) {
            base.Channel.GTS_ExecuteAllByToDate(Date);
        }
        
        public void Clock_FillByPersonID(decimal PersonId) {
            base.Channel.Clock_FillByPersonID(PersonId);
        }
        
        public void Clock_ExecuteByToDate(string toDate) {
            base.Channel.Clock_ExecuteByToDate(toDate);
        }
    }
}

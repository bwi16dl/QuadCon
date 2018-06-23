using BusinessRules;
using BusinessRules.DataCollector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Controller.Services.Admin
{
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        List<Rule> GetBusinessRules();
        [OperationContract]
        List<ExposedData> GetExposedData();
        [OperationContract]
        void AddBusinessRule(Rule rule);
        [OperationContract]
        void RemoveBusinessRule(Rule rule);
        
    }
}

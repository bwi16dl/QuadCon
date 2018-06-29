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
    // Exposed methods to collect information relevant for admin functionality, uses a bit different implementation than other methods, therefore commented here separately
    [ServiceContract]
    public interface IAdminService
    {
        // Method to collect existing business rules (from DB)
        [OperationContract]
        List<Rule> GetBusinessRules();

        // Method should collect definition of loaded classes (i.e. attributes and methods exposed to admin with A and AGet naming convention)
        [OperationContract]
        List<ExposedData> GetExposedData();

        // Methods to add new or remove existing business rule
        [OperationContract]
        void AddBusinessRule(Rule rule);
        [OperationContract]
        void RemoveBusinessRule(Rule rule);
        
    }
}

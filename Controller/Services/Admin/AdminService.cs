using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessRules;
using BusinessRules.DataCollector;

namespace Controller.Services.Admin
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class AdminService : IAdminService
    {
        public List<Rule> GetBusinessRules() { return BusinessRules.BusinessRules.GetRules(); }
        public void AddBusinessRule(Rule rule) { BusinessRules.BusinessRules.AddRule(rule); }
        public void RemoveBusinessRule(Rule rule) { BusinessRules.BusinessRules.RemoveRule(rule); }
        public List<ExposedData> GetExposedData() { return Collector.GetExposedData(); }
    }
}

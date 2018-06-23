using RuleObjects.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessRules
{
    [DataContract]
    public static class BusinessRules
    {
        [DataMember]
        public static List<Rule> Rules = new List<Rule>();

        #region INTERNAL
       private static DBHandler dataLayer = new DBHandler();

        public static void Initialize()
        {
            Rules = dataLayer.GetBusinessRules();
            new Thread(StartComparing).Start();
        }

        public static void StartComparing()
        {
            while(true)
            {
                Thread.Sleep(60000);
                foreach (var i in Rules) { i.Execute(); }
            }
        }
        #endregion

        #region INTERFACE
        public static List<Rule> GetRules() { return Rules; }

        public static void AddRule(Rule rule)
        {
            if (rule != null)
            {
                rule.Id = dataLayer.AddBusinessRule(rule);
                Rules.Add(rule);
            }
        }

        public static void RemoveRule(Rule rule)
        {
            if (rule != null)
            {
                Rule toRemove = null;
                List<Rule> remaining = new List<Rule>();
                
                foreach (var i in Rules)
                {
                    bool actionIdentical = i.Name.Equals(rule.Name) && i.Source.Equals(rule.Source) && i.Action.Equals(rule.Action) && i.Parameter.Equals(rule.Parameter);
                    bool validityIdentical = i.DayFrom.Equals(rule.DayFrom) && i.DayTill.Equals(rule.DayTill) && i.TimeFrom.Equals(rule.TimeFrom) && i.TimeTill.Equals(rule.TimeTill);
                    bool triggersIdentical = CheckTriggerIdentity(rule.Triggers, i.Triggers);
                    if (actionIdentical && validityIdentical && triggersIdentical) { toRemove = i; } else { remaining.Add(i); }
                }

                Rules = remaining;
                if (toRemove != null) { dataLayer.RemoveBusinessRule(toRemove); }
                
            }
        }

        private static bool CheckTriggerIdentity(List<Trigger> first, List<Trigger> second)
        { 
            if (first.Count != second.Count) { return false; }
            for (int i=0; i<first.Count; i++) { if (!first[i].Source.Equals(second[i].Source) || !first[i].Attribute.Equals(second[i].Attribute) || !first[i].Threshold.Equals(second[i].Threshold) || !first[i].Comparator.Equals(second[i].Comparator)) { return false; } }
            return true;
        }
        #endregion
    }
}

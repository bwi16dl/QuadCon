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
    // This is a static class that acts as a "virtual database" for business rules
    // It keeps a list of all existing business rules (queried from DB), and adds up some helper methods to work with it
    // Note that this class is synchronised with DB state (i.e. if a rule is added, it is first added to this list, and then to DB)
    [DataContract]
    public static class BusinessRules
    {
        // List of all currently defined business rules
        [DataMember]
        public static List<Rule> Rules = new List<Rule>();

        #region INTERNAL
        // Database handler initialization
        private static DBHandler dataLayer = new DBHandler();

        // Getting business rules and starting the logic that evaluates conditions and runs the rules
        public static void Initialize()
        {
            Rules = dataLayer.GetBusinessRules();
            new Thread(StartComparing).Start();
        }

        // Execute business rules, once per minute (in background)
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
        // Methods exposed to client: getter to get all rules
        public static List<Rule> GetRules() { return Rules; }

        // Methods exposed to client: setter to add a new rule
        // Note: Rule ID is defined on DB level (auto-increment), and then returned here in the rule object
        public static void AddRule(Rule rule)
        {
            if (rule != null)
            {
                rule.Id = dataLayer.AddBusinessRule(rule);
                Rules.Add(rule);
            }
        }

        // Methods exposed to client: setter to drop an existing rule
        // Note: Rule ID is not propagated to client, therefore it is NOT a global identifier. Likewise, triggers do not have identifiers on object level at all
        // Consequently, there is a logic to identify resp. rule and trigger by comparing all available attributes
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

        // Helper method to check trigger identity by comparing all attributes
        private static bool CheckTriggerIdentity(List<Trigger> first, List<Trigger> second)
        { 
            if (first.Count != second.Count) { return false; }
            for (int i=0; i<first.Count; i++) { if (!first[i].Source.Equals(second[i].Source) || !first[i].Attribute.Equals(second[i].Attribute) || !first[i].Threshold.Equals(second[i].Threshold) || !first[i].Comparator.Equals(second[i].Comparator)) { return false; } }
            return true;
        }
        #endregion
    }
}

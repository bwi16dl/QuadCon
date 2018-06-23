using BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleObjects.DB
{
    public class DBHandler
    {
        public BusinessRulesModel db = new BusinessRulesModel();

        public int AddBusinessRule(Rule rule)
        {
            int exists = (from i in db.BusinessRules where i.Id == rule.Id select i).Count();
            if (exists > 0) { Console.WriteLine("Rule with ID = " + rule.Id + " already exists in DB"); return 0; }

            try
            {
                db.BusinessRules.Add(new BusinessRule() { Name = rule.Name, DayFrom = rule.DayFrom, DayTill = rule.DayTill, TimeFrom = rule.TimeFrom, TimeTill = rule.TimeTill, Link = rule.Link, Source = rule.Source, Action = rule.Action, Parameter = rule.Parameter });
                db.SaveChanges();
                int ruleId = (from i in db.BusinessRules select i.Id).Max(); 
                foreach (var i in rule.Triggers) { db.Triggers.Add(new Trigger() { RuleId = ruleId, Source = i.Source, Attribute = i.Attribute, Comparator = i.Comparator, Threshold = i.Threshold }); }
                db.SaveChanges();
                return ruleId;
            }
            catch (Exception e) { Console.WriteLine("Cannot add business rule or trigger: " + e); return 0; }
        }

        public bool RemoveBusinessRule(Rule rule)
        {
            BusinessRule toRemove;

            int exists = (from i in db.BusinessRules where i.Id == rule.Id select i).Count();
            if (exists <= 0) { Console.WriteLine("Rule with ID = " + rule.Id + " not found in DB"); return false; }
            else { toRemove = (from i in db.BusinessRules where i.Id == rule.Id select i).Single(); }

            try
            {
                db.BusinessRules.Remove(toRemove);
                var relevantTriggers = (from i in db.Triggers where i.RuleId == rule.Id select i);
                foreach (var i in relevantTriggers) { db.Triggers.Remove(i); }
                db.SaveChanges();
                return true;
            } catch (Exception e) { Console.WriteLine("Cannot remove busines rule or trigger: " + e); return false; }
        }

        public List<Rule> GetBusinessRules()
        {
            List<Rule> rules = new List<Rule>();

            var selectedRules = (from j in db.BusinessRules select j);
            foreach (var i in selectedRules)
            {
                Rule currentRule = new Rule(i.Name, i.DayFrom, i.DayTill, i.TimeFrom, i.TimeTill, i.Link, i.Source, i.Action, i.Parameter) { Id = i.Id };

                List<BusinessRules.Trigger> currentTriggers = new List<BusinessRules.Trigger>();
                var selectedTriggers = (from j in db.Triggers where j.RuleId == currentRule.Id select j);
                foreach (var j in selectedTriggers) { currentTriggers.Add(new BusinessRules.Trigger(j.Source, j.Attribute, j.Threshold, j.Comparator)); }

                currentRule.Triggers = currentTriggers;

                rules.Add(currentRule);
            }

            return rules;
        }
    }
}

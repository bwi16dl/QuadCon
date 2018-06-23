using DesktopClient.AdminService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Model
{
    public class BusinessRule
    {
        public Rule businessRule;

        public string NameString { get { return businessRule.Name; } }
        public string ActionString { get { return "Run " + businessRule.Action + "(\"" + businessRule.Parameter + "\")" + " on " + businessRule.Source; } }
        public string ValidityString { get { return businessRule.DayFrom + "-" + businessRule.DayTill + " from " + businessRule.TimeFrom + " till " + businessRule.TimeTill; } }
        public string LinkString
        {
            get
            {
                if (businessRule.Link != null && businessRule.Link.Equals("AND")) { return "All conditions listed below are executed:"; }
                else { return "Any condition listed below is executed:"; }
            }
        }
        
        public ObservableCollection<BaseTrigger> Triggers
        {
            get
            {
                ObservableCollection<BaseTrigger> triggers = new ObservableCollection<BaseTrigger>();
                foreach (var i in businessRule.Triggers) { triggers.Add(new BaseTrigger(i)); }
                return triggers;
            }
        }
        
        public BusinessRule(Rule rule) { businessRule = rule; }
    }
}

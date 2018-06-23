using DesktopClient.AdminService;
using DesktopClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModel
{
    public class AdminVM : ViewModelBase
    {
        private AdminServiceClient client = new AdminServiceClient();
        
        #region BUSINESS RULES
        private ObservableCollection<BusinessRule> businessRules = new ObservableCollection<BusinessRule>();
        public ObservableCollection<BusinessRule> BusinessRules
        {
            get { return businessRules; }
            set { businessRules = value; }
        }

        private BusinessRule selectedRule;
        public BusinessRule SelectedRule
        {
            get { return selectedRule; }
            set { selectedRule = value; RaisePropertyChanged(); }
        }

        private RelayCommand drop;
        public RelayCommand Drop
        {
            get { return drop; }
            set { drop = value; }
        }
        #endregion

        #region CONSTRUCTOR
        public RuleConstructor Constructor { get; set; }

        private RelayCommand create;
        public RelayCommand Create
        {
            get { return create; }
            set { create = value; }
        }

        private void CreateNewRule()
        {
            List<AdminService.Trigger> triggers = new List<AdminService.Trigger>();
            foreach (var i in Constructor.Triggers) { if (i.baseTrigger != null) { triggers.Add(i.baseTrigger); } }

            Rule created = new Rule()
            {
                Name = Constructor.Name,
                Triggers = triggers,
                DayFrom = Constructor.SelectedDayFrom,
                DayTill = Constructor.SelectedDayTill,
                TimeFrom = Constructor.TimeFrom,
                TimeTill = Constructor.TimeTill,
                Link = Constructor.SelectedLink,
                Source = Constructor.SelectedSource,
                Action = Constructor.SelectedAction,
                Parameter = Constructor.ActionParam
            };

            BusinessRules.Add(new BusinessRule(created));
            client.AddBusinessRule(created);

            Constructor.ClearContents();
        }
        #endregion

        public AdminVM()
        {
            Constructor = new RuleConstructor(client.GetExposedData());

            foreach (var i in client.GetBusinessRules()) { BusinessRules.Add(new BusinessRule(i)); }

            Drop = new RelayCommand(
                () => { client.RemoveBusinessRule(SelectedRule.businessRule); BusinessRules.Remove(SelectedRule); SelectedRule = null; },
                () => { return SelectedRule != null; });

            Create = new RelayCommand(CreateNewRule, Constructor.CanCreate);
        }
    }
}

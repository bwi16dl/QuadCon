using DesktopClient.AdminService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopClient.Model
{
    public class RuleConstructor : ViewModelBase
    {
        public List<AdminAttributes> exposedData;

        #region RULES
        public List<string> Days { get; set; }
        public List<string> Links { get; set; }
        public List<string> Sources { get; set; }
        public ObservableCollection<string> Actions { get; set; }

        private string name;
        public string Name { get { return name; } set { name = value; RaisePropertyChanged(); } }

        private string selectedDayFrom;
        public string SelectedDayFrom { get { return selectedDayFrom; } set { selectedDayFrom = value; RaisePropertyChanged(); } }

        private string selectedDayTill;
        public string SelectedDayTill { get { return selectedDayTill; } set { selectedDayTill = value; RaisePropertyChanged(); } }

        private string timeFrom;
        public string TimeFrom { get { return timeFrom; } set { if (IsEligible(value)) { timeFrom = value; RaisePropertyChanged(); } } }

        private string timeTill;
        public string TimeTill { get { return timeTill; } set { if (IsEligible(value)) { timeTill = value; RaisePropertyChanged(); } } }

        private string selectedLink;
        public string SelectedLink { get { return selectedLink; } set { selectedLink = value; UpdateLinkString(); RaisePropertyChanged(); } }

        private string linkString;
        public string LinkString { get { return linkString; } set { linkString = value; RaisePropertyChanged(); } }

        private string selectedSource;
        public string SelectedSource { get { return selectedSource; } set { selectedSource = value; UpdateActions(); RaisePropertyChanged(); } }

        private string selectedAction;
        public string SelectedAction { get { return selectedAction; } set { selectedAction = value; RaisePropertyChanged(); } }

        private string actionParam;
        public string ActionParam { get { return actionParam; } set { actionParam = value; RaisePropertyChanged(); } }

        private RelayCommand clear;
        public RelayCommand Clear { get { return clear; } set { clear = value; } }
        #endregion

        #region TRIGGERS
        public CurrentTrigger CurrentTrigger { get; set; }

        private ObservableCollection<BaseTrigger> triggers;
        public ObservableCollection<BaseTrigger> Triggers { get { return triggers; } set { triggers = value; } }
        #endregion

        #region LOGIC
        private bool IsEligible(string input)
        {
            if (input is null || Regex.IsMatch(input, "^[0-9]{2}:[0-9]{2}$")) { return true; }
            else { MessageBox.Show("Please stick to correct format (HH:MM)!"); return false; }
        }

        private void UpdateLinkString()
        {
            if (SelectedLink != null && SelectedLink.Equals("AND")) { LinkString = "All conditions listed below are executed:"; }
            else { LinkString = "Any condition listed below is executed:"; }
        }

        private void UpdateActions()
        {
            Actions.Clear();

            foreach(var i in exposedData)
            {
                if (i.Source.Equals(selectedSource)) 
                {
                    foreach (var j in i.Actions) { Actions.Add(j); }
                    break;
                }
            }
        }

        public bool CanCreate()
        {
            bool res = true;

            res &= SelectedSource != null;
            res &= SelectedAction != null;
            res &= SelectedDayFrom != null;
            res &= SelectedDayTill != null;
            res &= TimeFrom != null;
            res &= TimeTill != null;
            res &= Triggers.Count > 0 ? SelectedLink != null : true;

            return res;
        }

        public void ClearContents()
        {
            Name = null;

            SelectedSource = null;
            SelectedAction = null;
            ActionParam = null;

            SelectedDayFrom = null;
            SelectedDayTill = null;
            TimeFrom = null;
            TimeTill = null;

            SelectedLink = null;

            Triggers.Clear();
        }

        public void Add(BaseTrigger trigger) { if (!Triggers.Contains(trigger)) { Triggers.Add(trigger); } }

        public void Remove(BaseTrigger trigger) { if (Triggers.Contains(trigger)) { Triggers.Remove(trigger); } }
        #endregion

        public RuleConstructor(List<ExposedData> sourceData)
        {
            exposedData = new List<AdminAttributes>();
            foreach(var i in sourceData) { exposedData.Add(new AdminAttributes(i)); }

            Days = new List<string>() { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
            Links = new List<string>() { "AND", "OR" };

            Sources = new List<string>();
            foreach (var i in sourceData) { Sources.Add(i.Source); }
            
            Triggers = new ObservableCollection<BaseTrigger>();
            Actions = new ObservableCollection<string>();

            CurrentTrigger = new CurrentTrigger(exposedData, Add);

            Clear = new RelayCommand(ClearContents, () => { return true; });
        }
    }
}

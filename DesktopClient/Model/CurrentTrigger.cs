using DesktopClient.AdminService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopClient.Model
{
    public class CurrentTrigger : ViewModelBase
    {
        public List<AdminAttributes> exposedData;
        public Action<BaseTrigger> Adder;

        public List<string> TriggerSources { get; set; }
        public List<string> TriggerComparators { get; set; }
        public ObservableCollection<string> TriggerAttributes { get; set; }

        private string selectedTriggerSource;
        public string SelectedTriggerSource { get { return selectedTriggerSource; } set { selectedTriggerSource = value; UpdateTriggerAttributes(); RaisePropertyChanged(); } }

        private string selectedTriggerAttribute;
        public string SelectedTriggerAttribute { get { return selectedTriggerAttribute; } set { selectedTriggerAttribute = value; RaisePropertyChanged(); } }

        private string selectedTriggerComparator;
        public string SelectedTriggerComparator { get { return selectedTriggerComparator; } set { selectedTriggerComparator = value; RaisePropertyChanged(); } }

        private string triggerThreshold;
        public string TriggerThreshold { get { return triggerThreshold; } set { triggerThreshold = value; RaisePropertyChanged(); } }
        
        private RelayCommand add;
        public RelayCommand Add { get { return add; } set { add = value; } }

        private RelayCommand remove;
        public RelayCommand Remove { get { return remove; } set { remove = value; } }

        private void UpdateTriggerAttributes()
        {
            TriggerAttributes.Clear();

            foreach (var i in exposedData)
            {
                if (i.Source.Equals(selectedTriggerSource))
                {
                    foreach (var j in i.Attributes) { TriggerAttributes.Add(j); }
                    break;
                }
            }
        }

        private void Clear()
        {
            SelectedTriggerSource = null;
            SelectedTriggerAttribute = null;
            SelectedTriggerComparator = null;
            TriggerThreshold = null;
        }

        public CurrentTrigger(List<AdminAttributes> sourceData, Action<BaseTrigger> adder)
        {
            exposedData = sourceData;
            Adder = adder;

            TriggerComparators = new List<string>() { "EQUALS", "MORE", "LESS" };

            TriggerSources = new List<string>();
            foreach (var i in sourceData) { TriggerSources.Add(i.Source); }

            TriggerAttributes = new ObservableCollection<string>();

            Add = new RelayCommand(
                () =>
                {
                    Adder(new BaseTrigger(new AdminService.Trigger() { Source = SelectedTriggerSource, Attribute = SelectedTriggerAttribute, Comparator = SelectedTriggerComparator, Threshold = TriggerThreshold }));
                    Clear();
                },
                () => { return SelectedTriggerSource != null && SelectedTriggerAttribute != null && SelectedTriggerComparator != null && TriggerThreshold != null; });
        }
    }
}

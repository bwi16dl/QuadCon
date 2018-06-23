using DesktopClient.AdminService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Model
{
    public class AdminAttributes
    {
        public string Source;
        public ObservableCollection<string> Attributes { get; set; }
        public ObservableCollection<string> Actions { get; set; }

        public AdminAttributes(ExposedData input)
        {
            Source = input.Source;

            Attributes = new ObservableCollection<string>();
            Actions = new ObservableCollection<string>();
            if (input.Attributes != null) { foreach (var i in input.Attributes) { Attributes.Add(i); } }
            if (input.Actions != null) { foreach (var i in input.Actions) { Actions.Add(i); } }
        }
    }
}

using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Model
{
    public class BaseTrigger
    {
        public AdminService.Trigger baseTrigger;

        public string TriggerString
        {
            get
            {
                string comparator;
                if (baseTrigger.Comparator.Equals("EQUALS")) { comparator = " = "; }
                else if (baseTrigger.Comparator.Equals("MORE")) { comparator = " > "; }
                else { comparator = " < "; }

                return baseTrigger.Attribute + " in " + baseTrigger.Source + comparator + baseTrigger.Threshold;
            }
        }

        public BaseTrigger(AdminService.Trigger trigger)
        {
            baseTrigger = trigger;
        }
    }
}

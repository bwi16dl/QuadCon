using Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public enum Weekdays { MON = 1, TUE = 2, WED = 3, THU = 4, FRI = 5, SAT = 6, SUN = 7 };

    // A class representing business rule object
    // It has 2 logical parts: evaluation (time and trigger attributes), which defines if a rule has to execute; and execution, which defines what the rule should do
    // LIMITATIONS:
    // The rules do not have historization possibility (for example, it is not possible to create a rule that is triggered by 20% change in an attribute)
    // The rules cannot do any actions on their own; they can just run methods that were exposed to them by sources
    // The rules are utilising "simplified" mehtods which can only consume strings as parameters (due to reflections used to process them)
    [DataContract]
    public class Rule
    {
        // Attributes relevant to rules
        #region PROPERTIES
        // Identification and naming
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        // Time attributes (used to evaluate when a rule should run)
        [DataMember]
        public string DayFrom { get; set; }
        [DataMember]
        public string DayTill { get; set; }
        [DataMember]
        public string TimeFrom { get; set; }
        [DataMember]
        public string TimeTill { get; set; }

        // A list of triggers all (if link == AND) or some (if link == OR) of which must be evaluated to TRUE for the rule to execute
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public List<Trigger> Triggers { get; set; }

        // An action which has to be run on a source with parameter, in case if the rule is evaluated to TRUE
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string Parameter { get; set; }

        #endregion

        #region LOGIC
        // A method that evaluates if a rule can execute. Consists of 2 parts:
        // 1. Evaluates if current daytime is within defined interval for rule evaluation
        // 2. Goes through a list of triggers and evaluates all of them
        public bool Evaluate()
        {
            TimeSpan tfrom = new TimeSpan(Int32.Parse(TimeFrom.Split(':')[0]), Int32.Parse(TimeFrom.Split(':')[1]), 0);
            TimeSpan ttill = new TimeSpan(Int32.Parse(TimeTill.Split(':')[0]), Int32.Parse(TimeTill.Split(':')[1]), 0);
            TimeSpan tnow = DateTime.Now.TimeOfDay;

            Weekdays dfrom = (Weekdays)Enum.Parse(typeof(Weekdays), DayFrom);
            Weekdays dtill = (Weekdays)Enum.Parse(typeof(Weekdays), DayTill);
            Weekdays dnow = (Weekdays)Enum.Parse(typeof(Weekdays), DateTime.Now.DayOfWeek.ToString().ToUpper().Substring(0, 3));

            bool eval = (int)dnow >= (int)dfrom && (int)dnow <= (int)dtill && tnow >= tfrom && tnow <= ttill;
            if (!eval) { return false; }

            foreach (var i in Triggers)
            {
                if (Link.Equals("AND")) { eval &= i.Evaluate(); }
                else if (Link.Equals("OR")) { eval |= i.Evaluate(); }
                else { eval = false; }
            }

            return eval;
        }

        // A method that executes an action which is supposed to be done by a rule
        // It first finds relevant objects, and then runs an action on it by reflection
        public void Execute()
        {
            Object source;

            if (TestObject.Find(Source) != null) { source = TestObject.Find(Source); }
            else if (KodiObject.Find(Source) != null) { source = KodiObject.Find(Source); }
            else if (WeatherObject.Find(Source) != null) { source = WeatherObject.Find(Source); }
            else if (GenericInfoObject.Find(Source) != null) { source = GenericInfoObject.Find(Source); }
            else { Console.WriteLine("Source not found"); return; }

            if (Evaluate()) { source.GetType().GetMethod("A" + Action).Invoke(source, new string[] { Parameter }); }
        }
        #endregion

        // Just a simple constructor
        public Rule(string name, string dfrom, string dtill, string tfrom, string ttill, string link, string source, string action, string param)
        {
            Name = name;
            Triggers = new List<Trigger>();

            DayFrom = dfrom;
            DayTill = dtill;
            TimeFrom = tfrom;
            TimeTill = ttill;

            Link = link;
            Source = source;
            Action = action;
            Parameter = param;
        }
    }
}

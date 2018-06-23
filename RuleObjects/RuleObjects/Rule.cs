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

    [DataContract]
    public class Rule
    {
        #region PROPERTIES
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DayFrom { get; set; }
        [DataMember]
        public string DayTill { get; set; }
        [DataMember]
        public string TimeFrom { get; set; }
        [DataMember]
        public string TimeTill { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string Parameter { get; set; }
        [DataMember]
        public List<Trigger> Triggers { get; set; }
        #endregion

        #region LOGIC
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

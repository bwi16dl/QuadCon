using Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace BusinessRules
{
    public enum Comparators { EQUALS, MORE, LESS }

    [DataContract]
    public class Trigger
    {
        #region INTERFACE
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Attribute { get; set; }
        [DataMember]
        public string Threshold { get; set; }
        [DataMember]
        public string Comparator { get; set; }
        #endregion

        #region INTERNAL
        public bool Evaluate()
        {
            Object source;

            if (TestObject.Find(Source) != null) { source = TestObject.Find(Source); }
            else if (KodiObject.Find(Source) != null) { source = KodiObject.Find(Source); }
            else if (WeatherObject.Find(Source) != null) { source = WeatherObject.Find(Source); }
            else if (GenericInfoObject.Find(Source) != null) { source = GenericInfoObject.Find(Source); }
            else { Console.WriteLine("Source not found"); return false; }

            string attribute = source.GetType().GetMethod("AGet" + Attribute).Invoke(source, null).ToString();
            Comparators comparator = (Comparators)Enum.Parse(typeof(Comparators), Comparator.ToUpper());

            if (comparator.Equals(Comparators.EQUALS)) { return attribute.Equals(Threshold); }
            else if (comparator.Equals(Comparators.MORE)) { try { return Double.Parse(attribute) > Double.Parse(Threshold); } catch (Exception) { Console.WriteLine("Cannot cast inputs"); return false; } }
            else if (comparator.Equals(Comparators.LESS)) { try { return Double.Parse(attribute) < Double.Parse(Threshold); } catch (Exception) { Console.WriteLine("Cannot cast inputs"); return false; } }
            else { return false; }
        }
        #endregion

        public Trigger(string source, string attribute, string threshold, string comparator)
        {
            Source = source;
            Attribute = attribute;
            Threshold = threshold;
            Comparator = comparator;
        }
    }
}

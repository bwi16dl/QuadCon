﻿using Objects;
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

    // A class that represents rule trigger
    // A trigger is an attribute in a source that can be >, < or == threshold (defined by user in Admin section of a client)
    // A trigger has dedicated table in DB (n-to-1 relation to business rules)
    [DataContract]
    public class Trigger
    {
        // Main trigger attributes (comparator represented by enum)
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
        // A method that evaluates trigger to TRUE or FALSE. This method is used by business rule for all its triggers to check its overall status (true or false)
        // It (1) finds relevant source object, (2) gets attribute value (by reflection), and (3) evaluates it by applying resp. comparator
        // Note that only numeric attributes can be evaluated with more or less, strings are only checked for equality
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

        // Just a small constructor to ease up subsequent implementation
        public Trigger(string source, string attribute, string threshold, string comparator)
        {
            Source = source;
            Attribute = attribute;
            Threshold = threshold;
            Comparator = comparator;
        }
    }
}

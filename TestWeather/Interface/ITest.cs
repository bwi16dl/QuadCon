using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    // Demo implementation of a source, to be removed after implementation is complete
    [InheritedExport(typeof(ITest))]
    public interface ITest
    {
        // Unique ID of test source (in case if more than 1 source available, they will be distinguished by this field)
        string GetName();

        // Getters to demonstrate admin functionality (bound to OneDayForecast)
        double AGetCurrentTemperature();
        string AGetCurrentWind();

        // Getters to demonstrate client-server bindings
        TestData GetOneDay();
        List<TestData> GetThreeDays();

        // Setters to demonstrate client-server bindings:
        // SetName is not supposed to be used by client, but is exposed to server
        // SetOneDay is added to demonstrate possibility to change custom data object
        // SetThreeDays was added for historical reasons (currently unused)
        void SetName(string name);
        void ASetOneDay(TestData oneDay);
        void SetThreeDays(List<TestData> threeDays);
        
        // A method to print string provided by a client on a server
        void ATrigger(string printWhat);
    }
}

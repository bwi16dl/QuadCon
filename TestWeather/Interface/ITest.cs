using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [InheritedExport(typeof(ITest))]
    public interface ITest
    {
        string GetName();

        int AGetCurrentTemperature();
        string AGetCurrentWind();
        TestData GetOneDay();
        List<TestData> GetThreeDays();

        void SetName(string name);
        void ASetOneDay(TestData oneDay);
        void SetThreeDays(List<TestData> threeDays);
        
        void ATrigger(string printWhat);
    }
}

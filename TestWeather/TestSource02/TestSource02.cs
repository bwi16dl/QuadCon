using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class TestClass02 : ITest
    {
        #region FIELDS
        private string name;
        private TestData oneDay;
        private List<TestData> threeDays;
        #endregion

        #region GETTERS
        public string GetName() { return name; }
        public int AGetCurrentTemperature() { return 10; }
        public string AGetCurrentWind() { return "Weak"; }
        public TestData GetOneDay() { return oneDay; }
        public List<TestData> GetThreeDays() { return threeDays; }
        #endregion

        #region SETTERS
        public void SetName(string name) { this.name = name; }
        public void ASetOneDay(TestData oneDay) { this.oneDay = oneDay; }
        public void SetThreeDays(List<TestData> threeDays)
        {
            List<TestData> list = new List<TestData>();
            foreach (var i in threeDays) { list.Add(i); }
            this.threeDays = list;
        }
        #endregion

        #region ACTIONS
        public void ATrigger(string printWhat) { Console.WriteLine(printWhat); }
        #endregion

        public TestClass02()
        {
            SetName("Test Weather Source 02");
            ASetOneDay(new TestData() { Temperature = 12.5, Wind="Strong" });
            SetThreeDays(new List<TestData>());
            
        }
    }
}

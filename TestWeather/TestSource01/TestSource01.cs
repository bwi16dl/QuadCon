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
    public class TestSource01 : ITest
    {
        #region FIELDS
        private string name;
        private int currentTemperature;
        private string currentWind;
        private TestData oneDay;
        private List<TestData> threeDays;
        #endregion

        #region GETTERS
        public string GetName() { return name; }
        public int AGetCurrentTemperature() { return currentTemperature; }
        public string AGetCurrentWind() { return currentWind; }
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

        private void StartUpdating()
        {
            Random random = new Random();

            TestData[] forecasts = new TestData[]
            {
                new TestData() {Wind="updated wind 01", Temperature=100},
                new TestData() {Wind="updated wind 02", Temperature=200},
                new TestData() {Wind="updated wind 03", Temperature=300},
                new TestData() {Wind="updated wind 04", Temperature=400},
                new TestData() {Wind="updated wind 05", Temperature=500},
                new TestData() {Wind="updated wind 06", Temperature=600}
            };

            while (true)
            {
                SetThreeDays(new List<TestData>()
                {
                    forecasts[random.Next(0,5)],
                    forecasts[random.Next(0,5)],
                    forecasts[random.Next(0,5)]
                });

                Thread.Sleep(10000);
            }
        }

        public TestSource01()
        {
            SetName("Test Weather Source 01");

            currentTemperature = 20;
            currentWind = "Strong";

            ASetOneDay(new TestData() { Temperature = 12.5, Wind="Strong" });
            SetThreeDays(new List<TestData>());

            new Thread(StartUpdating).Start();
        }
    }
}

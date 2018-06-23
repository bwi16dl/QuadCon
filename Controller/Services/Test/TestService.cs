using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Test;

namespace Controller.Services
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class TestService : ITestService
    {
        public string GetName(string sourceName) { return TestObject.Find(sourceName).GetName(); }
        public TestData GetOneDay(string sourceName) { return TestObject.Find(sourceName).GetOneDay(); }
        public List<TestData> GetThreeDays(string sourceName) { return TestObject.Find(sourceName).GetThreeDays(); }

        public void SetName(string sourceName, string name) { TestObject.Find(sourceName).SetName(name); }
        public void SetOneDay(string sourceName, TestData oneDay) { TestObject.Find(sourceName).ASetOneDay(oneDay); }
        public void SetThreeDays(string sourceName, List<TestData> threeDays) { TestObject.Find(sourceName).SetThreeDays(threeDays); }

        public void Trigger(string sourceName, string printWhat) { TestObject.Find(sourceName).ATrigger(printWhat); }
    }
}

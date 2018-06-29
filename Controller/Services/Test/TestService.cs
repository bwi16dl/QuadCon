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
    // Straightforward implementation of ITestService
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class TestService : ITestService
    {
        // The basic logic utilises helper method Find() to find relevant object (by name) and then runs respective method on it
        // NOTE that other web services have comparable implementation and, therefore, detailed explanation is only provided here

        public string GetName(string sourceName) { return TestObject.Find(sourceName).GetName(); }
        public TestData GetOneDay(string sourceName) { return TestObject.Find(sourceName).GetOneDay(); }
        public List<TestData> GetThreeDays(string sourceName) { return TestObject.Find(sourceName).GetThreeDays(); }

        public void SetName(string sourceName, string name) { TestObject.Find(sourceName).SetName(name); }
        public void SetOneDay(string sourceName, TestData oneDay) { TestObject.Find(sourceName).ASetOneDay(oneDay); }
        public void SetThreeDays(string sourceName, List<TestData> threeDays) { TestObject.Find(sourceName).SetThreeDays(threeDays); }

        public void Trigger(string sourceName, string printWhat) { TestObject.Find(sourceName).ATrigger(printWhat); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Test;

namespace Controller.Services
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetName(string sourceName);
        [OperationContract]
        TestData GetOneDay(string sourceName);
        [OperationContract]
        List<TestData> GetThreeDays(string sourceName);

        [OperationContract]
        void SetName(string sourceName, string name);
        [OperationContract]
        void SetOneDay(string sourceName, TestData oneDay);
        [OperationContract]
        void SetThreeDays(string sourceName, List<TestData> threeDays);

        [OperationContract]
        void Trigger(string sourceName, string printWhat);
    }
}

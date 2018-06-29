using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Test;

namespace Controller.Services
{
    // Web service which just exposes all the methods visible to the server (defined in respective source interface)
    // NOTE that implementation of other web services is identical and, therefore, is fully commented only here for ITest
    [ServiceContract]
    public interface ITestService
    {
        // GetName is actually unused at the moment, but is kept here to enable client to display more than one source
        // Note that this is currently not implemented on client side due to resource shortage, but the infrastructure is provided to make the extension easy

        // All the other methods are the same as defined in ITest (so, essentially, the server is acting as a "tube" to directly connect sources and clients). The decision was to do it this way, due to organisational constraints:
        // The server was done by one developer in the beginning of the project, while sources and respective client parts were implemented by different devs later,
        // meaning that server should have provided as much flexibility and as little necessity to change something, as possible
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

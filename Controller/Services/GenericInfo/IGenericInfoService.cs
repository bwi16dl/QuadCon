using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGenericInfo" in both code and config file together.
    [ServiceContract]
    public interface IGenericInfoService
    {
        [OperationContract]
        string GetName(string sourceName);

        [OperationContract]
        GenericInfo.Data.GenericInfo GetInfo(string sourceName);

        [OperationContract]
        void SetName(string sourceName, string name);

        [OperationContract]
        void SetInfo(string sourceName, GenericInfo.Data.GenericInfo info);
        [OperationContract]
        string GetRonsQuote(string sourceName);
        [OperationContract]
        bool GetIsUp(string sourceName);

    }
}

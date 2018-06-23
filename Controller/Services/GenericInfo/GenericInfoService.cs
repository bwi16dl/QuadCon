using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GenericInfo.Data;
using Objects;

namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GenericInfo" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class GenericInfoService : IGenericInfoService
    {
        public GenericInfo.Data.GenericInfo GetInfo(string sourceName)
        {
            return GenericInfoObject.Find(sourceName).GetInfo();
        }

        public bool GetIsUp(string sourceName)
        {
            return GenericInfoObject.Find(sourceName).Isup();
        }

        public string GetName(string sourceName)
        {
            return GenericInfoObject.Find(sourceName).GetName();
        }

        public string GetRonsQuote(string sourceName)
        {
            return GenericInfoObject.Find(sourceName).GetRonsQuote();
        }

        public void SetInfo(string SourceName, GenericInfo.Data.GenericInfo info)
        {
            GenericInfoObject.Find(SourceName).SetInfo(info);
        }

        public void SetName(string sourceName, string name)
        {
            GenericInfoObject.Find(sourceName).SetName(name);
        }
    }
}

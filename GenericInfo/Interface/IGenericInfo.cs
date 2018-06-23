using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericInfo
{
    [InheritedExport(typeof(IGenericInfo))]
    public interface IGenericInfo
    {
        string GetName();
        Data.GenericInfo GetInfo();
        void SetName(string name);
        void SetInfo(Data.GenericInfo info);
        string GetRonsQuote();

        bool Isup();
    }
}

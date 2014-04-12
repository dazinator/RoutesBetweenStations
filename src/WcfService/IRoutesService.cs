using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    [ServiceContract]
    public interface IRoutesService
    {
        [OperationContract]
        [WebInvoke()]
        void FindRoute(string from, string to);

        [OperationContract]
        List<string> GetStations();
    }

}

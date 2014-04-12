using System.Collections.Generic;
using System.ServiceModel;
using RoutesBetweenStations.Website.Dto;

namespace RoutesBetweenStations.Website.Wcf
{
    [ServiceContract()]
    public interface IRoutesService
    {
        [OperationContract]
        List<string> GetStations();

        [OperationContract]
        RouteInfo FindRoute(string fromStation, string toStation);
    }
}
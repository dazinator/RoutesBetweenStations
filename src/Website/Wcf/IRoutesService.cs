using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using RoutesBetweenStations.Website.Dto;

namespace RoutesBetweenStations.Website.Wcf
{
    [ServiceContract()]
    public interface IRoutesService
    {
        [OperationContract]
        [WebGet(UriTemplate = "getstations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetStations();

        [OperationContract]
        [WebGet(UriTemplate = "findroute?fromStation={fromStation}&toStation={toStation}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        RouteInfo FindRoute(string fromStation, string toStation);
    }
}
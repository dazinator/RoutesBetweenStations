using System.Collections.Generic;

namespace RoutesBetweenStations.Model.Provider
{
    /// <summary>
    /// The contract for any component that can provide the routes between 2 <see cref="Node"/>s. 
    /// </summary>
    public interface IRouteProvider
    {
        Route FindShortestRoute(List<Node> stations, Node fromNode, Node toNode);
    }
}
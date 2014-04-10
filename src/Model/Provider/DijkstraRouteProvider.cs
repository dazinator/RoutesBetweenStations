using System;
using System.Collections.Generic;

namespace RoutesBetweenStations.Model.Provider
{
    /// <summary>
    /// An implementation of <see cref="IRouteProvider"/> that uses Dijkstra's algorthimn to find routes.
    /// </summary>
    public class DijkstraRouteProvider : IRouteProvider
    {
        public IEnumerable<Route> FindRoutes(List<Node> stations, Node fromNode, Node toNode)
        {
            throw new NotImplementedException();
        }
    }
}
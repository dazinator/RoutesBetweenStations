using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using RoutesBetweenStations.DataAccess;
using RoutesBetweenStations.Model;
using RoutesBetweenStations.Model.Provider;
using RoutesBetweenStations.Website.Dto;

namespace RoutesBetweenStations.Website.Wcf
{

    /// <summary>
    /// An implementation of <see cref="IRoutesService"/> that will provide the AJAX operations for the routes website.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RoutesService : IRoutesService
    {
        private INodesRepository _NodesRepository;
        private IRouteProvider _RouteProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nodesRepository"></param>
        /// <param name="routeProvider"></param>
        public RoutesService(INodesRepository nodesRepository, IRouteProvider routeProvider)
        {
            if (nodesRepository == null)
            {
                throw new ArgumentNullException("nodesRepository");
            }
            if (routeProvider == null)
            {
                throw new ArgumentNullException("routeProvider");
            }
            _NodesRepository = nodesRepository;
            _RouteProvider = routeProvider;
        }

        /// <summary>
        /// Gets the list of station names.
        /// </summary>
        /// <returns></returns>
        public List<string> GetStations()
        {
            //NB: Could cache nodes list for better perfomance.
            var stations = _NodesRepository.GetNodes();
            var stationNames = stations.Select(s => s.Name);
            return stationNames.ToList();
        }

        /// <summary>
        /// Gets the <see cref="RouteInfo"/> that is the quickest route between the two stations named.
        /// </summary>
        /// <param name="fromStation">The name of the station to commence the route from.</param>
        /// <param name="toStation">The name of the station that is the final destination of the route.</param>
        /// <returns></returns>
        public RouteInfo FindRoute(string fromStation, string toStation)
        {
            var nodes = _NodesRepository.GetNodes();
            var startingNode = GetNodeOrThrow(fromStation, nodes);
            var finishingNode = GetNodeOrThrow(toStation, nodes);
            var route = _RouteProvider.FindShortestRoute(nodes, startingNode, finishingNode);
            var journey = new RouteInfo(route);
            return journey;
        }

        /// <summary>
        /// Obtains the <see cref="Node"/> in the list with the specified name, or throws a suitable exception if it cannot be found.
        /// </summary>
        /// <param name="name">The name of the <see cref="Node"/> (case insensitve)</param>
        /// <param name="nodes">The list of <see cref="Node"/>s to search.</param>
        /// <returns>The <see cref="Node"/> with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If the name is null or whitepeace, or if the node with a matching name cannot be found.</exception>
        private Node GetNodeOrThrow(string name, IEnumerable<Node> nodes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name of the node must be specified.");
            }

            var node = nodes.FirstOrDefault(s => s.Name.ToLowerInvariant() == name.ToLowerInvariant());
            if (node == null)
            {
                throw new ArgumentException(string.Format("Invalid node name: {0}", name));
            }
            return node;
        }

    }
}

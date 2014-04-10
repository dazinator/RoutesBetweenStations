using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutesBetweenStations.Model.Provider
{
    /// <summary>
    /// An implementation of <see cref="IRouteProvider"/> that uses Dijkstra's algorthim to find routes.
    /// </summary>
    public class DijkstraRouteProvider : IRouteProvider
    {
        /// <summary>
        /// Stores each node along with its Dijkstra algorithmn specific data which is used during route calculation.
        /// </summary>
        private Dictionary<Node, DijkstraSearchData> _DijxtraNodes = new Dictionary<Node, DijkstraSearchData>();

        /// <summary>
        /// Returns the shortest route between 2 nodes using Dijkstra's algorthim.
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public Route FindShortestRoute(List<Node> stations, Node fromNode, Node toNode)
        {
            // Assign to every node a tentative "distance" value: set it to zero for our initial node and to max long for other nodes.
            // Mark all nodes unvisited.
            var otherNodes = stations.Where(s => !s.Equals(fromNode));
            foreach (var otherNode in otherNodes)
            {
                _DijxtraNodes[otherNode] = new DijkstraSearchData() { DijkstraDistance = long.MaxValue, Visited = false };
            }
            _DijxtraNodes[fromNode] = new DijkstraSearchData() { DijkstraDistance = 0, Visited = false };

            //Set the initial node as current. 
            var current = fromNode;
            while (current != null)
            {
               // For the current node, consider all of its unvisited neighbors
                var unvisitedNeighbours = current.Connections.Where(c => !_DijxtraNodes[c.ConnectedToNode].Visited);

                //.. and calculate their tentative distances. 
                // Compare the newly calculated tentative distance to the current assigned value and assign the smaller one. 
                var currentDijkstraData = _DijxtraNodes[current];
                var currentDistance = currentDijkstraData.DijkstraDistance;
                
                // For example, if the current node A is marked with a distance of 6, 
                // and the edge connecting it with a neighbor B has length 2, 
                // then the distance to B (through A) will be 6 + 2 = 8. 
                // If B was previously marked with a distance greater than 8 then change it to 8. Otherwise, keep the current value.

                foreach (var unvisitedNeighbour in unvisitedNeighbours)
                {
                    var edgeLength = unvisitedNeighbour.TravelTime.Ticks;
                    var neighbourTentativeDistance = currentDistance + edgeLength;
                    var neighbourDijxtraData = _DijxtraNodes[unvisitedNeighbour.ConnectedToNode];
                    if (neighbourTentativeDistance < neighbourDijxtraData.DijkstraDistance)
                    {
                        neighbourDijxtraData.DijkstraDistance = neighbourTentativeDistance;
                        // Store the current node that leads to this node, this enables us to reverse navigate the route later..
                        neighbourDijxtraData.PreviousConnection = unvisitedNeighbour;
                    }
                }

                // When we are done considering all of the neighbors of the current node, mark the current node as visited and remove it from the unvisited set.
                // A visited node will never be checked again.
                currentDijkstraData.Visited = true;

                // If the destination node has been marked visited (when planning a route between two specific nodes) -- The algorithm has finished.
                if (_DijxtraNodes[toNode].Visited)
                {
                    break;
                }

                //If the smallest tentative distance among the nodes in the unvisited set is max long (when planning a complete traversal; 
                // occurs when there is no connection between the initial node and remaining unvisited nodes), -- The algorithm has finished. 
                var minDistance = _DijxtraNodes.Min(s => s.Value.DijkstraDistance);
                if (minDistance == long.MaxValue)
                {
                    break;
                }

                //Select the unvisited node that is marked with the smallest tentative distance, and set it as the new "current node" then repeat.
                current =
                    _DijxtraNodes.Where(d => !d.Value.Visited)
                                 .OrderBy(d => d.Value.DijkstraDistance)
                                 .Select(d => d.Key)
                                 .FirstOrDefault();
            }

            // walk back through the path. starting from the the destination to reconstruct the route.
            var route = new Route();
            var destination = _DijxtraNodes[toNode];
            while (destination.PreviousConnection != null)
            {
                route.Connections.Add(destination.PreviousConnection);
                destination = _DijxtraNodes[destination.PreviousConnection.ConnectedFromNode];
            }
            return route;
        }

        /// <summary>
        /// Private nested class, used to attach Dijkstra algorithm specific data with nodes during route calculation.
        /// </summary>
        private class DijkstraSearchData
        {
            public long DijkstraDistance { get; set; }
            public bool Visited { get; set; }
            public NodeConnection PreviousConnection { get; set; }
        }

    }
}
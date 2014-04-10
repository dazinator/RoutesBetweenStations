using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutesBetweenStations.Model
{
    /// <summary>
    /// Reprsents a route between one node and another.
    /// </summary>
    public class Route
    {
        public Route()
        {
            Connections = new List<NodeConnection>();
        }
        public TimeSpan TotalTravelTime()
        {
            return new TimeSpan(Connections.Sum(r => r.TravelTime.Ticks));
        }
        public IList<NodeConnection> Connections { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;
using RoutesBetweenStations.Model;

namespace RoutesBetweenStations.Website.Dto
{
    /// <summary>
    /// A DTO for transferal of information pertaining to the route between a starting node and an ending node.
    /// </summary>
    [DataContract]
    public class RouteInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="route"></param>
        public RouteInfo(Route route)
        {
            this.Connections = new List<ConnectionInfo>();
            if (route != null)
            {
                this.TotalTravelTimeInMinutes = route.TotalTravelTime().TotalMinutes;
                // do a reverse for loop so that the connections are listed in order of start to end.
                for (int i = route.Connections.Count; i-- > 0; )
                {
                    this.Connections.Add(new ConnectionInfo(route.Connections[i]));
                }
            }
        }

        [DataMember()]
        public double TotalTravelTimeInMinutes { get; set; }

        /// <summary>
        /// Each connection in the connections list encapsulates an individual part of the journey.
        /// </summary>
        [DataMember()]
        public List<ConnectionInfo> Connections { get; set; }

    }
}
using System.Runtime.Serialization;
using RoutesBetweenStations.Model;

namespace RoutesBetweenStations.Website.Dto
{
    /// <summary>
    /// The connection info is a DTO containing information relating to travel between two stations. Connections constitue part of an overal journey.
    /// </summary>
    [DataContract]
    public class ConnectionInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connection"></param>
        public ConnectionInfo(NodeConnection connection)
        {
            this.FromStation = connection.ConnectedFromNode.Name;
            this.ToStation = connection.ConnectedToNode.Name;
            this.TravelTimeInMinutes = connection.TravelTime.TotalMinutes;
        }

        [DataMember()]
        public string FromStation { get; set; }

        [DataMember()]
        public string ToStation { get; set; }

        [DataMember()]
        public double TravelTimeInMinutes { get; set; }
    }
}
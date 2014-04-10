namespace RoutesBetweenStations.Model.Factory
{
    /// <summary>
    /// An implementation of <see cref="NodeConnectionFactory"/> that creates <see cref="TrainConnection"/>'s.
    /// </summary>
    public class TrainConnectionFactory : NodeConnectionFactory
    {
        /// <summary>
        /// Creates an instance of a NodeConnection.
        /// </summary>
        /// <param name="fromNode">The node that is being connected from / starting node.</param>
        /// <param name="toNode">The node that is being connected to / destination node.</param>
        /// <param name="journeyTimeInMinutes">The number of minutes that travel takes, from the starting node to the destination node.</param>
        /// <returns>A <see cref="TrainConnection"/> which is a subclass of <see cref="NodeConnection"/></returns>
        public override NodeConnection CreateConnection(Node fromNode, Node toNode, int journeyTimeInMinutes)
        {
            return new TrainConnection(fromNode, toNode, journeyTimeInMinutes);
        }

    }
}
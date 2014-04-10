namespace RoutesBetweenStations.Model.Factory
{
    /// <summary>
    /// An abstract base class implementation of <see cref="INodeConnectionFactory"/>.
    /// </summary>
    public abstract class NodeConnectionFactory : INodeConnectionFactory
    {
        /// <summary>
        /// Creates an instance of a NodeConnection.
        /// </summary>
        /// <param name="fromNode">The node that is being connected from / starting node.</param>
        /// <param name="toNode">The node that is being connected to / destination node.</param>
        /// <param name="journeyTimeInMinutes">The number of minutes that travel takes, from the starting node to the destination node.</param>
        /// <returns>The instance of the <see cref="NodeConnection"/></returns>
        public abstract NodeConnection CreateConnection(Node fromNode, Node toNode, int journeyTimeInMinutes);

    }
}
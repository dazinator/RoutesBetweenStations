namespace RoutesBetweenStations.Model.Node
{
    /// <summary>
    /// This interface defines the contract for any factory that creates instances of <see cref="NodeConnection"/>.
    /// </summary>
    public interface INodeConnectionFactory
    {
        /// <summary>
        /// Creates an instance of a NodeConnection.
        /// </summary>
        /// <param name="fromNode">The node that is being connected from / starting node.</param>
        /// <param name="toNode">The node that is being connected to / destination node.</param>
        /// <param name="journeyTimeInMinutes">The number of minutes that travel takes, from the starting node to the destination node.</param>
        /// <returns>The instance of the <see cref="NodeConnection"/></returns>
        NodeConnection CreateConnection(Node fromNode, Node toNode, int journeyTimeInMinutes);
    }
}
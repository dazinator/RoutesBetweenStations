using System.Collections.Generic;

namespace RoutesBetweenStations.Model.Node
{
    /// <summary>
    /// An abtract class representing a Node in the network.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected Node()
        {
            this.Connections = new List<NodeConnection>();
        }

        /// <summary>
        /// The name of this node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The connections from this node to other nodes.
        /// </summary>
        public List<NodeConnection> Connections { get; set; }

    }
}
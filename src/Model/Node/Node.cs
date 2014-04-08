using System.Collections.Generic;
using System.Diagnostics;

namespace RoutesBetweenStations.Model.Node
{
    /// <summary>
    /// An abtract class representing a Node in the network.
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
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
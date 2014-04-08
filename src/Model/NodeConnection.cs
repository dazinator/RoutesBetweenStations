using System;

namespace Model.Core
{
    /// <summary>
    /// An abstract class representing a connection between two nodes based on a mode of transport.
    /// </summary>
    public abstract class NodeConnection
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="journeyTimeInMinutes"></param>
        protected NodeConnection(Node fromNode, Node toNode, int journeyTimeInMinutes)
        {
            if (fromNode == null)
            {
                throw new ArgumentNullException("fromNode");
            }
            if (toNode == null)
            {
                throw new ArgumentNullException("toNode");
            }
            this.ConnectedFromNode = fromNode;
            this.ConnectedToNode = toNode;
            this.TravelTime = TimeSpan.FromMinutes(journeyTimeInMinutes);
        }

        /// <summary>
        /// The name given to the mode of transport that the connection represents.
        /// </summary>
        public abstract string ModeOfTransportName { get; }

        /// <summary>
        /// The Node that this connects from.
        /// </summary>
        public Node ConnectedFromNode { get; set; }

        /// <summary>
        /// The Node that this connects to.
        /// </summary>
        public Node ConnectedToNode { get; set; }

        /// <summary>
        /// The travel time between the nodes via this connection.
        /// </summary>
        public TimeSpan TravelTime { get; set; }

    }
}
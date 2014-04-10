using System;
using RoutesBetweenStations.Model.Node;

namespace RoutesBetweenStations.Model.Train
{
    /// <summary>
    /// An implementation of <see cref="NodeConnection"/> that represents a connection by "Rail" mode of transport between two nodes.
    /// </summary>
    public class TrainConnection : NodeConnection
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="journeyTimeInMinutes"></param>
        public TrainConnection(Node.Node fromNode, Node.Node toNode, int journeyTimeInMinutes)
            : base(fromNode, toNode, journeyTimeInMinutes)
        {
            if (fromNode.Equals(toNode))
            {
                throw new ArgumentException("You cannot connect a node to itself.");
            }
        }

        public override string ModeOfTransportName
        {
            get { return "Train"; }
        }
    }
}
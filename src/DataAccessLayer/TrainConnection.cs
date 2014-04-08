using Model.Core;

namespace RoutesBetweenStations.DataAccessLayer.Tests
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
        public TrainConnection(Node fromNode, Node toNode, int journeyTimeInMinutes) : base(fromNode, toNode, journeyTimeInMinutes)
        {
        }

        public override string ModeOfTransportName
        {
            get { return "Train"; }
        }
    }
}
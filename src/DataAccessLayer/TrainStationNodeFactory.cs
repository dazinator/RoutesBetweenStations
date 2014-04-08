using Model.Core;

namespace RoutesBetweenStations.DataAccessLayer.Tests
{
    /// <summary>
    /// An implementation of an <see cref="NodeFactory"/> that will create only <see cref="TrainStationNode"/>'s.
    /// </summary>
    public class TrainStationNodeFactory : NodeFactory
    {
        public override Node CreateNode(string name)
        {
            return new TrainStationNode(name);
        }
    }
}
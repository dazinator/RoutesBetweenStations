using RoutesBetweenStations.Model.Node;

namespace RoutesBetweenStations.Model.Train
{
    /// <summary>
    /// An implementation of an <see cref="NodeFactory"/> that will create only <see cref="TrainStation"/>'s.
    /// </summary>
    public class TrainStationFactory : NodeFactory
    {
        public override Node.Node CreateNode(string name)
        {
            return new TrainStation(name);
        }
    }
}
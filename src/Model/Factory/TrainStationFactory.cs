namespace RoutesBetweenStations.Model.Factory
{
    /// <summary>
    /// An implementation of an <see cref="NodeFactory"/> that will create only <see cref="TrainStation"/>'s.
    /// </summary>
    public class TrainStationFactory : NodeFactory
    {
        public override Node CreateNode(string name)
        {
            return new TrainStation(name);
        }
    }
}
using RoutesBetweenStations.Model.Node;

namespace RoutesBetweenStations.Model.Train
{
    /// <summary>
    /// An implementation of <see cref="Node"/> that represents a Train Station.
    /// </summary>
    public class TrainStationNode : Node.Node
    {
        public TrainStationNode(string name)
        {
            this.Name = name;
        }

        
    }
}
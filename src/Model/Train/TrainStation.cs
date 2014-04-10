using RoutesBetweenStations.Model.Node;

namespace RoutesBetweenStations.Model.Train
{
    /// <summary>
    /// An implementation of <see cref="Node"/> that represents a Train Station.
    /// </summary>
    public class TrainStation : Node.Node
    {
        public TrainStation(string name)
        {
            this.Name = name;
        }

        
    }
}
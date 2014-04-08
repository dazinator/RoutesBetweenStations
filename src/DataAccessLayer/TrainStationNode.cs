using Model.Core;

namespace RoutesBetweenStations.DataAccessLayer.Tests
{
    /// <summary>
    /// An implementation of <see cref="Node"/> that represents a Train Station.
    /// </summary>
    public class TrainStationNode : Node
    {
        public TrainStationNode(string name)
        {
            this.Name = name;
        }

        
    }
}
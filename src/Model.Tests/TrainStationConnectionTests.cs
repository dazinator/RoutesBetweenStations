using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.Model.Train;

namespace Model.Tests
{
    [TestFixture()]
    public class TrainStationConnectionTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [Test(Description = "Cannot connect a TrainStationNode with itself.")]
        public void Cannot_Connect_A_TrainStationNode_With_Itself()
        {
            // Arrange
            var newNode = new TrainStationNode("paddington");
            newNode.Connections.Add(new TrainConnection(newNode, newNode, 10));
        }

    }
}

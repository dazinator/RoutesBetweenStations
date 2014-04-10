using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.Model.Train;

namespace Model.Tests
{
    [TestFixture()]
    public class TrainStationNodeTests
    {
        [Test(Description = "Can create an instance of a TrainStationNode")]
        public void Can_Create_New_TrainStationNode()
        {
            // Arrange
            var name = "paddington";
            var newNode = new TrainStationNode(name);
            Assert.That(newNode.Name, Is.EqualTo(name));
            Assert.That(newNode.Connections, Is.Not.Null);
        }


        [Test(Description = "Can add a TrainConnection to the Connections List")]
        public void Can_Add_A_Connection_To_Another_Node()
        {
            // Arrange
            var firstNode = new TrainStationNode("paddington");
            var otherNode = new TrainStationNode("waterloo");
            var connection = new TrainConnection(firstNode, otherNode, 10);
            firstNode.Connections.Add(connection);
            Assert.That(firstNode.Connections, Contains.Item(connection));
        }
       

    }
}

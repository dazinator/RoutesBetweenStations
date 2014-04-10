using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.Model;
using RoutesBetweenStations.Model.Factory;

namespace Model.Tests
{
    [TestFixture()]
    public class TrainStationTests
    {
        [Test(Description = "Can create a new TrainStation")]
        public void Can_Create_A_New_TrainStation()
        {
            // Arrange
            var name = "paddington";
            var newNode = new TrainStationFactory().CreateNode(name);
            Assert.That(newNode.Name, Is.EqualTo(name));
            Assert.That(newNode.Connections, Is.Not.Null);
        }


        [Test(Description = "Can add a connection to another TrainStation")]
        public void Can_Add_A_Connection_To_Another_TrainStation()
        {
            // Arrange
            var trainStationFactory = new TrainStationFactory();
            var firstNode = trainStationFactory.CreateNode("paddington");
            var otherNode = trainStationFactory.CreateNode("waterloo");

            var connectionFactory = new TrainConnectionFactory();
            var connection = connectionFactory.CreateConnection(firstNode, otherNode, 10);
            firstNode.Connections.Add(connection);
            Assert.That(firstNode.Connections, Contains.Item(connection));
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test(Description = "Cannot connect a TrainStation to itself.")]
        public void Cannot_Connect_A_TrainStation_To_Itself()
        {
            // Arrange
            var trainStationFactory = new TrainStationFactory();
            var newNode = trainStationFactory.CreateNode("paddington");

            var connectionFactory = new TrainConnectionFactory();
            newNode.Connections.Add(connectionFactory.CreateConnection(newNode, newNode, 10));
        }

    }
}

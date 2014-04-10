using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.Model.Train;

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
            var newNode = new TrainStation(name);
            Assert.That(newNode.Name, Is.EqualTo(name));
            Assert.That(newNode.Connections, Is.Not.Null);
        }


        [Test(Description = "Can add a connection to another TrainStation")]
        public void Can_Add_A_Connection_To_Another_TrainStation()
        {
            // Arrange
            var firstNode = new TrainStation("paddington");
            var otherNode = new TrainStation("waterloo");
            var connection = new TrainConnection(firstNode, otherNode, 10);
            firstNode.Connections.Add(connection);
            Assert.That(firstNode.Connections, Contains.Item(connection));
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test(Description = "Cannot connect a TrainStation to itself.")]
        public void Cannot_Connect_A_TrainStation_To_Itself()
        {
            // Arrange
            var newNode = new TrainStation("paddington");
            newNode.Connections.Add(new TrainConnection(newNode, newNode, 10));
        }
       

    }
}

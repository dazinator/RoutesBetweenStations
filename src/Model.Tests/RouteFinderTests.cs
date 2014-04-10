using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.DataAccess;
using RoutesBetweenStations.Model;
using RoutesBetweenStations.Model.Factory;
using RoutesBetweenStations.Model.Provider;

namespace Model.Tests
{
    [TestFixture()]
    public class RouteFinderTests
    {
        [Test(Description = "Can find a single direct route between 2 stations")]
        public void Can_Find_A_Single_Direct_Route_Between_2_Stations()
        {
            // Arrange
            // Set up the most basic train network - 2 stations with a single direct railway connection from a to b.
            INodeFactory trainStationFactory = new TrainStationFactory();

            var fromStation = trainStationFactory.CreateNode("station a");
            var toStation = trainStationFactory.CreateNode("station z");

            // Lay some railway track..
            const int travelTimeInMinutes = 10;
            INodeConnectionFactory connectionFactory = new TrainConnectionFactory();
            fromStation.Connections.Add(connectionFactory.CreateConnection(fromStation, toStation, travelTimeInMinutes));

            // Now put out train station network into list form..
            var network = new List<Node>();
            network.Add(fromStation);
            network.Add(toStation);

            // subject under test..
            var routeFinder = new DijkstraRouteProvider();

            // Act
            Route route = routeFinder.FindShortestRoute(network, fromStation, toStation);

            // Assert
            Assert.That(route, Is.Not.Null);
            Assert.That(route.TotalTravelTime(), Is.EqualTo(TimeSpan.FromMinutes(travelTimeInMinutes)));
            Assert.That(route.Connections, Is.Not.Null);
            Assert.That(route.Connections.Count, Is.EqualTo(1));
        }



    }
}

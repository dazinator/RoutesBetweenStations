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
            // From and To stations..
            var trainStationFactory = new TrainStationFactory();

            var fromNode = trainStationFactory.CreateNode("station a");
            var toNode = trainStationFactory.CreateNode("station z");

            // Create a single direct route from station a to station b that takes a total of 10 minutes
            const int travelTimeInMinutes = 10;

            var connectionFactory = new TrainConnectionFactory();
            fromNode.Connections.Add(connectionFactory.CreateConnection(fromNode, toNode, travelTimeInMinutes));

            // This is the network / graph containing all the stations that the routefinder will find routes amongst.
            var stations = new List<Node>();
            stations.Add(fromNode);
            stations.Add(toNode);

            var routeFinder = new DijkstraRouteProvider();

            // Act
            IEnumerable<Route> routes = routeFinder.FindRoutes(stations, fromNode, toNode);

            // Assert
            Assert.That(routes, Is.Not.Null);
            Assert.That(routes.Count(), Is.EqualTo(1));
            Route route = routes.First();
            Assert.That(route.TotalTravelTime(), Is.EqualTo(TimeSpan.FromMinutes(travelTimeInMinutes)));

            Assert.That(route.Connections, Is.Not.Null);
            Assert.That(route.Connections.Count, Is.Not.Null);
        }

       

    }
}

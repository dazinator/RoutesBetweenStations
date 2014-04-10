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
        [Test(Description = "Can find the quickest route between 2 stations when there is only a single connection.")]
        public void Can_Find_Quickest_Route_Between_2_Stations_With_A_Single_Connection()
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

        [Test(Description = "Can find the quickest route between 2 stations when a change is necessary to reach the destination station.")]
        public void Can_Find_Quickest_Route_Between_2_Stations_When_A_Change_Is_Necessary()
        {
            // Arrange
            // Set up the most basic train network - 2 stations with a single direct railway connection from a to b.
            INodeFactory trainStationFactory = new TrainStationFactory();

            var fromStation = trainStationFactory.CreateNode("station a");
            var toStation = trainStationFactory.CreateNode("station z");
            var interimStation = trainStationFactory.CreateNode("middle of nowhere");

            // Lay some railway track..
            INodeConnectionFactory connectionFactory = new TrainConnectionFactory();

            var trackToInterim = connectionFactory.CreateConnection(fromStation, interimStation, 10);
            fromStation.Connections.Add(trackToInterim);

            var trackToFinalStop = connectionFactory.CreateConnection(interimStation, toStation, 25);
            interimStation.Connections.Add(trackToFinalStop);

            // Now list the train stations in the network
            var network = new List<Node>();
            network.Add(fromStation);
            network.Add(toStation);
            network.Add(interimStation);

            // subject under test..
            var routeFinder = new DijkstraRouteProvider();

            // Act
            Route route = routeFinder.FindShortestRoute(network, fromStation, toStation);

            // Assert
            Assert.That(route, Is.Not.Null);

            TimeSpan expectedTotalTravelTime = trackToInterim.TravelTime.Add(trackToFinalStop.TravelTime);
            Assert.That(route.TotalTravelTime(), Is.EqualTo(expectedTotalTravelTime));
            Assert.That(route.Connections, Is.Not.Null);
            // should be 2 connections travelled (1 change)
            Assert.That(route.Connections.Count, Is.EqualTo(2));
        }

        [Test(Description = "Can find the quickest route between 2 stations when changes are necessary and there is more than one possible route (it should find the quickest)")]
        public void Can_Find_Quickest_Route_Between_2_Stations_When_Changes_Are_Necessary_And_There_Are_Alternative_Possible_Routes()
        {
            // Arrange
            // Set up the most basic train network - 2 stations with a single direct railway connection from a to b.
            INodeFactory trainStationFactory = new TrainStationFactory();

            var stationA = trainStationFactory.CreateNode("A");
            var stationZ = trainStationFactory.CreateNode("Z");
            var stationM = trainStationFactory.CreateNode("M");

            // Lay railway track for first route = total of 35 minutes..
            INodeConnectionFactory connectionFactory = new TrainConnectionFactory();

            // go from A to M
            var trackFromAToM = connectionFactory.CreateConnection(stationA, stationM, 10);
            stationA.Connections.Add(trackFromAToM);

            // go from M to Z
            var trackFromMtoZ = connectionFactory.CreateConnection(stationM, stationZ, 25);
            stationM.Connections.Add(trackFromMtoZ);


            // Lay railway track needed to complete a second route = this goes A - M - F - Z for a total of 29 minutes (quicker)..
            var stationF = trainStationFactory.CreateNode("F");
            var trackFromMToF = connectionFactory.CreateConnection(stationM, stationF, 10);
            stationM.Connections.Add(trackFromMToF);

            var trackFromFtoZ = connectionFactory.CreateConnection(stationF, stationZ, 9);
            stationF.Connections.Add(trackFromFtoZ);

            // Now list the train stations in the network
            var network = new List<Node>();
            network.Add(stationA);
            network.Add(stationM);
            network.Add(stationF);
            network.Add(stationZ);

            // subject under test..
            var routeFinder = new DijkstraRouteProvider();

            // Act
            Route fastestRoute = routeFinder.FindShortestRoute(network, stationA, stationZ);

            // Assert
            Assert.That(fastestRoute, Is.Not.Null);

            // Fastest route should be A - M - F - Z for a total of 29 minutes.
            TimeSpan expectedTotalTravelTime = trackFromAToM.TravelTime.Add(trackFromMToF.TravelTime).Add(trackFromFtoZ.TravelTime);
            Assert.That(fastestRoute.TotalTravelTime(), Is.EqualTo(expectedTotalTravelTime));
            Assert.That(fastestRoute.Connections, Is.Not.Null);
            // should be 3 connections travelled (2 changes)
            Assert.That(fastestRoute.Connections.Count, Is.EqualTo(3));
        }

    }
}

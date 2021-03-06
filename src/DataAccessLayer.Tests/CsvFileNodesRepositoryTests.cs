﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoutesBetweenStations.DataAccess;
using RoutesBetweenStations.Model;

namespace RoutesBetweenStations.DataAccessLayer.Tests
{
    [TestFixture()]
    public class CsvFileNodesRepositoryTests
    {
        [Test(Description = "Can get the train stations from a csv file, using the CSV repository.")]
        public void Can_Get_TrainStations_From_CSV_Repository()
        {
            // Arrange

            var repos = new CsvFileNodesRepository();

            // Act
            var nodes = repos.GetNodes();

            // Assert
            Assert.That(nodes, Is.Not.Null);
            Assert_That_Nodes_List_Contains_Station_Named("basingstoke", nodes);
            Assert_That_Nodes_List_Contains_Station_Named("reading", nodes);
            Assert_That_Nodes_List_Contains_Station_Named("farnborough", nodes);
            Assert_That_Nodes_List_Contains_Station_Named("clapham junction", nodes);
            Assert_That_Nodes_List_Contains_Station_Named("london victoria", nodes);
            Assert_That_Nodes_List_Contains_Station_Named("london paddington", nodes);
            Assert.That(nodes.Count, Is.EqualTo(6));

        }

        #region Helper Methods

        private void Assert_That_Nodes_List_Contains_Station_Named(string name, IEnumerable<Node> nodes)
        {
            var stationOrNull = nodes.FirstOrDefault(s => s.Name.ToLowerInvariant() == name.ToLowerInvariant());
            if (stationOrNull == null)
            {
                Assert.Fail("A node named " + name + " was not contained within the list of nodes.");
            }

        }

        #endregion

    }
}

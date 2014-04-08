using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RoutesBetweenStations.DataAccessLayer.Tests
{
    [TestFixture()]
    public class CsvFileNodesRepositoryTests
    {
        [Test(Description = "Given a csv file containing a distinct set of nodes, tests that the nodes can be loaded via the repository.")]
        public void Can_Get_Nodes()
        {
            // Arrange
            var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "StationsList.txt");
            using (var repos = new CsvFileNodesRepository(filePath))
            {
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

        }

        #region Helper Methods

        private void Assert_That_Nodes_List_Contains_Station_Named(string name, IList<Node> nodes)
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

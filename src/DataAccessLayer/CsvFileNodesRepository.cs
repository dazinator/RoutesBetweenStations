using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RoutesBetweenStations.Model;
using RoutesBetweenStations.Model.Factory;

namespace RoutesBetweenStations.DataAccess
{
    /// <summary>
    /// An <see cref="INodesRepository"/> implementation that uses a CSV file as it's data store.
    /// </summary>
    public class CsvFileNodesRepository : INodesRepository
    {
        private const char Delimiter = ',';
        private Stream _Stream;
        private INodeFactory _NodeFactory;
        private INodeConnectionFactory _NodeConnectionFactory;

        /// <summary>
        /// Constructor that takes the filePath used as the data source for the nodes.
        /// </summary>
        /// <param name="csvFilePath">The full file path of the CSV file containing Nodes data.</param>
        public CsvFileNodesRepository(string csvFilePath)
            : this(new TrainStationFactory(), new TrainConnectionFactory(), new FileStream(csvFilePath, FileMode.Open, FileAccess.Read))
        {
        }

        /// <summary>
        /// Constructor that takes a stream used as the data source for the nodes.
        /// </summary>
        /// <param name="nodeConnectionFactory">The factory that will ne used to create instances of <see cref="NodeConnection"/>'s read from the stream.</param>
        /// <param name="stream"></param>
        /// <param name="nodeFactory">The factory that will ne used to create instances of <see cref="Node"/>'s read from the stream.</param>
        public CsvFileNodesRepository(INodeFactory nodeFactory, INodeConnectionFactory nodeConnectionFactory, Stream stream)
        {
            _Stream = stream;
            _NodeFactory = nodeFactory;
            _NodeConnectionFactory = nodeConnectionFactory;
        }

        /// <summary>
        /// Returns the Nodes.
        /// </summary>
        /// <returns></returns>
        public IList<Node> GetNodes()
        {
            if (_Stream.CanSeek && _Stream.Position != 0)
            {
                _Stream.Seek(0, SeekOrigin.Begin);
            }
            if (_Stream.Position != 0)
            {
                throw new InvalidOperationException("Could not navigate to the beginning of the stream.");
            }
            if (!_Stream.CanRead)
            {
                throw new InvalidOperationException("The stream cannot be read from.");
            }

            var stations = new Dictionary<string, Node>();

            using (var reader = new StreamReader(_Stream, System.Text.Encoding.UTF8, true, 128, true))
            {
                int lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    var csvData = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(csvData))
                    {
                        var parts = csvData.Split(Delimiter);
                        if (parts.Length != 3)
                        {
                            throw new FormatException(string.Format("Line number: {0} is not correctly formatted. The line should consist of 3 values and use a comma delimiter to seperate them.", lineNumber));
                        }

                        var stationOne = parts[0].Trim();
                        var stationTwo = parts[1].Trim();
                        if (string.IsNullOrWhiteSpace(stationOne) || string.IsNullOrWhiteSpace(stationTwo))
                        {
                            throw new FormatException(string.Format("Line number: {0} is not correctly formatted. There must be 2 station names specified, the name of the station cannot be empty or whitespace.", lineNumber));
                        }
                        var journeyDuration = parts[2].Trim();

                        int minutes;
                        if (!int.TryParse(journeyDuration, out minutes))
                        {
                            throw new FormatException(string.Format("Line number: {0} is not correctly formatted. The duration value should be an integer representing the journey time between the stations as the number of minutes.", lineNumber));
                        }

                        Node startNode;
                        Node endNode;
                        if (!stations.ContainsKey(stationOne))
                        {
                            startNode = _NodeFactory.CreateNode(stationOne);
                            stations[stationOne] = startNode;
                        }
                        else
                        {
                            startNode = stations[stationOne];
                        }

                        if (!stations.ContainsKey(stationTwo))
                        {
                            endNode = _NodeFactory.CreateNode(stationTwo);
                            stations[stationTwo] = endNode;
                        }
                        else
                        {
                            endNode = stations[stationTwo];
                        }

                        // Create a connection for these 2 nodes, using the connection factory to create the appropriate concrete connection.
                        var connection = _NodeConnectionFactory.CreateConnection(startNode, endNode, minutes);
                        // ReSharper disable PossibleNullReferenceException
                        // startNode cannot be null by convention of factory implementations either returning new instance or throwing an exception. 
                        startNode.Connections.Add(connection);
                        // ReSharper restore PossibleNullReferenceException

                    }
                }
            }

            // Return the stations.
            return stations.Select(a => a.Value).ToList();

        }

        /// <summary>
        /// Disposes of the stream.
        /// </summary>
        public void Dispose()
        {
            if (_Stream != null)
            {
                _Stream.Dispose();
            }
        }
    }
}
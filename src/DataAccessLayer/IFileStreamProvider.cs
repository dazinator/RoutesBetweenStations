using System.IO;

namespace RoutesBetweenStations.DataAccess
{
    /// <summary>
    /// This defines a contract for a component that provides access to various IO streams that the application needs.
    /// </summary>
    public interface IFileStreamProvider
    {
        string CsvFilePath { get; set; }
        Stream GetCsvFile();
    }
}
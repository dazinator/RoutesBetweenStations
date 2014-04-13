using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RoutesBetweenStations.DataAccess
{
    /// <summary>
    /// An implementation of <see cref="IFileStreamProvider"/>
    /// </summary>
    public class FileStreamProvider : IFileStreamProvider
    {
        public FileStreamProvider()
        {
            // if running under asp.net then use the website bin folder.
            if (HttpContext.Current != null)
            {
                this.CsvFilePath = System.IO.Path.Combine(System.Web.HttpRuntime.BinDirectory, "StationsList.txt");
            }
            else
            {
                // Otherwise use the current environment directory.
                this.CsvFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "StationsList.txt");
            }
        }

        public string CsvFilePath { get; set; }

        public Stream GetCsvFile()
        {
            return new FileStream(CsvFilePath, FileMode.Open, FileAccess.Read);
        }
    }

}

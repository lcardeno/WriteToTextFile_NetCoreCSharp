using Dapper;
using ExportRDCFiles.DTO;
using ExportRDCFiles.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRDCFiles
{
    class Program
    {
        private static int ClientID;
        private static int Month;
        private static int Year;

        static void Main(string[] args)
        {
            ClientID = int.Parse(ConfigurationManager.AppSettings.Get("ClientID"));
            Month = int.Parse(ConfigurationManager.AppSettings.Get("ReportMonth"));
            Year = int.Parse(ConfigurationManager.AppSettings.Get("ReportYear"));

            IDataRepository repository = new DapperRepository();
            var reporter = new ReportService();

            var boatDataClient = repository.GetBoatDataClient(ClientID);
            var clientServices = repository.GetBoatDataClientServices(ClientID);


            foreach (var service in clientServices)
            {
                Console.WriteLine($"> creating {service.ServiceID}");
                var data = repository.GetComplaintsForBoatClient(ClientID, Month, Year, service.ServiceID);
                var rds = new ReportDataSet() { ComplaintsData = data, ClientName = boatDataClient.ClientName, Month = Month, Year = Year };
                reporter.GenerateReport(rds);
            }
        }
    }
}

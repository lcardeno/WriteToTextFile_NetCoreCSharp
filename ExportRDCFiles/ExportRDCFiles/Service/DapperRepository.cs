using Dapper;
using ExportRDCFiles.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRDCFiles.Service
{
    public class DapperRepository : IDataRepository
    {
        private string _connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");


        public BoatDataClient GetBoatDataClient(int clientID)
        {
            var sql = "exec [sp_GetBoatDataClients] @ClientID";
            var values = new { ClientID = clientID };

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<BoatDataClient>(sql, values).FirstOrDefault();
                return result;
            }
        }

        public List<BoatDataClientService> GetBoatDataClientServices(int clientID)
        {
            var sql = "exec [sp_GetBoatDataClientServiceDetail] @ClientID";
            var values = new { ClientID = clientID };

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<BoatDataClientService>(sql, values).ToList();
                return result;
            }
        }

        public List<ComplaintsData> GetComplaintsForBoatClient(int clientID, int month, int year, int serviceID)
        {
            var sql = "exec [sp_GetComplaintsForBoatClients] @ClientID, @Month, @Year, @ServiceID";
            var values = new { ClientID = clientID, Month = month, Year = year, ServiceID = serviceID };

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<ComplaintsData>(sql, values).ToList();
                return result;
            }
        }
    }
}

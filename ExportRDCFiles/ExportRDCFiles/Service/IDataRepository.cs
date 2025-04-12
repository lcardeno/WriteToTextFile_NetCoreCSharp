using ExportRDCFiles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRDCFiles.Service
{
    interface IDataRepository
    {
        BoatDataClient GetBoatDataClient(int clientID);
        List<BoatDataClientService> GetBoatDataClientServices(int clientID);
        List<ComplaintsData> GetComplaintsForBoatClient(int clientID, int month, int year, int serviceID);
    }
}

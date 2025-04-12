using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRDCFiles.DTO
{
    public class ReportDataSet
    {
        public string ClientName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<ComplaintsData> ComplaintsData { get; set; }
    }
}

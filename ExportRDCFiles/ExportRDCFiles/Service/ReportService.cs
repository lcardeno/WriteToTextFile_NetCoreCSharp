using ExportRDCFiles.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRDCFiles.Service
{
    public class ReportService
    {
        public void GenerateReport(ReportDataSet data)
        {
            string url = ConfigurationManager.AppSettings.Get("OutputPath");
            if (data.ComplaintsData is null || data.ComplaintsData.Count == 0) return;

            var stateCode = data.ComplaintsData.FirstOrDefault().StateCode;
            var path = PrepareFile(url, data.ClientName, stateCode);
            WriteDataToFile(path, data.ComplaintsData);
        }

        private void WriteDataToFile(string filePath, List<ComplaintsData> complaints)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                foreach(var c in complaints)
                {
                    var row = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}",
                        c.Service, //0
                        c.ServiceId, //1
                        c.ReportDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), //2
                        RemoveTabs(c.CourtName), //3
                        RemoveTabs(c.StateCode), //4
                        RemoveTabs(c.Plaintiffs), //5
                        RemoveTabs(c.Defendants), //6
                        RemoveTabs(c.Description), //7
                        c.FilingDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), //8
                        RemoveTabs(c.CaseNumber), //9
                        RemoveTabs(c.Judge), //10
                        RemoveTabs(c.LocalPlaintiffLawyer), //11
                        RemoveTabs(c.LocalPlaintiffFirm), //12
                        RemoveTabs(c.LocalDefendantLawyer), //13
                        RemoveTabs(c.LocalDefendantFirm), //14
                        RemoveTabs(c.City), //15
                        RemoveTabs(c.OutStatePlaintiffLawyer), //16
                        RemoveTabs(c.OutStatePlaintiffFirm), //17
                        RemoveTabs(c.OutStateDefendantLawyer), //18
                        RemoveTabs(c.OutStateDefendantFirm), //19
                        c.StateId //20
                        );

                    sw.WriteLine(row);
                }
            }
        }

        public static string RemoveTabs(string input)
        {
            var result = input.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(System.Environment.NewLine, "");
            return result;
        }

        private string GetFilePath(string dir, string clientName, string stateCode)
        {
            var now = DateTime.Now;
            var fileName = string.Format("{0}_{1}_{2}.txt", clientName, stateCode, now.ToString("MMyyyy"));
            var filePath = dir + fileName;

            return filePath;
        }

        private string PrepareFile(string dir, string clientName, string stateCode)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var now = DateTime.Now;
            var fileName = string.Format("{0}_{1}_{2}.txt",clientName, stateCode, now.ToString("MMyyyy"));
            var filePath = GetFilePath(dir, clientName, stateCode);

            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    WriteHeaderToFile(fs);
                }
            }

            return filePath;
        }

        private void WriteHeaderToFile(FileStream stream)
        {
            var header = "Service	ServiceID	ReportDate	CourtName	StateCode	Plaintiffs	Defendants	Description	FilingDate	CaseNumber	Judge	LocalPlaintiffLawyer	LocalPlaintiffFirm	LocalDefendantLawyer	LocalDefendantFirm	City	OutStatePlaintiffLawyer	OutStatePlaintiffFirm	OutStateDefendantLawyer	OutStateDefendantFirm	StateId\n";
            Byte[] title = new UTF8Encoding(true).GetBytes(header);
            stream.Write(title, 0, title.Length);
        }
    }
}

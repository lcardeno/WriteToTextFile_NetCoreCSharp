using System;

namespace ExportRDCFiles.DTO
{
    public class ReportRowData
    {
        public string Service { get; set; }
        public int ServiceID { get; set; }
        public DateTime ReportDate { get; set; }
        public string CourtName { get; set; }
        public string StateCode { get; set; }
        public string Plaintiffs { get; set; }
        public string Defendants { get; set; }
        public string Description { get; set; }
        public DateTime FilingDate { get; set; }
        public string CaseNumber { get; set; }
        public string Judge { get; set; }
        public string LocalPlaintiffLawyer { get; set; }
        public string LocalPlaintiffFirm { get; set; }
        public string LocalDefendantLawyer { get; set; }
        public string LocalDefendantFirm { get; set; }
        public string City { get; set; }
        public string OutStatePlaintiffLawyer { get; set; }
        public string OutStatePlaintiffFirm { get; set; }
        public string OutStateDefendantLawyer { get; set; }
        public string OutStateDefendantFirm { get; set; }
        public int StateId { get; set; }
    }
}

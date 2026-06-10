namespace DevetionStudetns.DTO.Repotrs
{
    public class GetReportsDto
    {
        public int ReportId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string ReportName { get; set; } 
        public string ConcernedParty { get; set; }
        public byte[] ReportAttachment { get; set; }
    }
}

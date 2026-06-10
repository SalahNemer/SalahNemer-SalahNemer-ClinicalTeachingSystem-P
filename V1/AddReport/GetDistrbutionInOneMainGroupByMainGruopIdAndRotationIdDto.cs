namespace V1.Controllers.CreateReport
{
    public class GetDistrbutionInOneMainGroupByMainGruopIdAndRotationIdDto
    {
        public string MainGroupSympole {  get; set; }
        public string SubGroupSympole {  get; set; }
        public string WeekName { get; set; }
        public DateOnly StartSessionDate { get; set; }
        public DateOnly EndSessionDate { get; set; }
        public string FullName { get; set; }
        public string CourseName { get; set; }
        public string HospitalName { get; set; }



    }
}

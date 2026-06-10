namespace V1.DTO.ApprovalsDTO
{
    public class GetApprovalsQ2Dto
    {
        public int DistributionId { get; set; }
        public string? MainGroupSympole { get; set; }
        public string? SubGroupSympole { get; set;}
        public string? DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? HospitalName { get; set; }
        public string? DepartmentName { get; set; }
        public string? CourseName { get; set; }
        public string? RotationName { get; set; }
        public DateOnly? StartRotationDate { get; set; }
        public DateOnly? EndRotationDate { get; set; }
        public string? WeekName { get; set; }
        public DateOnly? StartSessionDate { get; set; }
        public DateOnly? EndSessionDate { get; set ; }
    }
}

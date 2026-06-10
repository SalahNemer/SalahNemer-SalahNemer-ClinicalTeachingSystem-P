namespace DevetionStudetns.DTO.DistributionsMainGroupDTO
{
    public class ShowDistrbutionMainQDto
    {
        public int DistributionsMainGroupId { get; set; }
        public string MainGroupSympole {  get; set; }
        public int AcademicYearId { get; set; }
        public string DepartmentName { get; set; }
        public string RotationName { get; set; }
        public DateOnly StartRotationDate { get; set; }
        public DateOnly EndRotationDate { get; set; }
    }
}

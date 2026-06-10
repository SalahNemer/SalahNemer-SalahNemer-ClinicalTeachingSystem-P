namespace DevetionStudetns.DTO.RotationsDTO
{
    public class GetRotaionDto
    {
        public int RotationId { get; set; }
        public string RotationName { get; set; }
        public DateOnly StartRotationDate { get; set; }
        public DateOnly EndRotationDate { get; set; }
        public string AcademicYearName { get; set; }
    }
}

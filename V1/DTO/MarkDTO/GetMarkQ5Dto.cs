namespace V1.DTO.MarkDTO
{
    public class GetMarkQ5Dto
    {
        public int markId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string StudentsId { get; set; }
        public string StudentsName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string MarkType { get; set; }
        public string? Comments {  get; set; }   
        public float Mark { get; set; }
    }
}

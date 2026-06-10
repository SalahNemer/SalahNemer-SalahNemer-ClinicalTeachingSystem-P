namespace V1.DTO.MarkDTO
{
    public class GetMarkQ6Dto
    {
        public int MarkId { get; set; } 
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string MarkType { get; set; }
        public float Mark { get; set; }

    }
}

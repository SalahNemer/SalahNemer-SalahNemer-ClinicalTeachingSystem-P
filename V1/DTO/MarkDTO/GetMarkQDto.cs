using System.Diagnostics.Contracts;

namespace V1.DTO.MarkDTO
{
    public class GetMarkQDto
    {
        public int MarkId { get; set; }
        public string DoctorId { get; set; }
        public string FullName { get; set; }
        public string StudentId { get; set; }
        public int StudentLevel { get; set; }        
        public string CourseName { get; set; }
        public string MarkType { get; set; }
        public float Mark {  get; set; }
        public string Comments { get; set; }
    }
}

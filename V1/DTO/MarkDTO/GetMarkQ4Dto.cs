namespace V1.DTO.MarkDTO
{
    public class GetMarkQ4Dto
    {
        public int MarkId { get; set; } 
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SubGroupSympole { get; set; } = string.Empty;
        public string MainGroupSympole { get; set; } = string.Empty;
        public int CouresId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int CourseIevel { get; set; }
        public string MarkType {  get; set; } 
        public float Mark {  get; set; }    
    }
}

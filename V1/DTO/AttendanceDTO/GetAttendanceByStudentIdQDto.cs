namespace DevetionStudetns.DTO.AttendanceDTO
{
    public class GetAttendanceByStudentIdQDto
    {
        public string StudentId { get; set; } = string.Empty; 
        public string StudentName { get; set; } = string.Empty; 
        public string StudentEmail { get; set; } = string.Empty; 
        public int AttendanceId { get; set; } 
        public int CourseId { get; set; } 
        public string CourseName { get; set; } = string.Empty; 
        public string DoctorId { get; set; } = string.Empty; 
        public string DoctorName { get; set; } = string.Empty; 
        public string AttendanceStatus { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public string? Notes { get; set; }
    }
}

namespace DevetionStudetns.DTO.AttendanceDTO
{
    public class GetAttendanceByDoctorIdQDto
    {
        public string  DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int AttendanceId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string  StudentId { get; set; }
        public string StudentName { get; set; }
        public string AttendanceStatus { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public string? Notes { get; set; }
    }
}

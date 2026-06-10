namespace DevetionStudetns.DTO.AttendanceDTO
{
    public class CreateAttendanceDTO
    {
        public int CourseId { get; set; }
        public string StudentId {  get; set; }
        public string DoctorId { get; set; }
        public string  AttendanceStatus { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public string? Notes { get; set; }
        
    }
}

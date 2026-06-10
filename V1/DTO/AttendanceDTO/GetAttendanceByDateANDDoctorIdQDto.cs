namespace V1.DTO.AttendanceDTO
{
    public class GetAttendanceByDateANDDoctorIdQDto
    {
        public int AttendanceId { get; set; } 
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty ;
        public string AttendanceStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}

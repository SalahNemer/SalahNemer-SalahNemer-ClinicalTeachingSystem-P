namespace V1.DTO.AttendanceDTO
{
    public class GetAttendanceByDateQDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty ;
        public string AttendanceStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }

    }
}

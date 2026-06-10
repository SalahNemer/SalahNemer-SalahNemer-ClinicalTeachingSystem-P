namespace DevetionStudetns.DTO.AttendanceDTO
{
    public class AttendanceUpdateDTO
    {
        public string StudentId { get; set; }  = string.Empty;
        public string AttendanceStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}

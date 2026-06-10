namespace DevetionStudetns.DTO.AppointmentsDTO
{
    public class GetAppotmentsDto
    {
        public string SubGroupSympole { get; set; }
        public int RotationId { get; set; }
        public string RotationName { get; set; }
        public DateOnly StartRotationDate { get; set; }
        public DateOnly EndRotationDate { get; set; }
        public string WeekName { get; set; }
        public DateOnly StartSessionDate { get; set; }
        public DateOnly EndSessionDate { get; set; }
        public TimeOnly SessionStartTime { get; set; }
        public TimeOnly SessionEndTime { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace V1.DTO.AppointmentDTO
{
    public class GetAppointmentsQDto
    {
        public int AppointmentId { get; set; }
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

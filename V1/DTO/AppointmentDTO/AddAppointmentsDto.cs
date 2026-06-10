using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.AppointmentsDTO
{
    public class AddAppointmentsDto
    {
        [Required(ErrorMessage = "WeekName is required.")]
        public string WeekName { get; set; }
        [Required(ErrorMessage = "RotationId is required.")]
        public int RotationId { get; set; }
        [Required(ErrorMessage = "Start Session Date is required.")]
        public DateOnly StartSessionDate { get; set; }
        [Required(ErrorMessage = "End Session Date is required.")]
        public DateOnly EndSessionDate { get; set; }
        [Required(ErrorMessage = "Session Start Time is required.")]
        public TimeOnly SessionStartTime { get; set; }
        [Required(ErrorMessage = "Session End Time is required.")]
        public TimeOnly SessionEndTime { get; set; }
    }
}

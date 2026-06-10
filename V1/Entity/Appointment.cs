using DataBase.Entity;
using DevetionStudetns.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.entitys
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string WeekName { get; set; }

        [Required]
        [Column("StartSessionDate")] 
        public DateOnly StartSessionDate { get; set; }
        [Required]
        [Column("EndSessionDate")] 
        public DateOnly EndSessionDate { get; set; }
        [Required]
        [Column("SessionStartTime")]
        public TimeOnly SessionStartTime { get; set; }
        [Required]
        [Column("SessionEndTime")]
        public TimeOnly SessionEndTime { get; set; }

        public int RotationId { get; set; }
        [ForeignKey("RotationId")]
        public Rotation Rotations { get; set; }

        public ICollection<Distribution> Distributions { get; set; }
        public ICollection<WeeklyEvaluation> WeeklyEvaluation { get; set; }
        public ICollection<AnswerTheEvaluation> answerTheEvaluation { get; set; }


    }
}

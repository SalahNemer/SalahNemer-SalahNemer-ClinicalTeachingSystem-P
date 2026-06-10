using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DataBase.entitys
{
    [Table("Distributions")]
    public class Distribution
    {
        [Key]
        public int DistributionId { get; set; }
        [Required]
        [Column("SubGroupId")]
        public int SubGroupId { get; set; }
        [ForeignKey("SubGroupId")]
        public SubGroup SubGroup { get; set; }
        [Required]
        [Column("DoctorId")]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Users User { get; set; }
        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [Required]
        [Column("AppointmentId")]
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointments { get; set; }
        [Required]
        [Column("RotationId")]
        public int RotationId { get; set; }
        [ForeignKey("RotationId")]
        public Rotation Rotations { get; set; }
        [Required]
        [Column("DistributionStatus")]
        public int DistributionStatus { get; set; }
        public string? Notes { get; set; }
    }
}

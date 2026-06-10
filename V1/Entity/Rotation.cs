using DevetionStudetns.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.entitys
{
    [Table("Rotations")]
    public class Rotation
    {
        [Key]
        public int RotationId { get; set; }
        [Required]
        [Column("StartRotationDate")]
        public DateOnly StartRotationDate { get; set; }
        [Required]
        [Column("EndRotationDate")]
        public DateOnly EndRotationDate { get; set; }
        [Required]
        public string RotationName { get; set; }

        [Required]
        public string AcademicYearName { get; set; }

        public ICollection<Distribution> Distributions { get; set; }
        public ICollection<DistributionsMainGroup> DistributionsMainGroup { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}

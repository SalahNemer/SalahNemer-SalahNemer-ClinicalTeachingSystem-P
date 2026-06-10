using database.models;
using DataBase.Entity;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace BuildDB_Team.entitys
{
    [Table("Marks")]
    public class Marks
    {
        [Key]
        public int MarkId { get; set; }

        [Required]
        [Column("StudentId")]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Students { get; set; }

        [Required]
        [Column("DoctorId")]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctors { get; set; }

        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course course { get; set; }

        [Column("MarkType")]
        public string MarkType { get; set; }
        [Required]
        [Column("Mark")]
        public float Mark { get; set; }

        [Required]
        [Column("EntryDate")]
        public DateTime EntryDate { get; set; }

        [Column("Comments")]
        public string? Comments { get; set; }

        public int MarkStatus { get; set; }
    }
}

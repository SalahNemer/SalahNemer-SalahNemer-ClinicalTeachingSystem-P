using DataBase.Entity;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace database.models  
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        [Column("AttendanceId")]
        public int AttendanceId { get; set; }

        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course course { get; set; }

        [Required]
        [Column("StudentId")]
        public string StudentId { get; set; } 
        [ForeignKey("StudentId")]
        public Users Students { get; set; }  

        [Required]
        [Column("DoctorId")]
        public string DoctorId { get; set; } = string.Empty;
        [ForeignKey("DoctorId")]
        public Users Doctors { get; set; }  

        [Required]
        [Column("AttendanceDate")]
        public DateOnly AttendanceDate { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("AttendanceStatus")]
        public string AttendanceStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }

    }
}

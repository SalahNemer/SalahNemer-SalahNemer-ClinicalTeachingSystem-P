using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using database.models;

namespace DevetionStudetns.Entity
{
    public class Doctor_Course
    {
        [Required]
        [Column("Cours")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course course { get; set; }
        [Required]
        [Column("DoctorId")]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor doctors { get; set; }
        public string CurrentAcademicYearName { get; set; }
    }
}

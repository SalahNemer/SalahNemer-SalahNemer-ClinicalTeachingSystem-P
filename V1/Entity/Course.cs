using BuildDB_Team.entitys;
using DataBase.Entity;
using database.models;
using DataBase.entitys;
using DevetionStudetns.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DevetionStudetns.NewFolder
{
    [Table("Course")]
    public class Course
    {
        [Key]   
        public int CouresId { get; set; }
        [Required]
        [NotNull]
        public int CourseIevel { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [Required]
        [NotNull]
        public string CourseName { get; set; } = string.Empty;
        [NotNull]
        [Required]
        public string CourseCode { get; set; } = string.Empty;
        [Required]
        public int CourseAcademicHours { get; set; }
        [Required]
        public float WeeklyRatingPercentage { get; set; }

        public ICollection<Distribution> Distributions { get; set; }
        public ICollection<Doctor_Course> doctor_Courses { get; set; }
        public ICollection<Attendance> Attendance { get; set; }
        public ICollection<Marks> Marks { get; set; }
        public ICollection<WeeklyEvaluation> WeeklyEvaluation { get; set; }
    }
}

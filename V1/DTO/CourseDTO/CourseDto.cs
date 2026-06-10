using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.CourseDTO
{
    public class CourseDto
    {
        [Required(ErrorMessage = "This column CourseIevel is required.")]
        public int CourseIevel { get; set; }
        [Required(ErrorMessage = "This column DepartmentId is required.")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "This column CourseName is required.")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "This column CourseCode is required.")]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "This column CourseAcademicHours is required.")]
        public int CourseAcademicHours { get; set; }
        public float WeeklyRatingPercentage { get; set; }
    }
}

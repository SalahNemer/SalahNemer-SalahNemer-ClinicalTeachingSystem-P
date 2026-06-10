using System.ComponentModel.DataAnnotations;

namespace V1.DTO.CourseDTO
{
    public class UpdateCourseDto
    {
        [Required(ErrorMessage = "This column CourseIevel is required.")]
        public int CourseIevel { get; set; }
        [Required(ErrorMessage = "This column DepartmentId is required.")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "This column CourseName is required.")]
        public string CourseName { get; set; }
       
        [Required(ErrorMessage = "This column CourseAcademicHours is required.")]
        public int CourseAcademicHours { get; set; }
        public float WeeklyRatingPercentage { get; set; }
    }
}

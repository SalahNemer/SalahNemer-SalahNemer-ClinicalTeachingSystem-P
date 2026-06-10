using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO
{
    public class GetCourseDto
    {
        public int CouresId { get; set; }
        public int CourseIevel { get; set; }
        public int DepartmentId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int CourseAcademicHours { get; set; }
        public float WeeklyRatingPercentage { get; set; }
    }
}

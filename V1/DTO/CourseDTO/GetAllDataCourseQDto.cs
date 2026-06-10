using DevetionStudetns.NewFolder;

namespace FinalProject.DTO.CourseDTO
{
    public class GetAllDataCourseQDto
    {
        public int CouresId { get; set; }
        public string CourseName { get; set; }
        public int CourseIevel { get; set; }
        public string CourseCode { get; set; }
        public int CourseAcademicHours { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public float WeeklyRatingPercentage { get; set; }
    }
}

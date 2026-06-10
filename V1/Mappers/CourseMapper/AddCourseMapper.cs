using DevetionStudetns.DTO;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.CourseDTO;

namespace DevetionStudetns.Mappers.CourseMappier
{
    public static class AddCourseMapper
    {
        public static Course addCourse(this CourseDto courseDto)
        {
            return new Course
            {
                CourseIevel = courseDto.CourseIevel,
                CourseName = courseDto.CourseName,
                CourseCode = courseDto.CourseCode,
                DepartmentId = courseDto.DepartmentId,
                CourseAcademicHours = courseDto.CourseAcademicHours,
                WeeklyRatingPercentage = courseDto.WeeklyRatingPercentage,
            };
        }

        public static Course GetCourse(this GetCourseDto courseDto)
        {
            return new Course
            {
                CouresId = courseDto.CouresId,
                CourseIevel = courseDto.CourseIevel,
                CourseName = courseDto.CourseName,
                CourseCode = courseDto.CourseCode,
                DepartmentId = courseDto.DepartmentId,
                CourseAcademicHours = courseDto.CourseAcademicHours,
                WeeklyRatingPercentage = courseDto.WeeklyRatingPercentage,
            };
        }

    }
}

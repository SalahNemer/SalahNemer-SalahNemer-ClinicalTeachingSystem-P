using DevetionStudetns.Entity;
using V1.DTO.DoctorCrouseDTO;

namespace V1.Mappers.DoctorCourseMapper
{
    public static class AddDoctorCourseMapper
    {
        public static Doctor_Course AddDoctorCrouse (this AddDoctorCourseDto addDoctorCourseDto)
        {
            return new Doctor_Course
            {
                CourseId = addDoctorCourseDto.CourseId,
                CurrentAcademicYearName = addDoctorCourseDto.CurrentAcademicYearName,
                DoctorId = addDoctorCourseDto.DoctorId,
            };
        }
    }
}

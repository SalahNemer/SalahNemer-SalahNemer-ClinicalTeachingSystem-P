using database.models;
using DevetionStudetns.DTO;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.Interface;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.CourseDTO;
using loginpage.DBcon;
using V1.DTO.CourseDTO;

namespace DevetionStudetns.Service
{
    public class CourseServes
    {
        private readonly ICourse _Course;
        public CourseServes(ICourse Course)
        {
            _Course = Course;
        }
        public async Task<List<GetCourseDto>> getCourse()
        {
            return await _Course.GetCourse();
        }
        public async Task<List<GetAllDataCourseQDto>> GetAllData()
        {
            return await _Course.GetAllData();
        }
        public async Task<List<GetAllDataCourseQDto>> GetCorseByDepartementId(int DepartementId)
        {
            return await _Course.GetCorseByDepartement(DepartementId);
        }
        public async Task<GeneralMsgDto> AddCourse(CourseDto course)
        {
            return await _Course.AddCourse(course);
        }
        public async Task<GeneralMsgDto> deleteCourse(int courseId)
        {
            return await _Course.DeleteCourse(courseId);
        }
        public async Task<GeneralMsgDto> updateCourse(int id, UpdateCourseDto course)
        {
            return await _Course.UpdateCourseDate(id, course);
        }
        public async Task<List<GetAllDataCourseQDto>> getCourseByCourseLevel(int CourseLevel)
        {
            return await _Course.GetCourseByCourseLevel(CourseLevel);
        }
        public async Task<List<GetAllDataCourseQDto>> getCourseByCourseLevelAndDepartement(int CourseLevel, int DepartementId)
        {
            return await _Course.GetCourseByCourseLevelAndDepartement(CourseLevel, DepartementId);
        }
        public async Task<List<GetAllDataCourseQDto>> GetCourseByCourseId(int CourseId)
        {
            return await _Course.GetCourseByCourseId(CourseId);
        }


    }
}

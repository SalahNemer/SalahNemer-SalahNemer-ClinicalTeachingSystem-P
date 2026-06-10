using database.models;
using DevetionStudetns.DTO;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.CourseDTO;
using loginpage.DBcon;
using V1.DTO.CourseDTO;

namespace DevetionStudetns.Interface
{
    public interface ICourse
    {
        public Task<List<GetCourseDto>> GetCourse();
        public Task<List<GetAllDataCourseQDto>> GetAllData();
        public Task<List<GetAllDataCourseQDto>> GetCorseByDepartement(int DepartementId);
        public Task<List<GetAllDataCourseQDto>> GetCourseByCourseLevel(int CourseLevel);
        public Task<List<GetAllDataCourseQDto>> GetCourseByCourseLevelAndDepartement(int CourseLevel,int DepartementId);
        public Task<GeneralMsgDto> AddCourse(CourseDto hospital);
        public Task<GeneralMsgDto> DeleteCourse(int id);
        public Task<GeneralMsgDto> UpdateCourseDate(int id, UpdateCourseDto dto);
        public Task<List<GetAllDataCourseQDto>> GetCourseByCourseId(int CourseId);
    }
}

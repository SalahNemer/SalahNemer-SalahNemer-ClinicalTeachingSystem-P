using loginpage.DBcon;
using V1.DTO.DoctorCrouseDTO;

namespace V1.Interface.IRepositry
{
    public interface IDoctorCrouse
    {
        public GeneralMsgDto AddDcotorCourse(AddDoctorCourseDto addDoctorCourseDto);
        public List<GetDoctorCourseDto> GetAllDoctorCourse();
        public List<GetDoctorCourseDto> GetDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId);
        public List<GetDoctorCourseDto> GetDoctorCourseByCurrentAcademicYear(string CurrentAcademicYearName);
        public GeneralMsgDto DeleteDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId);
        public GeneralMsgDto UpdateDoctorCourseByDoctorIdAndCourseId(UpdateDoctorCourseDto updateDoctorCourseDto, string DoctorId, int CourseId);
        public List<GetSuperviseDoctorQDto> GetSupervisedDoctor();

    }
}

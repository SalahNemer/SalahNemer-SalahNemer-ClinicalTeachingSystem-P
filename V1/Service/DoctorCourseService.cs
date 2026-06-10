using loginpage.DBcon;
using V1.DTO.DoctorCrouseDTO;
using V1.Interface.IRepositry;

namespace V1.Service
{
    public class DoctorCourseService
    {
        private readonly IDoctorCrouse _context;
        public DoctorCourseService( IDoctorCrouse context)
        {
            _context = context;
        }
        public GeneralMsgDto AddDcotorCourse(AddDoctorCourseDto addDoctorCourseDto)
        {
            return _context.AddDcotorCourse(addDoctorCourseDto);
        }
        public List<GetDoctorCourseDto> GetAllDoctorCourse()
        {
            return _context.GetAllDoctorCourse();
        }
        public List<GetDoctorCourseDto> GetDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId)
        {
            return _context.GetDoctorCourseByDoctorIdAndCourseId(DoctorId, CourseId);
        }
        public List<GetDoctorCourseDto> GetDoctorCourseByCurrentAcademicYear(string CurrentAcademicYearName)
        {
            return _context.GetDoctorCourseByCurrentAcademicYear(CurrentAcademicYearName);
        }
        public GeneralMsgDto DeleteDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId)
        {
            return _context.DeleteDoctorCourseByDoctorIdAndCourseId(DoctorId, CourseId);
        }
        public GeneralMsgDto UpdateDoctorCourseByDoctorIdAndCourseId(UpdateDoctorCourseDto updateDoctorCourseDto, string DoctorId, int CourseId)
        {
            return _context.UpdateDoctorCourseByDoctorIdAndCourseId(updateDoctorCourseDto, DoctorId, CourseId); 
        }
        public List<GetSuperviseDoctorQDto> GetSupervisedDoctor()
        {
            return _context.GetSupervisedDoctor();

        }
    }
}

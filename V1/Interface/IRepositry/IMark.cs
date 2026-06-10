using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using V1.DTO.MarkDTO;

namespace V1.Interface.IRepositry
{
    public interface IMark
    {
        public GeneralMsgDto AddMarks(List<AddMarkDto> addMarkDto);
        public GeneralMsgDto DeleteMark(int MarkId);
        public GeneralMsgDto UpdateMark(UpdateMarkDto NewMarkDot, int MarkId);
        public List<GetMarkQDto> GetMarkById(int markId);
        public List<GetMarkQ1Dto> GetMarkByStudentsId(string StudentId);
        public List<GetMarkQDto> GetMarkBySCorseIdAndAcadimicYear(string AcademicYearName, int CouresId);
        public List<GetMarkQ3Dto> GetStudentsInOneCourseLastTheRotationByDoctorId(string DoctorId, string AcademicYear, string MarkType, int RotationId);
        public List<GetMarkQ2Dto> GetSumOfTheWeeklyEvaluation(string StudentsId);
        public List<GetMarkQ4Dto> GetStudentsForTheUpdate(string DoctorId, string AcademicYear , int RotationId, string MarkType);
        public List<GetMarkQ6Dto> GetMarkForTheStudentByIdAndCourseId(string StudentsId, int CourseId);
    }
}

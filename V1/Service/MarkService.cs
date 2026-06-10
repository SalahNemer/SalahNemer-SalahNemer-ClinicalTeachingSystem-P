using database.models;
using DataBase.entitys;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;
using V1.DTO.MarkDTO;
using V1.Interface.IRepositry;

namespace V1.Service
{
    public class MarkService
    {
        private readonly IMark _context;
        public MarkService(IMark mark)
        {
            _context = mark;
        }
        public GeneralMsgDto AddMarksService(List<AddMarkDto> addMarkDto)
        {
            return _context.AddMarks(addMarkDto);
        }
        public GeneralMsgDto DeleteMarkService(int MarkId)
        {
            return _context.DeleteMark(MarkId);
        }
        public GeneralMsgDto UpdateMarkService(UpdateMarkDto NewMarkDot, int MarkId)
        {
            return _context.UpdateMark(NewMarkDot, MarkId);
        }
        public List<GetMarkQDto> GetMarkByIdService(int markId)
        {
            return _context.GetMarkById(markId);
        }
        public List<GetMarkQ1Dto> GetMarkByStudentsIdService(string StudentId)
        {
            return _context.GetMarkByStudentsId(StudentId);
        }
        public List<GetMarkQDto> GetMarkBySCorseIdAndAcadimicYearService(string AcademicYearName, int CouresId)
        {
            return _context.GetMarkBySCorseIdAndAcadimicYear(AcademicYearName, CouresId);
        }
        public List<GetMarkQ3Dto> GetStudentsInOneCourseLastTheRotationByDoctorIdSerive(string DoctorId , string AcademicYear , string MarkType , int RotationId)
        {
            return _context.GetStudentsInOneCourseLastTheRotationByDoctorId(DoctorId , AcademicYear, MarkType,  RotationId);
        }
        public List<GetMarkQ2Dto> GetSumOfTheWeeklyEvaluation(string StudentsId)
        {
            return _context.GetSumOfTheWeeklyEvaluation(StudentsId);

        }
        public List<GetMarkQ4Dto> GetStudentsForTheUpdate(string DoctorId, string AcademicYear ,  int RotationId, string MarkType)
        {
            return _context.GetStudentsForTheUpdate(DoctorId, AcademicYear , RotationId, MarkType);

        }
        public List<GetMarkQ6Dto> GetMarkForTheStudentByIdAndCourseId(string StudentsId, int CourseId)
        {
            return _context.GetMarkForTheStudentByIdAndCourseId(StudentsId, CourseId);

        }

    }
}

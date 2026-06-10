using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.StudentsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using testDtoAndmapper.Entity;
using V1.DTO.StudentsDTO;

namespace DevetionStudetns.Service
{
    public class StudentsService
    {
        private readonly IStudents _context; 
        public StudentsService (IStudents context)
        {
            _context = context;
        }
        public List<GetStudentsQDto> GetStudentsById(string studentId)
        {
            return _context.getStudentsData(studentId);
        }

        public List<GetStudentsQ2Dto> GetAllStudentsInOneSubGroupByIdSerive (int subGroupId)
        {
            return _context.GetAllStudentsInOneSubGroupById(subGroupId);
        }
        public List<GetStudentsQDto> GetAllStudentsInSameLevelService(int Level)
        {
            return _context.GetAllStudentsInSameLevel(Level);

        }
        public List<GetStudentsQ1Dto> GetAllStudentsInSameLevelInAllSubGroupService(int Level)
        {
            return _context.GetAllStudentsInSameLevelInAllSubGroup(Level);
        }
        public GeneralMsgDto DeleteStudentsService(string StudentsId)
        {
            return _context.DeleteStudents(StudentsId); 
        }
        public GeneralMsgDto AddStudentdsService(StudentsAddDto studentsAddDto)
        {
            return _context.AddStudentds(studentsAddDto);
        }
        public List<GetStudentsQ1Dto> GetAllStudentsService()
        {
            return _context.GetAllStudents();
        }
        public GeneralMsgDto UpdateStudentsService(UpdateStudentsDto NewStudents, string studentsId)
        {
            return _context.UpdateStudents(NewStudents, studentsId);    
        }

        public async Task<GeneralMsgDto> UpdateStudentAndUserServes(UpdateStudentAndUserDto students, string studentsId)
        {
            return await _context.UpdateStudentAndUser(students, studentsId);
        }

    }
}

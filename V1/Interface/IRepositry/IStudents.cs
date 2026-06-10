using DevetionStudetns.DTO.StudentsDTO;
using FinalProject.DTO.StudentsDTO;
using loginpage.DBcon;
using V1.DTO.StudentsDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IStudents
    {
        public List<GetStudentsQDto> getStudentsData(string userId);
        public List<GetStudentsQ2Dto> GetAllStudentsInOneSubGroupById(int SubGroupId);
        public List<GetStudentsQDto> GetAllStudentsInSameLevel(int Level);
        public List<GetStudentsQ1Dto> GetAllStudentsInSameLevelInAllSubGroup(int Level);
        public GeneralMsgDto DeleteStudents(string StudentsId);
        public GeneralMsgDto AddStudentds(StudentsAddDto studentsAddDto);
        public List<GetStudentsQ1Dto> GetAllStudents();
        public GeneralMsgDto UpdateStudents(UpdateStudentsDto NewStudents, string studentsId);
        public Task<GeneralMsgDto> UpdateStudentAndUser(UpdateStudentAndUserDto student, string studentsId);

    }
}

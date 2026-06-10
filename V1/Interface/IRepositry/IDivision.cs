using DevetionStudetns.DTO.DivisionsDTO;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface IDivision
    {
        public GeneralMsgDto AddDivision(DivisionAddDto AddDivision);
        public GeneralMsgDto DeleteDivision(int DivisionId);
        public GeneralMsgDto UpdateDivision(DivisionAddDto NewData, int DivisionId);
        public List<GetDivisionDto> GetAllStudentsInOneSubGroupBySubGroupId(int SubGroupId);
        public List<GetDivisionDto> GetAllStudentsInOneMainByMainGroupId(int MainGroupId);
        public List<GetDivisionDto> GetAllStudentsInTheSameLevel(int Level);
        public List<GetDivisionDto> GetDataForStudentId(string StudentId);
    }
}

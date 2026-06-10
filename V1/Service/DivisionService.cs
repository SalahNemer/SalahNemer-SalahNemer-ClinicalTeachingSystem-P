using DataBase.DBcon;
using DevetionStudetns.DTO.DivisionsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class DivisionService
    {
        private readonly IDivision _context;
        private readonly DBC context;

        public DivisionService(IDivision con1 , DBC con)
        {
            context = con; 
            _context = con1;
        }

        public GeneralMsgDto AddDivisionService(DivisionAddDto AddDivision)
        {
            return _context.AddDivision(AddDivision);
        }
        public GeneralMsgDto DeleteDivisionService(int DivisionId)
        {
            var gitSubGroup = context.Divisions.FirstOrDefault(p => p.DivisionId == DivisionId);
            var getNumberOfStudentsInOneSubGroup = context.Divisions.Where(p=>p.SubGroupId == gitSubGroup.SubGroupId).ToList().Count - 1;
            var setNumberOfStudentInOneSubGroup = context.subGroups.FirstOrDefault(p => p.SubGroupId == gitSubGroup.SubGroupId);
            setNumberOfStudentInOneSubGroup.NumberOfStudetns = getNumberOfStudentsInOneSubGroup;
            context.SaveChanges();
            return _context.DeleteDivision(DivisionId);
        }
        public GeneralMsgDto UpdateDivisionService(DivisionAddDto NewData, int DivisionId)
        {
            var gitSubGroup = context.Divisions.FirstOrDefault(p => p.DivisionId == DivisionId);
            var getNumberOfStudentsInOneSubGroup = context.Divisions.Where(p => p.SubGroupId == gitSubGroup.SubGroupId).ToList().Count - 1;
            var setNumberOfStudentInOneSubGroup = context.subGroups.FirstOrDefault(p => p.SubGroupId == gitSubGroup.SubGroupId);
            setNumberOfStudentInOneSubGroup.NumberOfStudetns = getNumberOfStudentsInOneSubGroup;


            var GetSubGroup = context.subGroups.FirstOrDefault(p=>p.SubGroupId == NewData.SubGroupId);
            var UpdateNumberOfStudentsInOneSubGroup = context.Divisions.Where(p => p.SubGroupId == NewData.SubGroupId).ToList().Count - 1;
            GetSubGroup.NumberOfStudetns = UpdateNumberOfStudentsInOneSubGroup; 
            return _context.UpdateDivision(NewData, DivisionId);
        }
        public List<GetDivisionDto> GetAllStudentsInOneSubGroupBySubGroupIdService (int SubGroupId)
        {
            return _context.GetAllStudentsInOneSubGroupBySubGroupId(SubGroupId);
        }
        public List<GetDivisionDto> GetAllStudentsInOneMainByMainGroupIdService(int MainGroupId)
        {
            return _context.GetAllStudentsInOneMainByMainGroupId(MainGroupId);
        }
        public List<GetDivisionDto> GetAllStudentsInTheSameLevelService(int Level)
        {
            return _context.GetAllStudentsInTheSameLevel(Level);
        }
        public List<GetDivisionDto> GetDataForStudentIdService(string StudentId)
        {
            return _context.GetDataForStudentId(StudentId);
        }
    }
}

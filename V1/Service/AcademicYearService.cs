using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class AcademicYearService
    {
        private readonly IAcademicYear _context;
        public AcademicYearService (IAcademicYear context)
        {
            _context = context;
        }
        public List<GetAcademicYearDto> ShwoAcademicYear()
        {
            return _context.GetAllAcademicYearDto();
        }       
        public GeneralMsgDto AddAcademicYearDto(GetAcademicYearDto academicYear)
        {
            return _context.AddAcademicYearDto(academicYear);

        }
        public List<AcademicYear> GetAllAcademicYear()
        {
            return _context.GetAllAcademicYear();

        }
        public GeneralMsgDto DeleteAcademicYear(int AcadmicYearId)
        {
            return _context.DeleteAcademicYear(AcadmicYearId);

        }
    }
}

using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface IAcademicYear
    {
        public List<GetAcademicYearDto> GetAllAcademicYearDto();
        public GeneralMsgDto AddAcademicYearDto(GetAcademicYearDto academicYear);
        public List<AcademicYear> GetAllAcademicYear();
        public GeneralMsgDto DeleteAcademicYear(int AcadmicYearId);

    }
}

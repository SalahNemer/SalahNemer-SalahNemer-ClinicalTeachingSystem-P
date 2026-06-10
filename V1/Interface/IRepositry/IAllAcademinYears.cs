using loginpage.DBcon;
using V1.abed.AllAcademicYearDTO;

namespace V1.abed
{
    public interface IAllAcademinYears
    {
        public GeneralMsgDto AddCurrentAcademicYear(AddAllAcademicYearDto addAllAcademicYear);
        public GeneralMsgDto UpdateAcademicYear(AddAllAcademicYearDto NewAcademicYear, int AcademicYearId);
        public GeneralMsgDto DeleteAcademicYear(int AcademicYearId);
        public List<GetAllAcademicYearDto> GetAllAcademicYear();
        public List<GetAllAcademicYearDto> GetAcademicYearById(int AcademicYearId);
    }
}

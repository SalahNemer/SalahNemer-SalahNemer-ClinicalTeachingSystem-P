using loginpage.DBcon;
using V1.abed.AllAcademicYearDTO;

namespace V1.abed
{
    public class AllAcademinYearsSerivce
    {
        private readonly IAllAcademinYears _context;
        public AllAcademinYearsSerivce (IAllAcademinYears context)
        {
            _context = context;
        }
        public GeneralMsgDto AddCurrentAcademicYearSerivce(AddAllAcademicYearDto addAllAcademicYear)
        {
            return _context.AddCurrentAcademicYear(addAllAcademicYear);
        }
        public GeneralMsgDto UpdateAcademicYearSerivce(AddAllAcademicYearDto NewAcademicYear, int AcademicYearId)
        {
            return _context.UpdateAcademicYear(NewAcademicYear, AcademicYearId);
        }
        public GeneralMsgDto DeleteAcademicYearSerivce(int AcademicYearId)
        {
            return _context.DeleteAcademicYear(AcademicYearId);
        }
        public List<GetAllAcademicYearDto> GetAllAcademicYearSerivce()
        {
            return _context.GetAllAcademicYear();
        }
        public List<GetAllAcademicYearDto> GetAcademicYearByIdSerivce(int AcademicYearId)
        {
            return _context.GetAcademicYearById(AcademicYearId);
        }
    }
}

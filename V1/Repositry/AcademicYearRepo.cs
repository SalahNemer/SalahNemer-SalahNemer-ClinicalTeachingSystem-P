using DataBase.DBcon;
using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.DTO.firebase;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.AcademicYearMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace DevetionStudetns.Repositry.AcademicYearRepostry
{
    public class AcademicYearRepo : IAcademicYear
    {
        private readonly DBC _context;
        public AcademicYearRepo(DBC context)
        { 
            _context = context; 
        }

        public List<GetAcademicYearDto> GetAllAcademicYearDto()
        {
            List< AcademicYear > academicYears = _context.AcademicYears.ToList();
            List<GetAcademicYearDto> academicYearDtos = new List< GetAcademicYearDto >();
            if (academicYears == null)
            {
                return null;
            }
            else
            {
                foreach (AcademicYear academicYear in academicYears)
                {
                    academicYearDtos.Add(academicYear.ShwoAcademicYearMapper());
                }
                return academicYearDtos;
            }
        }

        public GeneralMsgDto AddAcademicYearDto (GetAcademicYearDto academicYear)
        {
            var getAcadmicYear = _context.AcademicYears.FirstOrDefault(p=>p.AcademicYearLevel == academicYear.AcademicYearLevel);
            if (academicYear.AcademicYearLevel >= 4 && academicYear.AcademicYearLevel <= 6 && getAcadmicYear == null )
            {
                try
                {

                    _context.AcademicYears.Add(academicYear.AddAcademicYearMap());
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                     SuccessfullyMsgs.SUCCESSFULLY_ADD_LEVEL,
                                     "Not Found",
                                     "not Found "
                                     );
                    return ErrorMsg;
                    
                }
                catch (Exception ex) {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                  IErrorMsgs.Error,
                                  "Not Found",
                                  "not Found "
                                  );
                    return  ErrorMsg;
                }
            }
            GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                              IErrorMsgs.NOT_FOUND_LEVEL,
                              "Not Found",
                              "not Found "
                              );
            return ErrorMsgs;
        }
        public GeneralMsgDto DeleteAcademicYear(int AcadmicYearId)
        {
            var getAcadmicYear = _context.AcademicYears.FirstOrDefault(p => p.AcademicYearLevel == AcadmicYearId);
            if (getAcadmicYear != null)
            {
                try
                {

                    _context.AcademicYears.Remove(getAcadmicYear);
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                     SuccessfullyMsgs.SUCCESSFULLY_DELETE_ACADIMIC_YEAR,
                                     "Not Found",
                                     "not Found "
                                     );
                    return ErrorMsg;

                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                  IErrorMsgs.Error,
                                  "Not Found",
                                  "not Found "
                                  );
                    return ErrorMsg;
                }
            }
            GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                              IErrorMsgs.NOT_FOUND_LEVEL,
                              "Not Found",
                              "not Found "
                              );
            return ErrorMsgs;
        }
        public List<AcademicYear>  GetAllAcademicYear()
        {
            var GetAcademicYear =  _context.AcademicYears.ToList();
            return GetAcademicYear; 
        } 
       
    }
}

using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using V1.abed.AllAcademicYearDTO;
using V1.abed.AllAcademicYearMapper;
using V1.DTO.DistributionDTO;

namespace V1.abed
{
    public class AllAcademicYearRepo : IAllAcademinYears
    {
        private readonly DBC _context;
        public AllAcademicYearRepo(DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto AddCurrentAcademicYear (AddAllAcademicYearDto addAllAcademicYear)
        {

            if ( addAllAcademicYear == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            else
            {
                var getDublicateData = _context.AllAcademinYears.Where(p=>p.CurrentAcademicYearName == addAllAcademicYear.CurrentAcademicYearName).ToList().Count;
                if (getDublicateData == 0)
                {
                    try
                    {
                        _context.AllAcademinYears.Add(addAllAcademicYear.AddAcademicYearsMappers());
                        _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                   SuccessfullyMsgs.SUCCESSFULLY_ADD_YEAR,
                                   "Successfully",
                                   "Successfully Add "
                                   );
                        return SucMsg;
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Error",
                                "Error"
                                );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.DUBLICATE_ACADEMIC_YEAR,
                                "DUBLICATE DATA ",
                                "DUBLICATE DATA"
                                );
                    return ErrorMsg;
                }
                 
            }

        }
        public GeneralMsgDto UpdateAcademicYear(AddAllAcademicYearDto NewAcademicYear, int AcademicYearId)
        {
            if (NewAcademicYear == null || AcademicYearId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            else
            {
                var OldAcademicYear = _context.AllAcademinYears.FirstOrDefault(p => p.CurrentAcademicYearId == AcademicYearId);
                if (OldAcademicYear != null)
                {
                    if (OldAcademicYear.CurrentAcademicYearName != NewAcademicYear.CurrentAcademicYearName)
                    {
                        var GetDublecateData = _context.AllAcademinYears.Where(p => p.CurrentAcademicYearName == NewAcademicYear.CurrentAcademicYearName).ToList().Count;
                        if (GetDublecateData == 0)
                        {
                            try
                            {
                            OldAcademicYear.CurrentAcademicYearName = NewAcademicYear.CurrentAcademicYearName;
                            _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                   SuccessfullyMsgs.SUCCESSFULLY_UPDATED_ACADEMIC_YEAR,
                                   "Successfully",
                                   "Successfully"
                                   );
                        return SucMsg;

                            }
                            catch(Exception ex) {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Error",
                                "Error"
                                );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.Duplicate_Data,
                           "Duplicate Data",
                           "Duplicate Data "
                           );
                            return ErrorMsg;
                        }
                    }
                    else
                    {         
                           GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.Duplicate_Data,
                           "Duplicate Data",
                           "Duplicate Data "
                           );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                           "Not Found",
                           "not Found "
                           );
                    return ErrorMsg;
                    

                }
            }

        }

        public GeneralMsgDto DeleteAcademicYear( int AcademicYearId)
        {
            if ( AcademicYearId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            else
            {
                var DeleteAcademicYear = _context.AllAcademinYears.FirstOrDefault(p => p.CurrentAcademicYearId == AcademicYearId);
                if (DeleteAcademicYear != null)
                { 
                    try
                    {
                    _context.AllAcademinYears.Remove(DeleteAcademicYear);
                    _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                   SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                   "SUCCESSFUL",
                                   "SUCCESSFUL"
                                   );
                        return SucMsg;
                       
                    }
                    catch ( Exception ) 
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.Error,
                        "Error",
                        "Error"
                        );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                           "Not Found",
                           "not Found "
                           );
                    return ErrorMsg;
                }

            }
        }
        public List<GetAllAcademicYearDto> GetAcademicYearById (int AcademicYearId)
        {
            try
            {
                if (AcademicYearId == null)
                {
                    return null;
                }
                else
                {
                    string sql = @"	 
                                select *
                                    from AllAcademicYear a 
                                    where a.CurrentAcademicYearId = @AcademicYearId
                            ";

                    var result = _context.Database.SqlQueryRaw<GetAllAcademicYearDto>(
                        sql, new SqlParameter("AcademicYearId", AcademicYearId)).ToList();
                    if (result == null)
                        return null;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetAllAcademicYearDto> GetAllAcademicYear()
        {
            try
            {
                    string sql = @"	 
                                select *
                                    from AllAcademicYear a 
                            ";
                    var result = _context.Database.SqlQueryRaw<GetAllAcademicYearDto>(
                        sql).ToList();
                    if (result == null)
                        return null;
                    return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

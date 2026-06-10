using BuildDB_Team.entitys;
using DataBase.DBcon;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.SubGroupDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.MainGroupMapoer;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevetionStudetns.Repositry.MainGroupReosetry
{
    public class MainGourpRepositry : IMainGroup
    {
        private readonly DBC _context;
        public MainGourpRepositry(DBC context)
        {
            _context = context;
        }


        public GeneralMsgDto AddMianGroupRepo(AddMainGroupDto mainGroupDto)
        {

            try
            {
                if (mainGroupDto == null)
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
                    var ValedationSympole = _context.mainGrops.Where(P => P.AcademicYearId == mainGroupDto.AcademicYearId && P.MainGroupSympole == mainGroupDto.MainGroupSympole && P.AcademicYearName == mainGroupDto.AcademicYearName).ToList().Count;
                    var GetAcadimicYear = _context.AcademicYears.FirstOrDefault(P => P.AcademicYearLevel == mainGroupDto.AcademicYearId);
                    if (GetAcadimicYear == null)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.FAILED_ENTER_LEVEL_EXITING_LIMITS,
                                             "Fialed",
                                             "There is not any Academic Level : " + mainGroupDto.AcademicYearId
                                             );
                        return ErrorMsg;
                    }
                    if (ValedationSympole != 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                 IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                                 "Enter the Courect sympole of the Main Group",
                                                 "Error in data entry. The data entered is duplcate"
                                                 );
                        return ErrorMsg;
                    }
                    else
                    {
                        var getAcademicYear = _context.AllAcademinYears.Where(p => p.CurrentAcademicYearName == mainGroupDto.AcademicYearName).ToList().Count;
                        if (getAcademicYear != 0)
                        {

                            try
                            {
                                _context.mainGrops.Add(mainGroupDto.AddMainGroupMap());
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.Error,
                                                      "Enter the Courect sympole of the Main Group",
                                                      "Error in data entry. The data entered is not included "
                                                      );
                                return ErrorMsg;
                            }
                            GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                             SuccessfullyMsgs.WORKSHOP_REGISTERED_SUCCESSFULLY,
                                             "Add Successfully",
                                             "You Are Add subGroup "
                                             );
                            return SuccessfullyMsg;
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                                                  "Not Found ",
                                                  "Not Found "
                                                  );
                            return ErrorMsg;
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepo(string acadimicYear, int level)
        {
            try
            {
                List<MainGrop> mainGrops = _context.mainGrops.Where(p => p.AcademicYearId == level && p.AcademicYearName == acadimicYear).ToList();
                if (mainGrops == null)
                {
                    return null;
                }
                List<GetMainGroupDto> mainGroupDtos = new List<GetMainGroupDto>();
                foreach (MainGrop getMainGroup in mainGrops)
                {
                    mainGroupDtos.Add(getMainGroup.ShowMainGourp());
                }
                return mainGroupDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepoForTheDistpution(string acadimicYear, int level)
        {
            try
            {
                string sql = @"
	                        SELECT DISTINCT 
                            m.MainGroupId,
                            m.MainGroupSympole,
                            m.AcademicYearName,
                            m.AcademicYearId
	                        FROM 
                            MainGroup m 
	                        LEFT JOIN DistributionsMainGroup d 
                            ON d.MainGroupId = m.MainGroupId
	                        WHERE 
                            d.RotationId IS NOT NULL
	                        and m.AcademicYearName =@acadimicYear
	                        and m.AcademicYearId = @level
                            ";

                var result = _context.Database.SqlQueryRaw<GetMainGroupDto>(
                                sql ,
                                new SqlParameter("acadimicYear", acadimicYear),
                                new SqlParameter("level", level)
                                ).ToList();

                if (result == null)
                {
                    return null;
                }
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }



        public GetMainGroupDto GetMianGroupBySemolyRepo(int getMainGroupById)
        {
            try
            {
                var mainGrops = _context.mainGrops.FirstOrDefault(p => p.MainGroupId == getMainGroupById);
                if (mainGrops == null)
                {
                    return null;
                }
                GetMainGroupDto mainGroupDtos = mainGrops.ShowMainGourp();

                return mainGroupDtos;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public GeneralMsgDto DeleteMainGroup(int mainGroupId)
        {
            try
            {
                if (mainGroupId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.INVALID_DATA_FORMAT,
                    "Enter the requird filled",
                               Convert.ToString(mainGroupId)
                               );
                    return ErrorMsg;
                }
                else
                {
                    MainGrop DeleteMainGroup = _context.mainGrops.FirstOrDefault(p => p.MainGroupId == mainGroupId);
                    if (DeleteMainGroup != null)
                    {
                        try
                        {

                        _context.mainGrops.Remove(DeleteMainGroup);
                        _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                       SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                       "Successfully Delete",
                                                       "You are delete this Distributions Main Group "
                                                       );
                        return SucMsg;
                        }
                        catch (Exception ex)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_DATA,
                               "Enter the requird filled",
                               "Ther is not any data to delete it "
                               );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_DATA,
                               "Enter the requird filled",
                               "Ther is not any data to delete it "
                               );
                        return ErrorMsg;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public GeneralMsgDto UpdateMainGroupRepo(AddMainGroupDto NewDataMainGoup, int MainGroupId)
        {
            try
            {
                if (NewDataMainGoup == null || MainGroupId == null)
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
                    var ValedationSympole = _context.mainGrops.Where(P => P.AcademicYearId == NewDataMainGoup.AcademicYearId && P.MainGroupSympole == NewDataMainGoup.MainGroupSympole && P.AcademicYearName == NewDataMainGoup.AcademicYearName).ToList().Count;
                    var GetAcadimicYear = _context.AcademicYears.FirstOrDefault(P => P.AcademicYearLevel == NewDataMainGoup.AcademicYearId);
                    if (GetAcadimicYear == null)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.FAILED_ENTER_LEVEL_EXITING_LIMITS,
                                             "Fialed",
                                             "There is not any Academic Level : " + NewDataMainGoup.AcademicYearId
                                             );
                        return ErrorMsg;
                    }
                    if (ValedationSympole >= 1)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                 IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                                 "Enter the Courect sympole of the Main Group",
                                                 "Error in data entry. The data entered is duplcate"
                                                 );
                        return ErrorMsg;
                    }
                    else
                    {
                        var oldMainGroup = _context.mainGrops.FirstOrDefault(p => p.MainGroupId == MainGroupId);
                        if (oldMainGroup == null)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.INVALID_DATA_FORMAT,
                                                  "Enter the Courect sympole of the Main Group",
                                                  "Error in data entry. The data entered is not included "
                                                  );
                            return ErrorMsg;
                        }
                        else
                        {
                            var getAcademicYear = _context.AllAcademinYears.Where(p => p.CurrentAcademicYearName == NewDataMainGoup.AcademicYearName).ToList().Count;
                            if (getAcademicYear != 0)
                            {
                                try
                                {
                                    oldMainGroup.AcademicYearId = NewDataMainGoup.AcademicYearId;
                                    oldMainGroup.MainGroupSympole = NewDataMainGoup.MainGroupSympole;
                                    _context.SaveChanges();

                                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                                 SuccessfullyMsgs.SUCCESSFULLY_UPDATED_MAIN_GROUP,
                                                 "Update Successfully",
                                                 "You Are Update Main Group "
                                                 );
                                    return SuccessfullyMsg;

                                }
                                catch (Exception ex)
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.Error,
                                                          "Error ",
                                                          "Error "
                                                          );
                                    return ErrorMsg;
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                                                      "Not Found ",
                                                      "Not Found "
                                                      );
                                return ErrorMsg;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }

        public string getMainGroupSympole(int mainGroupid)
        {
            try
            {
                var getSympole = _context.mainGrops.Find(mainGroupid);
                var SympoleMainGroup = getSympole.MainGroupSympole;
                return SympoleMainGroup;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }


    }
}
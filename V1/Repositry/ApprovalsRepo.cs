using BuildDB_Team.entitys;
using database.models;
using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.ApprovalsDTO;
using V1.DTO.MarkDTO;
using V1.Interface.IRepositry;

namespace V1.Repositry
{
    public class ApprovalsRepo : IApprovals
    {
        private readonly DBC _context;
        public ApprovalsRepo(DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto ApprovalsForTheDivision(string CurrentAcadmicYear )
        {
            var GetAllDivisionForTheApprovals = _context.Divisions.Where(p => p.DivisionStatus == 1 && p.SubGroup.MainGrop.AcademicYearName == CurrentAcadmicYear).ToList();
            if (GetAllDivisionForTheApprovals.Any()) 
            {
                try
                {
                    foreach (var division in GetAllDivisionForTheApprovals)
                    {
                        division.DivisionStatus = 2;
                    }
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_DIVISOIN,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_DIVISION,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }

        }
        public GeneralMsgDto ReSendApprovalsForTheDivision(string CurrentAcadmicYear)
        {
            var GetAllDivisionForTheApprovals = _context.Divisions.Where(p => p.DivisionStatus == 4 && p.SubGroup.MainGrop.AcademicYearName == CurrentAcadmicYear).ToList();
            if (GetAllDivisionForTheApprovals.Any()) 
            {
                try
                {
                    foreach (var division in GetAllDivisionForTheApprovals)
                    {
                        division.DivisionStatus = 2;
                    }
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_DIVISOIN,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_DIVISION,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }

        }
        
        public List<GetApprovalsQDto> GetListOfDivisionForTheApprovles(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    s.DivisionId,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    s.StudentId,
							    u.FullName,
							    u.Gender,
							    w.StudentLevel
							    from Divisions s 
							    left join SubGroup a on a.SubGroupId = s.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Students w on w.UserId = s.StudentId 
							    left join Users u on u.UserId = w.UserId 
							    where s.DivisionStatus = 2 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQDto>(
                    sql, 
                    new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetApprovalsQDto> GetListOfDivisionAfterTheApprovles(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    s.DivisionId,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    s.StudentId,
							    u.FullName,
							    u.Gender,
							    w.StudentLevel
							    from Divisions s 
							    left join SubGroup a on a.SubGroupId = s.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Students w on w.UserId = s.StudentId 
							    left join Users u on u.UserId = w.UserId 
							    where s.DivisionStatus = 1 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQDto>(
                    sql,
                    new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public GeneralMsgDto UpdateApprovalsForTheDivision(UpdateApprovalsDto updateApprovalsDto ,string AcademicYearName)
        {
            var GetAllDivisionForTheApprovals = _context.Divisions.Where(p => p.DivisionStatus == 2 && p.SubGroup.MainGrop.AcademicYearName == AcademicYearName).ToList();
            if (updateApprovalsDto.DivisionStatus != 3 && updateApprovalsDto.DivisionStatus != 4)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.FIALD_INPUT,
                                "Not Found",
                                "Not Found"
                                );
                return ErrorMsg;
            }
            else
            {
                if (GetAllDivisionForTheApprovals.Any()) 
                {
                    try
                    {
                        foreach (var division in GetAllDivisionForTheApprovals)
                        {
                            division.DivisionStatus = updateApprovalsDto.DivisionStatus;
                            division.Notes = updateApprovalsDto.Notes;
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Error,
                                    "Not Found",
                                    "Not Found"
                                    );
                        return ErrorMsg;
                    }
                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                          SuccessfullyMsgs.SUCCESSFULLY_APPROVLES_DIVISOIN,
                                          "Successfully",
                                          "Successfully "
                                          );
                    return SuccessfullyMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.NOT_FOUND_ANY_DIVISION,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
            }

        }

        public List<GetApprovalsQ1Dto> DivisionsNotApprovedByTheDean(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    s.DivisionId,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    s.StudentId,
							    u.FullName,
							    u.Gender,
							    w.StudentLevel,
							    s.Notes
							    from Divisions s 
							    left join SubGroup a on a.SubGroupId = s.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Students w on w.UserId = s.StudentId 
							    left join Users u on u.UserId = w.UserId 
							    where s.DivisionStatus = 4 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQ1Dto>(
                    sql
                    , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetApprovalsQDto> DivisionsApprovedByTheDean(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    s.DivisionId,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    s.StudentId,
							    u.FullName,
							    u.Gender,
							    w.StudentLevel
							    from Divisions s 
							    left join SubGroup a on a.SubGroupId = s.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Students w on w.UserId = s.StudentId 
							    left join Users u on u.UserId = w.UserId 
							    where s.DivisionStatus = 3 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQDto>(
                    sql
                    , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public GeneralMsgDto ApprovalsForTheDistbution(string AcademicYearName)
        {
            var GetAllDistributionForTheApprovals = _context.Distributions.Where(p => p.DistributionStatus == 1 && p.SubGroup.MainGrop.AcademicYearName == AcademicYearName).ToList();
            if (GetAllDistributionForTheApprovals.Any())
            {
                try
                {
                    foreach (var distribution in GetAllDistributionForTheApprovals)
                    {
                        distribution.DistributionStatus = 2;
                    }
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_Distbution,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_DISTBUTION,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }

        }

        public GeneralMsgDto ReSendApprovalsForTheDistbution(string AcademicYearName)
        {
            var GetAllDistributionForTheApprovals = _context.Distributions.Where(p => p.DistributionStatus == 4 && p.SubGroup.MainGrop.AcademicYearName == AcademicYearName).ToList();
            if (GetAllDistributionForTheApprovals.Any())
            {
                try
                {
                    foreach (var distribution in GetAllDistributionForTheApprovals)
                    {
                        distribution.DistributionStatus = 2;
                    }
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_Distbution,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_DISTBUTION,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }

        }
        
        public List<GetApprovalsQ2Dto> GetListOfDistributionAfterTheApprovles(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    d.DistributionId ,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    d.DoctorId,
							    u.FullName,
							    h.HospitalName,
							    q.DepartmentName,
							    c.CourseName,
							    r.RotationName,
							    r.StartRotationDate,
							    r.EndRotationDate,
							    t.WeekName,
							    t.StartSessionDate,
							    t.EndSessionDate
							    from Distributions d 
							    left join SubGroup a on a.SubGroupId = d.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Doctors s on s.UserId = d.DoctorId
							    left join Users u on u.UserId = s.UserId 
							    left join Hospitals h on h.HospitalId = s.HospitalId 
							    left join Department q on q.DepartmentId = s.DepartmentId
							    left join Course c on c.CouresId = d.CourseId 
							    left join Appointments t on t.AppointmentId = d.AppointmentId 
							    left join Rotations r on r.RotationId = t.RotationId
							    where d.DistributionStatus = 1 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQ2Dto>(
                    sql
                    , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovalsQ2Dto> GetListOfDistributionForTheApprovles(string AcademicYearName)
        {
            try
            {
            var sql = @"
                            select 
							d.DistributionId ,
							m.MainGroupSympole,
							a.SubGroupSympole,
							d.DoctorId,
							u.FullName,
							h.HospitalName,
							q.DepartmentName,
							c.CourseName,
							r.RotationName,
							r.StartRotationDate,
							r.EndRotationDate,
							t.WeekName,
							t.StartSessionDate,
							t.EndSessionDate
							from Distributions d 
							left join SubGroup a on a.SubGroupId = d.SubGroupId
							left join MainGroup m on m.MainGroupId = a.MainGroupId
							left join Doctors s on s.UserId = d.DoctorId
							left join Users u on u.UserId = s.UserId 
							left join Hospitals h on h.HospitalId = s.HospitalId 
							left join Department q on q.DepartmentId = s.DepartmentId
							left join Course c on c.CouresId = d.CourseId 
							left join Appointments t on t.AppointmentId = d.AppointmentId 
							left join Rotations r on r.RotationId = t.RotationId
							where d.DistributionStatus = 2 
                            and m.AcademicYearName = @AcademicYearName
                    ";
            var result = _context.Database.SqlQueryRaw<GetApprovalsQ2Dto>(
                sql
                , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
            return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public GeneralMsgDto UpdateApprovalsForTheDistribution(UpdateApprovals1Dto updateApprovalsDto , string AcademicYearName)
        {
            var GetAllDistributionForTheApprovals = _context.Distributions.Where(p => p.DistributionStatus == 2 && p.SubGroup.MainGrop.AcademicYearName == AcademicYearName).ToList();
            if (updateApprovalsDto.DistributionStatus != 3 && updateApprovalsDto.DistributionStatus != 4)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.FIALD_INPUT,
                                "Not Found",
                                "Not Found"
                                );
                return ErrorMsg;
            }
            else
            {
                if (GetAllDistributionForTheApprovals.Any())
                {
                    try
                    {
                        foreach (var Distribution in GetAllDistributionForTheApprovals)
                        {
                            Distribution.DistributionStatus = updateApprovalsDto.DistributionStatus;
                            Distribution.Notes = updateApprovalsDto.Notes;
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Error,
                                    "Not Found",
                                    "Not Found"
                                    );
                        return ErrorMsg;
                    }
                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                          SuccessfullyMsgs.SUCCESSFULLY_APPROVLES_Distbution,
                                          "Successfully",
                                          "Successfully "
                                          );
                    return SuccessfullyMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.NOT_FOUND_ANY_DISTBUTION,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
            }
        }
        public List<GetApprovalsQ3Dto> DistributionNotApprovedByTheDean(string AcademicYearName)
        {
            try
            {
                var sql = @"
                               select 
							    d.DistributionId ,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    d.DoctorId,
							    u.FullName,
							    h.HospitalName,
							    q.DepartmentName,
							    c.CourseName,
							    r.RotationName,
							    r.StartRotationDate,
							    r.EndRotationDate,
							    t.WeekName,
							    t.StartSessionDate,
							    t.EndSessionDate,
							    d.Notes
							    from Distributions d 
							    left join SubGroup a on a.SubGroupId = d.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Doctors s on s.UserId = d.DoctorId
							    left join Users u on u.UserId = s.UserId 
							    left join Hospitals h on h.HospitalId = s.HospitalId 
							    left join Department q on q.DepartmentId = s.DepartmentId
							    left join Course c on c.CouresId = d.CourseId 
							    left join Appointments t on t.AppointmentId = d.AppointmentId 
							    left join Rotations r on r.RotationId = t.RotationId
							    where d.DistributionStatus = 4 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQ3Dto>(
                    sql , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovalsQ2Dto> DistributionApprovedByTheDean(string AcademicYearName)
        {
            try
            {
                var sql = @"
                                select 
							    d.DistributionId ,
							    m.MainGroupSympole,
							    a.SubGroupSympole,
							    d.DoctorId,
							    u.FullName,
							    h.HospitalName,
							    q.DepartmentName,
							    c.CourseName,
							    r.RotationName,
							    r.StartRotationDate,
							    r.EndRotationDate,
							    t.WeekName,
							    t.StartSessionDate,
							    t.EndSessionDate
							    from Distributions d 
							    left join SubGroup a on a.SubGroupId = d.SubGroupId
							    left join MainGroup m on m.MainGroupId = a.MainGroupId
							    left join Doctors s on s.UserId = d.DoctorId
							    left join Users u on u.UserId = s.UserId 
							    left join Hospitals h on h.HospitalId = s.HospitalId 
							    left join Department q on q.DepartmentId = s.DepartmentId
							    left join Course c on c.CouresId = d.CourseId 
							    left join Appointments t on t.AppointmentId = d.AppointmentId 
							    left join Rotations r on r.RotationId = t.RotationId
							    where d.DistributionStatus = 3 
                                and m.AcademicYearName = @AcademicYearName
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovalsQ2Dto>(
                    sql , new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public GeneralMsgDto SendMarkToTheDepartmetnsHead( string DoctorId )
        {
            var GetAllMarkForTheApprovals = _context.Marks.Where(p => p.MarkStatus == 1 && p.DoctorId == DoctorId).ToList();
            if (GetAllMarkForTheApprovals.Any()) 
            {
                try
                {
                    foreach (var Mark in GetAllMarkForTheApprovals)
                    {
                        Mark.MarkStatus = 2;
                    }
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_MARK_FOR_THE_DEPARTMENT_HEAD,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_MARK,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }
        }
        public GeneralMsgDto ReSendMarkToTheDepartmetnsHead(string DoctorId )
        {
            var GetAllMarkForTheApprovals = _context.Marks.Where(p => p.MarkStatus == 8 || p.MarkStatus == 9 || p.MarkStatus == 10 && p.DoctorId == DoctorId).ToList();
            if (GetAllMarkForTheApprovals.Any()) 
            {
                try
                {
                    foreach (var Mark in GetAllMarkForTheApprovals)
                    {
                        Mark.MarkStatus = 2;
                    }
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.SUCCESSFULLY_SEND_TO_APPROVLES_MARK_FOR_THE_DEPARTMENT_HEAD,
                                      "Successfully",
                                      "Successfully "
                                      );
                return SuccessfullyMsg;
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_ANY_MARK,
                            "Not Found",
                            "Not Found"
                            );
                return ErrorMsg;
            }
        }
        public  GeneralMsgDto UpdateApprovalsForTheMark(UpdateApprovalsDto updateApprovalsDto , int markId )
        {
            var GetAllMarkForTheApprovals = _context.Marks.FirstOrDefault(p => p.MarkId == markId);
            if (updateApprovalsDto.DivisionStatus < 1 && updateApprovalsDto.DivisionStatus > 11)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.FIALD_INPUT,
                                "Not Found",
                                "Not Found"
                                );
                return ErrorMsg;
            }
            else
            {
                if (GetAllMarkForTheApprovals != null ) 
                {
                    try
                    {
                        GetAllMarkForTheApprovals.MarkStatus = updateApprovalsDto.DivisionStatus;
                        GetAllMarkForTheApprovals.Comments = updateApprovalsDto.Notes;                      
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Error,
                                    "Not Found",
                                    "Not Found"
                                    );
                        return ErrorMsg;
                    }
                    if (updateApprovalsDto.DivisionStatus == 5 || updateApprovalsDto.DivisionStatus == 6 || updateApprovalsDto.DivisionStatus == 7)
                    {
                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                          SuccessfullyMsgs.SUCCESSFULLY_APPROVLES_MARK,
                                          "Successfully",
                                          "Successfully "
                                          );
                    return SuccessfullyMsg;
                    }
                    else
                    {
                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                          SuccessfullyMsgs.SUCCESSFULLY_DESAPPROVLES_MARK,
                                          "Successfully",
                                          "Successfully "
                                          );
                        return SuccessfullyMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.NOT_FOUND_ANY_MARK,
                                "Not Found",
                                "Not Found"
                                );
                    return ErrorMsg;
                }
            }

        }

        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDepartmentHead(string DepartmentHeadId)
        {
            try
            {
                var sql = @"
                             SELECT 
                                m.markId,
                                m.DoctorId AS DoctorId, 
                                do.FullName AS DoctorName,
                                m.StudentId AS StudentsId,
                                std.FullName AS StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                            FROM Marks m
                            JOIN Doctors doc ON doc.UserId = m.DoctorId 
                            JOIN Users do ON do.UserId = m.DoctorId 
                            JOIN Users std ON std.UserId = m.StudentId  
                            JOIN Course c ON c.CouresId = m.CourseId 
                            JOIN Department d ON d.DepartmentId = doc.DepartmentId
                            WHERE m.MarkStatus = 2 
                            AND d.DepartmentId = (SELECT head.DepartmentId FROM Doctors head WHERE head.UserId = @DepartmentHeadId);

                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql,
                    new SqlParameter("DepartmentHeadId", DepartmentHeadId)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDeprtmentHead()
        {
            try
            {
                var sql = @"
                                select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 8

                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDeprtmentHead()
        {
            try
            {
                var sql = @"
                               select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 5
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheClinicalDepartmentDirector()
        {
            try
            {
                var sql = @"
                              select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 5 
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheClinicalDepartmentDirector()
        {
            try
            {
                var sql = @"
                                select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 9
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheClinicalDepartmentDirector()
        {
            try
            {
                var sql = @"
                               select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 6
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
     
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDean()
        {
            try
            {
                var sql = @"
                              select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus =  6
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDean()
        {
            try
            {
                var sql = @"
                                select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 10
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDean()
        {
            try
            {
                var sql = @"
                               select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 7
                        ";
                var result = _context.Database.SqlQueryRaw<GetApprovlesQ4Dto>(
                    sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetMarkQ5Dto> GetMarkNotApprovedToTheDoctor(string doctorId)
        {
            try
            {
                var sql = @"
                               select 
                                m.markId,
                                m.DoctorId as DoctorId, 
                                do.FullName as DoctorName,
                                m.StudentId as StudentsId,
                                std.FullName as StudentsName,
                                c.CourseCode,
                                c.CourseName,
                                m.MarkType,
                                m.Comments,
                                m.Mark
                                from Marks m
                                join Users do on do.UserId = m.DoctorId
                                join Users std on std.UserId = m.StudentId
                                join Course c on c.CouresId = m.CourseId
                                where m.MarkStatus = 8
                                or m.MarkStatus = 9
                                or m.MarkStatus = 10
                                and m.DoctorId =@p0 
                        ";
                var result = _context.Database.SqlQueryRaw<GetMarkQ5Dto>(
                    sql , doctorId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetMarkQ5Dto> GetMarkApprovedToTheDoctor(string doctorId )
        {
            try
            {
                var sql = @"
                           select 
                            m.markId,
                            m.DoctorId as DoctorId, 
                            do.FullName as DoctorName,
                            m.StudentId as StudentsId,
                            std.FullName as StudentsName,
                            c.CourseCode,
                            c.CourseName,
                            m.MarkType,
                            m.Comments,
                            m.Mark
                            from Marks m
                            join Users do on do.UserId = m.DoctorId
                            join Users std on std.UserId = m.StudentId
                            join Course c on c.CouresId = m.CourseId
                            where m.MarkStatus = 5
                            or m.MarkStatus = 6
                            or m.MarkStatus = 7
                            and m.DoctorId =@p0 
                    ";
                var result = _context.Database.SqlQueryRaw<GetMarkQ5Dto>(
                    sql, doctorId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
    }
}



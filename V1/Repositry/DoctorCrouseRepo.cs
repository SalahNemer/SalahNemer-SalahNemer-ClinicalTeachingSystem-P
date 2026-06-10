using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.DoctorCrouseDTO;
using V1.Interface.IRepositry;
using V1.Mappers.DoctorCourseMapper;

namespace V1.Repositry
{
    public class DoctorCrouseRepo : IDoctorCrouse
    {
        private readonly DBC _context;
        public DoctorCrouseRepo(DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto AddDcotorCourse(AddDoctorCourseDto addDoctorCourseDto)
        {
            try
            {
                if (addDoctorCourseDto == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                 IErrorMsgs.INVALID_DATA_FORMAT,
                                                 "Enter the Courect Data ",
                                                 "Enter Courect Data for Add Division "
                                                 );
                    return ErrorMsg;
                }
                var ValedationDocotor = _context.doctors.Where(p => p.UserId == addDoctorCourseDto.DoctorId).ToList().Count;
                var ValedationCourse = _context.Courses.Where(p => p.CouresId == addDoctorCourseDto.CourseId).ToList().Count;
                var ValedationCurrentAcadimicYear = _context.AllAcademinYears.Where(p => p.CurrentAcademicYearName == addDoctorCourseDto.CurrentAcademicYearName).ToList().Count;
                var ValedationDublecateData = _context.Doctor_Course.Where(p => p.DoctorId == addDoctorCourseDto.DoctorId && p.CourseId == addDoctorCourseDto.CourseId && p.CurrentAcademicYearName == addDoctorCourseDto.CurrentAcademicYearName).ToList().Count;
                int sumOfValedation = ValedationDocotor + ValedationCourse + ValedationCurrentAcadimicYear;
                if (sumOfValedation == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND + IErrorMsgs.COURSE_RECORD_NOT_FOUND + IErrorMsgs.NOT_FOUND_YEAT,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationDocotor == 0 && ValedationCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND + IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationDocotor == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;

                }
                if (ValedationCurrentAcadimicYear == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.NOT_FOUND_YEAT,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationDublecateData != 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DUBLECATE_DATA,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                else
                {
                    var getIfDocotrSupperviseAboveOneCouse = _context.Doctor_Course.Where(p => p.DoctorId == addDoctorCourseDto.DoctorId && p.CurrentAcademicYearName == addDoctorCourseDto.CurrentAcademicYearName).ToList().Count ;
                    if (getIfDocotrSupperviseAboveOneCouse == 0)
                    {
                        try
                        {
                            _context.Doctor_Course.Add(addDoctorCourseDto.AddDoctorCrouse());
                            _context.SaveChanges();
                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                SuccessfullyMsgs.SUCCESSFULLY_ADD_DOCTOT_TO_THE_COURSE,
                                "SUCCESSFULLY",
                                "SUCCESSFULLY Add "
                                );
                            return SucMsg;
                        }
                        catch (Exception ex)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                        IErrorMsgs.Error,
                                                                        "Enter the Courect Data ",
                                                                        "Enter Courect Data for Add Division "
                                                                        );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                        IErrorMsgs.DUBLECATE_DOCTOR_ABOVE_ONE_COURSE,
                                                                        "Enter the Courect Data ",
                                                                        "Enter Courect Data for Add Division "
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
        public List<GetDoctorCourseDto> GetAllDoctorCourse()
        {
            try
            {
                var sql = @"
                            select 
	                        doCo.DoctorId ,
	                        u.FullName as DoctorName,
	                        do.MedicalSpecialty,
	                        co.CourseCode,
	                        co.CourseName
	                        from Doctor_Course doCo
	                        join Doctors do on do.UserId = doCo.DoctorId
	                        join Course co on co.CouresId = doCo.Cours
	                        join Users u on u.UserId = do.UserId
                          ";
                var result = _context.Database.SqlQueryRaw<GetDoctorCourseDto>(
                    sql).ToList();
                if ( result.Count != 0 ) 
                    return result;
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDoctorCourseDto> GetDoctorCourseByDoctorIdAndCourseId(string DoctorId , int CourseId )
        {
            try
            {
                var sql = @"
                            select 
	                        doCo.DoctorId ,
	                        u.FullName as DoctorName,
	                        do.MedicalSpecialty,
	                        co.CourseCode,
	                        co.CourseName
	                        from Doctor_Course doCo
	                        join Doctors do on do.UserId = doCo.DoctorId
	                        join Course co on co.CouresId = doCo.Cours
	                        join Users u on u.UserId = do.UserId
	                        where doCo.Cours = @CourseId
	                        and doco.DoctorId = @DoctorId
                          ";
                var result = _context.Database.SqlQueryRaw<GetDoctorCourseDto>(
                    sql,
                    new SqlParameter("CourseId", CourseId),
                    new SqlParameter("DoctorId", DoctorId)).ToList();
                if (result.Count != 0)
                    return result;
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDoctorCourseDto> GetDoctorCourseByCurrentAcademicYear(string CurrentAcademicYearName)
        {
            try
            {
                var sql = @"
                            select 
	                        doCo.DoctorId ,
	                        u.FullName as DoctorName,
	                        do.MedicalSpecialty,
	                        co.CourseCode,
	                        co.CourseName
	                        from Doctor_Course doCo
	                        join Doctors do on do.UserId = doCo.DoctorId
	                        join Course co on co.CouresId = doCo.Cours
	                        join Users u on u.UserId = do.UserId
	                        where doCo.CurrentAcademicYearName =@CurrentAcademicYearName
                          ";
                var result = _context.Database.SqlQueryRaw<GetDoctorCourseDto>(
                    sql,
                    new SqlParameter("CurrentAcademicYearName", CurrentAcademicYearName)).ToList();
                if (result.Count != 0)
                    return result;
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public GeneralMsgDto DeleteDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId)
        {
            try
            {
                var getDoctorCouseFroTheDeleteByDoctorIdAndCourseId = _context.Doctor_Course.FirstOrDefault(p => p.DoctorId == DoctorId && p.CourseId == CourseId);
                if (getDoctorCouseFroTheDeleteByDoctorIdAndCourseId == null)
                {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.NOT_FOUND_CORSE_DOCTOR,
                                        "NOT found  ",
                                        "NOT found "
                                        );
                return ErrorMsg;
                }
                else
                {
                    try
                    {
                        _context.Doctor_Course.Remove(getDoctorCouseFroTheDeleteByDoctorIdAndCourseId);
                        _context.SaveChanges();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                            SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                            "SUCCESSFULLY",
                                            "SUCCESSFULLY delete "
                                            );
                    return SucMsg;
                    }
                    catch (Exception e) {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.Error,
                                            "error  ",
                                            "error "
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
        public GeneralMsgDto UpdateDoctorCourseByDoctorIdAndCourseId(UpdateDoctorCourseDto updateDoctorCourseDto, string DoctorId, int CourseId)
        {

                if (updateDoctorCourseDto == null && DoctorId == null && CourseId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                 IErrorMsgs.INVALID_DATA_FORMAT,
                                                 "Enter the Courect Data ",
                                                 "Enter Courect Data for Add Division "
                                                 );
                    return ErrorMsg;
                }
                var ValedationDocotor = _context.doctors.Where(p => p.UserId == updateDoctorCourseDto.DoctorId).ToList().Count;
                var ValedationCourse = _context.Courses.Where(p => p.CouresId == updateDoctorCourseDto.CourseId).ToList().Count;
                var ValedationDublecateData = _context.Doctor_Course.Where(p => p.DoctorId == updateDoctorCourseDto.DoctorId && p.CourseId == updateDoctorCourseDto.CourseId).ToList().Count;
                int sumOfValedation = ValedationDocotor + ValedationCourse ;
                if (sumOfValedation == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND + IErrorMsgs.COURSE_RECORD_NOT_FOUND + IErrorMsgs.NOT_FOUND_YEAT,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationDocotor == 0 && ValedationCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND + IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationDocotor == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DOCTOR_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                if (ValedationCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;

                }
                if (ValedationDublecateData != 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.DUBLECATE_DATA,
                                                "Enter the Courect Data ",
                                                "Enter Courect Data for Add Division "
                                                );
                    return ErrorMsg;
                }
                else
                {
                    var getDoctorCourse = _context.Doctor_Course.FirstOrDefault(p => p.DoctorId == DoctorId && p.CourseId == CourseId);
                        if (getDoctorCourse != null)
                        {

                                    getDoctorCourse.CourseId = updateDoctorCourseDto.CourseId;
                                    getDoctorCourse.DoctorId = updateDoctorCourseDto.DoctorId;
                                    _context.SaveChanges(); 
                                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                        SuccessfullyMsgs.SUCCESSFULLY_UPDATE_DOCTOT_TO_THE_COURSE,
                                                        "SUCCESSFULLY",
                                                        "SUCCESSFULLY Add " );
                                    return SucMsg;
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                    IErrorMsgs.NOT_FOUND_CORSE_DOCTOR,
                                                    "Enter the Courect Data ",
                                                    "Enter Courect Data for Add Division "
                                                    );
                            return ErrorMsg;
                        }
                }

            }

        public List<GetSuperviseDoctorQDto> GetSupervisedDoctor()
        {

            string sql = @"	 
                      select 
                       a.UserId ,
                       u.FullName ,
                       a.HospitalId,
                       a.DepartmentId,
                       a.MedicalSpecialty ,
                       a.YearOfObtainingTheCertificate,
                       a.AcademicDegree,
                       a.TheCountryYouGraduatedFrom,
                       a.TheUniversityFromWhichHeObtainedHisLastDegree,
                       a.YearsExperience
                       from Doctors a 
                       join Users u on u.UserId = a.UserId 
                       where u.RoulName = 9 or u.RoulName = 10 or u.RoulName = 11 or u.RoulName = 12 
                       and u.AccountStatus = 1 
                        ";
            var result = _context.Database.SqlQueryRaw<GetSuperviseDoctorQDto>(sql).ToList();
            if (result == null)
                return null;
            return result;

        }
    }
}

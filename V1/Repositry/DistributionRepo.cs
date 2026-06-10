using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.DistributionsDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.DistubutionsMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using V1.DTO.AppointmentDTO;
using V1.DTO.DistributionDTO;

namespace DevetionStudetns.Repositry.AddDistributionRepositry
{
    public class DistributionRepo : IDistribution
    {
        private readonly DBC _context;
        public DistributionRepo(DBC dBC)
        {
            _context = dBC;
        }
        public GeneralMsgDto AddDistribution(AddDistibutionsDto addDistibutionsDto)
        {
            try
            {
                if (addDistibutionsDto == null)
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
                    var GetSubGroupById = _context.subGroups.Where(p => p.SubGroupId == addDistibutionsDto.SubGroupId).ToList().Count;
                    var GetDoctorById = _context.doctors.Where(p => p.UserId == addDistibutionsDto.DoctorId).ToList().Count;
                    var GetCourseById = _context.Courses.Where(p => p.CouresId == addDistibutionsDto.CourseId).ToList().Count;
                    var GetRotationById = _context.Rotations.Where(p => p.RotationId == addDistibutionsDto.RotationId).ToList().Count;
                    var GetAppotmentById = _context.Appointment.Where(p => p.AppointmentId == addDistibutionsDto.AppointmentId).ToList().Count;
                    var GetDublecateData = _context.Distributions.Where(p => p.SubGroupId == addDistibutionsDto.SubGroupId && p.AppointmentId == addDistibutionsDto.AppointmentId).ToList().Count;
                    var ValedatoinOfInput = GetSubGroupById + GetDoctorById + GetCourseById + GetRotationById + GetAppotmentById;
                    if (ValedatoinOfInput == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH + " , " + IErrorMsgs.DOCTOR_NOT_FOUND
                                              + " , " + IErrorMsgs.COURSE_RECORD_NOT_FOUND + " , " + IErrorMsgs.NOT_FOUND_ROTAION
                                              + " , " + IErrorMsgs.NOT_FOUND_APPOTMENTS,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                        return ErrorMsg;
                    }
                    else
                    {
                        if (GetSubGroupById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetDoctorById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DOCTOR_NOT_FOUND,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetCourseById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetRotationById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_ROTAION,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetAppotmentById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_APPOTMENTS,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        else
                        {
                            var getDublecateData = _context.Distributions.Where(p => p.SubGroupId == addDistibutionsDto.SubGroupId && p.DoctorId == addDistibutionsDto.DoctorId &&
                            p.CourseId == addDistibutionsDto.CourseId && p.RotationId == addDistibutionsDto.RotationId && p.AppointmentId == addDistibutionsDto.AppointmentId).ToList().Count;
                            if (getDublecateData != 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                              "Error DUPLICATE ",
                                              "Enter the correct  data  , you are DUPLICATE the data "
                                              );
                                return ErrorMsg;

                            }
                            else
                            {
                                var getDublecateAppotments = _context.Distributions.Where(p => p.SubGroupId == addDistibutionsDto.SubGroupId && p.AppointmentId == addDistibutionsDto.AppointmentId).ToList().Count;

                                if (getDublecateAppotments != 0)
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUPLICATE_RECORD_APPOTMENTS,
                                              "Error DUPLICATE ",
                                              "Enter the correct  data  , you are DUPLICATE the data "
                                              );
                                    return ErrorMsg;
                                }
                                else
                                {
                                    var getDublecateDoctor = _context.Distributions.Where(p => p.SubGroupId == addDistibutionsDto.SubGroupId && p.DoctorId == addDistibutionsDto.DoctorId).ToList().Count;
                                    if (GetDublecateData == 0) { 
                                        if (getDublecateDoctor >= 1)
                                        {
                                            try
                                            {
                                                _context.Distributions.Add(addDistibutionsDto.AddDistribution());
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
                                                             SuccessfullyMsgs.SUCCESSFULLY_ADD_DISTBUTION_SMAE_DOCTOR_GET_SAME_SUBGROUP_GREATER_THAN_ONE_LUCTCHER,
                                                             "Add Successfully",
                                                             "You Are Add New Distributions "
                                                             );
                                            return SuccessfullyMsg;

                                        }

                                        else
                                        {
                                            try
                                            {
                                                _context.Distributions.Add(addDistibutionsDto.AddDistribution());
                                                _context.SaveChanges();
                                            }
                                            catch (Exception ex)
                                            {
                                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                      IErrorMsgs.INVALID_DATA_FORMAT,
                                                                      "Enter the Courect sympole of the Main Group",
                                                                      "Error in data entry. The data entered is not included "
                                                                      );
                                                return ErrorMsg;
                                            }
                                            GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                                             SuccessfullyMsgs.SUCCESSFULLY_ADD_DISTBUTION,
                                                             "Add Successfully",
                                                             "You Are Add New Distributions "
                                                             );
                                            return SuccessfullyMsg;

                                        }
                                    }
                                    else
                                    {
                                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                  IErrorMsgs.DUBLECATE_DISTRBUTION_IN_SAME_WEEK,
                                                                  "Enter the Courect sympole of the Main Group",
                                                                  "Error in data entry. The data entered is not included "
                                                                  );
                                        return ErrorMsg;
                                    }
                                }

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
        public GeneralMsgDto DeleteDistubution(int DistrbutionId)
        {
            try
            {
                var GetDistibutionIdForDelete = _context.Distributions.FirstOrDefault(p => p.DistributionId == DistrbutionId);
                if (DistrbutionId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.INVALID_DATA_FORMAT,
                               "Enter the requird filled",
                               Convert.ToString(DistrbutionId)
                               );
                    return ErrorMsg;
                }
                else
                {
                    if (GetDistibutionIdForDelete != null)
                    {
                        try
                        {
                            _context.Distributions.Remove(GetDistibutionIdForDelete);
                            _context.SaveChanges();
                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                           SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                           "Successfully Delete",
                                                           "You are delete this Distrbution "
                                                           );
                            return SucMsg;
                        }
                        catch (Exception ex)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.Error,
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
        public GeneralMsgDto UpdateDistrbution(AddDistibutionsDto NewDistrbutionData, int DistrbutionId)
        {
            try
            {
                if (NewDistrbutionData == null || DistrbutionId == null)
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
                    var GetSubGroupById = _context.subGroups.Where(p => p.SubGroupId == NewDistrbutionData.SubGroupId).ToList().Count;
                    var GetDoctorById = _context.doctors.Where(p => p.UserId == NewDistrbutionData.DoctorId).ToList().Count;
                    var GetCourseById = _context.Courses.Where(p => p.CouresId == NewDistrbutionData.CourseId).ToList().Count;
                    var GetRotationById = _context.Rotations.Where(p => p.RotationId == NewDistrbutionData.RotationId).ToList().Count;
                    var GetAppotmentById = _context.Appointment.Where(p => p.AppointmentId == NewDistrbutionData.AppointmentId).ToList().Count;
                    var ValedatoinOfInput = GetSubGroupById + GetDoctorById + GetCourseById + GetRotationById + GetAppotmentById;
                    if (ValedatoinOfInput == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH + " , " + IErrorMsgs.DOCTOR_NOT_FOUND
                                              + " , " + IErrorMsgs.COURSE_RECORD_NOT_FOUND + " , " + IErrorMsgs.NOT_FOUND_ROTAION
                                              + " , " + IErrorMsgs.NOT_FOUND_APPOTMENTS,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                        return ErrorMsg;
                    }
                    else
                    {
                        if (GetSubGroupById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetDoctorById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DOCTOR_NOT_FOUND,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetCourseById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.COURSE_RECORD_NOT_FOUND,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetRotationById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_ROTAION,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        if (GetAppotmentById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.NOT_FOUND_APPOTMENTS,
                                              "Not Found",
                                              "Enter the correct  data "
                                              );
                            return ErrorMsg;
                        }
                        else
                        {
                            var getDublecateData = _context.Distributions.Where(p => p.SubGroupId == NewDistrbutionData.SubGroupId && p.DoctorId == NewDistrbutionData.DoctorId &&
                            p.CourseId == NewDistrbutionData.CourseId && p.RotationId == NewDistrbutionData.RotationId && p.AppointmentId == NewDistrbutionData.AppointmentId).ToList().Count;
                            if (getDublecateData != 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                              "Error DUPLICATE ",
                                              "Enter the correct  data  , you are DUPLICATE the data "
                                              );
                                return ErrorMsg;

                            }
                            else
                            {
                                var getDublecateAppotments = _context.Distributions.Where(p => p.SubGroupId == NewDistrbutionData.SubGroupId && p.AppointmentId == NewDistrbutionData.AppointmentId).ToList().Count;
                                var OldDistrbutionData = _context.Distributions.FirstOrDefault(p => p.DistributionId == DistrbutionId);

                                if (getDublecateAppotments != 0)
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUPLICATE_RECORD_APPOTMENTS,
                                              "Error DUPLICATE ",
                                              "Enter the correct  data  , you are DUPLICATE the data "
                                              );
                                    return ErrorMsg;
                                }
                                else
                                {
                                    var getDublecateDoctor = _context.Distributions.Where(p => p.SubGroupId == NewDistrbutionData.SubGroupId && p.DoctorId == NewDistrbutionData.DoctorId).ToList().Count;
                                    if (getDublecateDoctor >= 1)
                                    {
                                        OldDistrbutionData.RotationId = NewDistrbutionData.RotationId;
                                        OldDistrbutionData.CourseId = NewDistrbutionData.CourseId;
                                        OldDistrbutionData.SubGroupId = NewDistrbutionData.SubGroupId;
                                        OldDistrbutionData.AppointmentId = NewDistrbutionData.AppointmentId;
                                        OldDistrbutionData.DoctorId = NewDistrbutionData.DoctorId;
                                        try
                                        {
                                            _context.SaveChanges();

                                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                                           SuccessfullyMsgs.DISTBUTION_UPDATED_SUCCESSFULLY,
                                                                           "Successfully Update",
                                                                           "You are Update this Distrbution "
                                                                           );
                                            return SucMsg;
                                        }
                                        catch (Exception ex)
                                        {
                                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                           IErrorMsgs.NOT_FOUND_DATA,
                                                                           "Enter the requird filled",
                                                                           "Ther is not any data to Update it "
                                                                           );
                                            return ErrorMsg;
                                        }


                                    }

                                    else
                                    {
                                        OldDistrbutionData.RotationId = NewDistrbutionData.RotationId;
                                        OldDistrbutionData.CourseId = NewDistrbutionData.CourseId;
                                        OldDistrbutionData.SubGroupId = NewDistrbutionData.SubGroupId;
                                        OldDistrbutionData.AppointmentId = NewDistrbutionData.AppointmentId;
                                        OldDistrbutionData.DoctorId = NewDistrbutionData.DoctorId;
                                        try
                                        {
                                            _context.SaveChanges();

                                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                                           SuccessfullyMsgs.DISTBUTION_UPDATED_SUCCESSFULLY,
                                                                           "Successfully Update",
                                                                           "You are Update this Distrbution "
                                                                           );
                                            return SucMsg;
                                        }
                                        catch (Exception ex)
                                        {
                                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                           IErrorMsgs.NOT_FOUND_DATA,
                                                                           "Enter the requird filled",
                                                                           "Ther is not any data to Update it "
                                                                           );
                                            return ErrorMsg;
                                        }
                                    }
                                }
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

        public List<GetDistbutionsQDto> getAllDistbutionByMainGroupId(int mainGroupId, int RotationId)
        {
            try
            {
                string sql = @"	 
                               SELECT
                                    d.DistributionId,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName,
                                    r.RotationName,
                                    x.UserId,
                                    x.FullName,
                                    ISNULL(x.Email, '') AS Email,
                                    ISNULL(c.CourseName, '') AS CourseName,
                                    ISNULL(c.CourseCode, '') AS CourseCode,
                                    ISNULL(a.WeekName, '') AS WeekName,
                                    ISNULL(a.StartSessionDate, '') AS StartSessionDate,
                                    ISNULL(a.EndSessionDate, '') AS EndSessionDate,
                                    ISNULL(a.SessionStartTime, '') AS SessionStartTime,
                                    ISNULL(a.SessionEndTime, '') AS SessionEndTime
                                FROM 
                                    Distributions d 
                                    JOIN Appointments a ON d.AppointmentId = a.AppointmentId
                                    LEFT JOIN Doctors s ON s.UserId = d.DoctorId 
                                    LEFT JOIN Hospitals h ON h.HospitalId = s.HospitalId 
                                    LEFT JOIN Department t ON t.DepartmentId = s.DepartmentId 
                                    LEFT JOIN Course c ON d.CourseId = c.CouresId 
                                    LEFT JOIN Rotations r ON r.RotationId = d.RotationId
                                    LEFT JOIN SubGroup p ON p.SubGroupId = d.SubGroupId 
                                    LEFT JOIN MainGroup m ON p.MainGroupId = m.MainGroupId
                                    LEFT JOIN Users x ON x.UserId = s.UserId
                                WHERE 
                                    m.MainGroupId = @mainGroupId
                                    AND r.RotationId = @RotationId
                            ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql, new SqlParameter("mainGroupId", mainGroupId)
                    , new SqlParameter("RotationId", RotationId)).ToList();
                if (result == null)
                    return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDistbutionsQDto> getAllDistbutionBySubGroupId(int SubGroupId, int RotationId)
        {
            try
            {
                string sql = @"	 
                                select
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,
                                    x.UserId ,
                                    x.FullName,
                                    x.Email,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId

                                    where p.SubGroupId = @SubGroupId
                                	    and r.RotationId = @RotationId


                            ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql, new SqlParameter("SubGroupId", SubGroupId)
                    , new SqlParameter("RotationId", RotationId)).ToList();
                if (result == null)
                    return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDistbutionsQ1Dto> getAllDistbutionToTheDoctorByDoctorIdAndRotationId(string DoctorId)
        {
            try
            {
                var GetAppoutmentId = @"
                                	      select 
		                                    r.RotationId
		                                    from Rotations r 
		                                    where       
		                                     CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
		                                    BETWEEN DATEADD(DAY, -7, CAST(r.StartRotationDate AS DATE))
		                                    AND CAST(r.EndRotationDate AS DATE) 
                                ";
                var AppotmentResult =  _context.Rotations
                            .FromSqlRaw(GetAppoutmentId)
                            .Select(a => a.RotationId)
                            .FirstOrDefault();

                if (AppotmentResult == null) 
                {
                    return null;
                }
                string sql = @"	 
                                select DISTINCT
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,    
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId
	                                left join Divisions div on div.SubGroupId = d.SubGroupId

	                                where s.UserId =@DoctorId
                                    and d.RotationId = @AppotmentResult                        
                                    and d.DistributionStatus = 3
                            	    and div.DivisionStatus = 3
	                                ORDER BY 
                                    a.StartSessionDate ASC ;

                            ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQ1Dto>(
                    sql,
                    new SqlParameter("DoctorId", DoctorId)
                    , new SqlParameter("AppotmentResult", AppotmentResult)
                    ).ToList();
                if (result == null)
                    return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }


        public List<GetDistbutionsQDto> getDistbutionByDistbutionId(int DistbutionId)
        {
            try
            {
                string sql = @"	 
                            	    select
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,
                                    x.UserId ,
                                    x.FullName,
                                    x.Email,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId                                  
	                                where 
	                                d.DistributionId = @DistbutionId
                                    ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql,
                    new SqlParameter("DistbutionId", DistbutionId)
                    ).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDistbutionsQDto> getDistbutionForTheStudentsByStudentsId(string StudentsId)
        {
            try
            {
                var GetAppoutmentId = @"
                                	      select 
		                                    r.RotationId
		                                    from Rotations r 
		                                    where       
		                                     CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
		                                    BETWEEN DATEADD(DAY, -7, CAST(r.StartRotationDate AS DATE))
		                                    AND CAST(r.EndRotationDate AS DATE) 
                                ";
                var AppotmentResult = _context.Rotations
                            .FromSqlRaw(GetAppoutmentId)
                            .Select(a => a.RotationId)
                            .FirstOrDefault();
                if (AppotmentResult == null)
                    return null;
                string sql = @"	 
                            	    select DISTINCT
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,
                                    x.UserId ,
                                    x.FullName,
                                    x.Email,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
	                                left join Divisions wa on wa.SubGroupId = p.SubGroupId
                                    left join Students w on wa.StudentId = w.UserId  
                                    left join Users x on x.UserId = s.UserId  
	                                left join Divisions div on div.SubGroupId = d.SubGroupId


                                    where 
                                    w.UserId = @StudentsId 
	                                and d.RotationId = @AppotmentResult                           
                                    and d.DistributionStatus = 3
	                                and div.DivisionStatus = 3
	                                ORDER BY 
                                    a.StartSessionDate ASC ;

                                    ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql,
                    new SqlParameter("StudentsId", StudentsId)
                    ,new SqlParameter("AppotmentResult", AppotmentResult)
                    ).ToList();
                if (result == null)
                    return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDistbutionsQDto> getAllDistbutionId(int DistributionId)
        {
            try
            {
                string sql = @"	 
                                select
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,
                                    x.UserId ,
                                    x.FullName,
                                    x.Email,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId

                                    where  d.DistributionId = @DistributionId
                            ";

                var result = _context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql, new SqlParameter("DistributionId", DistributionId)
                    ).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDocotorCourseDataQDto> GetDoctorByRotationIdMainGroupAndAcadmicYear (int MainGroup , int RotationId , string AcadmicYear )
        {
            try
            {
                    string sql = @"	 
                                   select DISTINCT
		                                u.FullName ,
		                                u.UserId , 
		                                co.CouresId,
		                                co.CourseName,
		                                dis.DepartmentId,
		                                dis.RotationId
		                                from DistributionsMainGroup dis
		                                join Doctors do on do.DepartmentId = dis.DepartmentId 
		                                join Users u on u.UserId = do.UserId 
		                                join Doctor_Course doCo on doCo.DoctorId = do.UserId
		                                join Course co on co.CouresId = doCo.Cours
		                                where  
		                                dis.MainGroupId = @MainGroup 
		                                and dis.RotationId = @RotationId
		                                and doCo.CurrentAcademicYearName = @AcadmicYear
                                ";

                    var result = _context.Database.SqlQueryRaw<GetDocotorCourseDataQDto>(
                        sql, new SqlParameter("MainGroup", MainGroup),
                        sql, new SqlParameter("RotationId", RotationId),
                        sql, new SqlParameter("AcadmicYear", AcadmicYear)
                        ).ToList();
                    if (result == null)
                        return null;
                    return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDistbutionsQ2Dto> GetCourseByDctorIdAndAcadmicYear(string DoctorId, string AcadmicYear)
        {
            try
            {
            string sql = @"	 
                             select DISTINCT
                                co.CouresId,
                                co.CourseName,
                                dis.DepartmentId
                                from DistributionsMainGroup dis
                                join Doctors do on do.DepartmentId = dis.DepartmentId 
                                join Users u on u.UserId = do.UserId 
                                join Doctor_Course doCo on doCo.DoctorId = do.UserId
                                join Course co on co.CouresId = doCo.Cours
                                where 
                                doCo.DoctorId = @DoctorId
		                        and doCo.CurrentAcademicYearName = @AcadmicYear
                        ";

            var result = _context.Database.SqlQueryRaw<GetDistbutionsQ2Dto>(
                sql, new SqlParameter("DoctorId", DoctorId),
                sql, new SqlParameter("AcadmicYear", AcadmicYear)
                ).ToList();
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

using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.DistributionsDTO;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.RotationsDTO;
using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.AppointmentsMapper;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using V1.DTO.AppointmentDTO;

namespace DevetionStudetns.Repositry.AppotmentsReposetry
{
    public class AppotmentsRepo : IAppointments
    {
        private readonly DBC _context;
        public AppotmentsRepo(DBC dBC)
        {
            _context = dBC;
        }
        public GeneralMsgDto AddAppotments(AddAppointmentsDto addAppointmentsDto)
        {
            if (addAppointmentsDto == null)
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
                if (addAppointmentsDto.StartSessionDate >= addAppointmentsDto.EndSessionDate)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_session_end_date_must_be_greater_than_the_session_start_date,
                                "Enter the requird filled",
                                "Enter the Couret first Date And end Date . The Start Date must to be less than End Date "
                                );
                    return ErrorMsg;
                }
                var getNumberOfAppotmentsInOneRotation = _context.Appointment.Where(p => p.RotationId == addAppointmentsDto.RotationId).ToList().Count;
                if (getNumberOfAppotmentsInOneRotation >= 13)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.MAXUMUM_NUMBER_OF_APPOTMENTS_IN_ON_ROTATION,
                                "Error",
                                "MAXUMUM_NUMBER_OF_APPOTMENTS_IN_ON_ROTATION"
                                );
                    return ErrorMsg;
                }
                var getGetRotation = _context.Rotations.Where(p => p.RotationId == addAppointmentsDto.RotationId).ToList().Count;
                if (getGetRotation == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.NOT_FOUND_ROATATION,
                                "Not Found",
                                "this isn't have rotation have this id " + addAppointmentsDto.RotationId
                                );
                    return ErrorMsg;
                }
                else
                {
                    int dateDifference = addAppointmentsDto.EndSessionDate.DayNumber - addAppointmentsDto.StartSessionDate.DayNumber;
                    if (dateDifference >= 7 && dateDifference <= 10)
                    {
                        bool isOverlapping = _context.Appointment
                .Any(d =>
                (addAppointmentsDto.StartSessionDate >= d.StartSessionDate && addAppointmentsDto.StartSessionDate <= d.EndSessionDate) ||
                (addAppointmentsDto.EndSessionDate >= d.StartSessionDate && addAppointmentsDto.EndSessionDate <= d.EndSessionDate) ||
                (d.StartSessionDate >= addAppointmentsDto.StartSessionDate && d.StartSessionDate <= addAppointmentsDto.EndSessionDate) ||
                (d.EndSessionDate >= addAppointmentsDto.StartSessionDate && d.EndSessionDate <= addAppointmentsDto.EndSessionDate)
                    );

                        if (!isOverlapping)
                        {
                            var getNewRotationData = _context.Rotations.FirstOrDefault(p => p.RotationId == addAppointmentsDto.RotationId);
                            if (addAppointmentsDto.StartSessionDate >= getNewRotationData.StartRotationDate && addAppointmentsDto.EndSessionDate <= getNewRotationData.EndRotationDate)
                            {
                                var GetDublecateWeekName = _context.Appointment.Where(p => p.RotationId == addAppointmentsDto.RotationId && p.WeekName == addAppointmentsDto.WeekName).ToList().Count;
                                if (GetDublecateWeekName == 0)
                                {
                                    try
                                    {
                                        _context.Appointment.Add(addAppointmentsDto.AddAppointments());
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
                                                        SuccessfullyMsgs.WORKSHOP_REGISTERED_SUCCESSFULLY,
                                                        "Add Successfully",
                                                        "You Are Add New Appointments "
                                                        );
                                    return SuccessfullyMsg;

                                }
                                else
                                {

                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.DUPLICATE_WEEK_NAME,
                                                        "DUPLICATE DATA",
                                                        "DUPLICATE DATA "
                                                        );
                                    return ErrorMsg;
                                }

                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.OUT_OF_ROTATION_DATE,
                                                        "Error",
                                                        "Out of rotation date "
                                                        );
                                return ErrorMsg;

                            }
                        }

                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.ERROR_APPOTMENTS,
                                                        "Error",
                                                        "Error in data entry "
                                                        );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.ERROR_APPOTMENTS_GRATER_THAN_1WEEK,
                                                          "Error",
                                                          "Error in data entry "
                                                          );
                        return ErrorMsg;
                    }

                }
            }
        }

        public GeneralMsgDto DeleteAppointments(int AppointmentsId)
        {
            if (AppointmentsId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                           "Enter the requird filled",
                           Convert.ToString(AppointmentsId)
                           );
                return ErrorMsg;
            }
            else
            {
                var GetAppointmentIdForDelete = _context.Appointment.FirstOrDefault(p => p.AppointmentId == AppointmentsId);
                if (GetAppointmentIdForDelete != null)
                {
                    try
                    {
                        _context.Appointment.Remove(GetAppointmentIdForDelete);
                        _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                       SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                       "Successfully Delete",
                                                       "You are delete this Appointment "
                                                       );
                        return SucMsg;
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.Error,
                               "Error",
                               "Error "
                               );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_APPTOMENTS,
                           "Enter the requird filled",
                           "Ther is not any data to delete it "
                           );
                    return ErrorMsg;
                }
            }
        }
        public GeneralMsgDto UpdateAppointment(AddAppointmentsDto NewAppointmentData, int AppointmentId)
        {
            if (AppointmentId == null || NewAppointmentData == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                       IErrorMsgs.INVALID_DATA_FORMAT,
                                                       "Enter the requird filled",
                                                       Convert.ToString(AppointmentId)
                                                       );
                return ErrorMsg;
            }
            else
            {
                if (NewAppointmentData.StartSessionDate >= NewAppointmentData.EndSessionDate)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_session_end_date_must_be_greater_than_the_session_start_date,
                                "Enter the requird filled",
                                "The session end date must be greater than the session start date "
                                );
                    return ErrorMsg;
                }
                else
                {
                    var OldAppotmentData = _context.Appointment.FirstOrDefault(p => p.AppointmentId == AppointmentId);

                    if (OldAppotmentData == null)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                IErrorMsgs.NOT_FOUND_DATA,
                                                                "Enter the requird filled",
                                                                "Ther is not any data to update it "
                                                                );
                        return ErrorMsg;
                    }
                    else
                    {
                        bool isOverlapping = _context.Appointment
                         .Any(d =>
                             d.AppointmentId != AppointmentId && 
                             (
                                 (NewAppointmentData.StartSessionDate >= d.StartSessionDate && NewAppointmentData.StartSessionDate <= d.EndSessionDate) ||
                                 (NewAppointmentData.EndSessionDate >= d.StartSessionDate && NewAppointmentData.EndSessionDate <= d.EndSessionDate) ||
                                 (d.StartSessionDate >= NewAppointmentData.StartSessionDate && d.StartSessionDate <= NewAppointmentData.EndSessionDate) ||
                                 (d.EndSessionDate >= NewAppointmentData.StartSessionDate && d.EndSessionDate <= NewAppointmentData.EndSessionDate)
                             )
                         );

                        if (!isOverlapping)
                        {

                            var getNewRotationData = _context.Rotations.FirstOrDefault(p => p.RotationId == NewAppointmentData.RotationId);
                            if (NewAppointmentData.StartSessionDate >= getNewRotationData.StartRotationDate && NewAppointmentData.EndSessionDate <= getNewRotationData.EndRotationDate)
                            {
                                if (OldAppotmentData.RotationId == NewAppointmentData.RotationId &&
                                    OldAppotmentData.WeekName == NewAppointmentData.WeekName &&
                                    OldAppotmentData.StartSessionDate == NewAppointmentData.StartSessionDate &&
                                    OldAppotmentData.EndSessionDate == NewAppointmentData.EndSessionDate &&
                                    OldAppotmentData.SessionStartTime == NewAppointmentData.SessionStartTime &&
                                    OldAppotmentData.SessionEndTime == NewAppointmentData.SessionEndTime
                                    )
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                   IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                                                   "DUPLICATE data",
                                                                   "DUPLICATE data"
                                                                   );
                                    return ErrorMsg;
                                }

                                else
                                {
                                    var GetDublecateWeekName = _context.Appointment.Where(p => p.RotationId == NewAppointmentData.RotationId && p.WeekName == NewAppointmentData.WeekName && p.AppointmentId != AppointmentId).ToList().Count;
                                    if (GetDublecateWeekName == 0)
                                    {

                                        try
                                        {
                                            OldAppotmentData.RotationId = NewAppointmentData.RotationId;
                                            OldAppotmentData.WeekName = NewAppointmentData.WeekName;
                                            OldAppotmentData.StartSessionDate = NewAppointmentData.StartSessionDate;
                                            OldAppotmentData.EndSessionDate = NewAppointmentData.EndSessionDate;
                                            OldAppotmentData.SessionStartTime = NewAppointmentData.SessionStartTime;
                                            OldAppotmentData.SessionEndTime = NewAppointmentData.SessionEndTime;
                                            _context.SaveChanges();

                                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                                           SuccessfullyMsgs.LECTURE_SCHEDULE_UPDATED,
                                                                           "Successfully Update",
                                                                           "You are Update this Appointment "
                                                                           );
                                            return SucMsg;
                                        }
                                        catch (Exception ex)
                                        {

                                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                           IErrorMsgs.NOT_FOUND_DATA,
                                                                           "Enter the requird filled",
                                                                           ex.InnerException?.Message
                                                                           );
                                            return ErrorMsg;
                                        }

                                    }
                                    else
                                    {
                                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.DUPLICATE_WEEK_NAME,
                                                        "DUPLICATE DATA",
                                                        "DUPLICATE DATA "
                                                        );
                                        return ErrorMsg;
                                    }
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.OUT_OF_ROTATION_DATE,
                                                        "Error",
                                                        "Out of rotation date "
                                                        );
                                return ErrorMsg;

                            }
                        }

                        else
                        {

                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.ERROR_APPOTMENTS,
                                                      "Error",
                                                      "Error in data entry "
                                                      );
                            return ErrorMsg;
                        }
                    }
                }
            }
        }
        public List<GetAppotmentsDto> getAppointmentsInTheOneSubGroup(int subGroupId, int RotationId)
        {
            try
            {
                string sql = @"	 
                              SELECT 
                                   p.SubGroupSympole,
                                   s.RotationId,
                                   s.RotationName,
                                   s.StartRotationDate,
                                   s.EndRotationDate,
                                   a.WeekName,
                                   a.StartSessionDate,
                                   a.EndSessionDate ,
                                   a.SessionStartTime,
                                   a.SessionEndTime
                                from 
                                Appointments a 
                                join Distributions i on a.AppointmentId = i.AppointmentId
                                join SubGroup p on p.SubGroupId = i.SubGroupId
                                join Rotations s on s.RotationId = a.RotationId
                                where 
                                    p.SubGroupId =@subGroupId
                                and 	                           
                        	    a.RotationId = @RotationId
                                ORDER BY 
                                     a.StartSessionDate ASC ;
                            ";
                var result = _context.Database.SqlQueryRaw<GetAppotmentsDto>(
                    sql, new SqlParameter("subGroupId", subGroupId),
                    new SqlParameter("RotationId", RotationId)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public List<GetAppotmentsDto> getAppointmentsInTheOneMainGroup(int MainGroupId, int RotationId)
        {
            try
            {
                string sql = @"	 
                                SELECT 
                                    p.SubGroupSympole,
	                                s.RotationId,
                                    s.RotationName,
	                                s.StartRotationDate,
	                                s.EndRotationDate,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate ,
                                    a.SessionStartTime,
                                    a.SessionEndTime
                                from 
                                Appointments a 
                                join Distributions i on a.AppointmentId = i.AppointmentId
                                join SubGroup p on p.SubGroupId = i.SubGroupId
                                join Rotations s on s.RotationId = a.RotationId
                                where 
                                    p.MainGroupId =@mainGroupId
                                and 	                           
                                s.RotationId = @RotationId
                                ORDER BY 
                                        p.SubGroupSympole ASC ;
                            ";

                var result = _context.Database.SqlQueryRaw<GetAppotmentsDto>(
                    sql,
                    new SqlParameter("mainGroupId", MainGroupId)
                    , new SqlParameter("RotationId", RotationId)

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

        public List<GetAppointmentsQDto> GetAppointmentsByRotationId(int rotationId)
        {
            try
            {
                string sql = @"	 
                               select 
                                a.AppointmentId,
                                a.RotationId,
	                            r.RotationName,
	                            r.StartRotationDate,
	                            r.EndRotationDate,
                                a.WeekName,
                                a.StartSessionDate,
                                a.EndSessionDate,
                                a.SessionStartTime,
                                a.SessionEndTime
                                from 
                                Appointments a 
	                            join Rotations r on r.RotationId = a.RotationId
	                                where 
	                                a.RotationId = @rotationId
	                         ORDER BY 
                                a.StartSessionDate ASC ;
                            ";

                var result = _context.Database.SqlQueryRaw<GetAppointmentsQDto>(
                    sql, new SqlParameter("rotationId", rotationId)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetAppointmentsQDto> GetAppointmentsByAppointmentId(int AppointmentId)
        {

            try
            {
                string sql = @"	 
                             
	                                select 
                                        a.AppointmentId,
                                        a.RotationId,
	                                    r.RotationName,
	                                    r.StartRotationDate,
	                                    r.EndRotationDate,
                                        a.WeekName,
                                        a.StartSessionDate,
                                        a.EndSessionDate,
                                        a.SessionStartTime,
                                        a.SessionEndTime
                                        from 
                                        Appointments a 
	                                    join Rotations r on r.RotationId = a.RotationId
	                                where 
	                                a.AppointmentId = @AppointmentId

                            ";

                var result = _context.Database.SqlQueryRaw<GetAppointmentsQDto>(
                    sql, new SqlParameter("AppointmentId", AppointmentId)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetAppointmentsQDto> GetAllAppointmentsByAppointment()
        {
            try
            {
                string sql = @"	 
                               select 
                                    a.AppointmentId,
                                    a.RotationId,
	                                r.RotationName,
                                    r.StartRotationDate,
	                                r.EndRotationDate,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime
                                    from 
                                    Appointments a 
	                                join Rotations r on r.RotationId = a.RotationId
                            ";

                var result = _context.Database.SqlQueryRaw<GetAppointmentsQDto>(
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

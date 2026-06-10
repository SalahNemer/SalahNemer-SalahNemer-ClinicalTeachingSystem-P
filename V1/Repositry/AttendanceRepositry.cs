using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.DTO.Repotrs;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Interface;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using testDtoAndmapper.Entity;
using V1.DTO.AttendanceDTO;

namespace DevetionStudetns.Repositry.AttendanceRepositry
{
    public class AttendanceRepositry : IAttendanceRepositry
    {
        public AttendanceRepositry(DBC dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly DBC _dbContext;

        public async Task<IEnumerable<Attendance>> GetAllAtendance()
        {
            try
            {
                return await _dbContext.Attendance.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<Attendance?> GetAttendanceById(int id)
        {
            try
            {
                return await _dbContext.Attendance.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        private async Task<GeneralMsgDto?> ValidateAttendanceAsync(Attendance attendance)
        {
            try
            {
                if (attendance.CourseId == 0 || !await _dbContext.Courses.AnyAsync(c => c.CouresId == attendance.CourseId))
                    return new GeneralMsgDto(IErrorMsgs.COURSE_ID_NOT_FOUND_IN_COURSE_TABLE, "معرف الكورس غير صالح", "الرجاء إدخال معرف كورس صالح.");

                if (string.IsNullOrEmpty(attendance.StudentId) || !await _dbContext.Users.AnyAsync(u => u.UserId == attendance.StudentId))
                    return new GeneralMsgDto(IErrorMsgs.STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE, "معرف الطالب غير صالح", "الرجاء إدخال معرف طالب صالح.");

                if (string.IsNullOrEmpty(attendance.DoctorId) || !await _dbContext.Users.AnyAsync(d => d.UserId == attendance.DoctorId))
                    return new GeneralMsgDto(IErrorMsgs.DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE, "معرف الطبيب غير صالح", "الرجاء إدخال معرف طبيب صالح.");

                var validStatuses = new List<string> { "حاضر", "غائب", "غائب بعذر" };
                if (!validStatuses.Contains(attendance.AttendanceStatus))
                    return new GeneralMsgDto(IErrorMsgs.INVALID_ATTENDANCE_STATUS, "حالة الحضور غير صالحة", $"يجب أن تكون حالة الحضور واحدة من: {string.Join(", ", validStatuses)}.");

                var currentYear = DateTime.UtcNow.Year;
                var today = DateOnly.FromDateTime(DateTime.UtcNow);
                var minAllowedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)); 
                var maxAllowedDate = today; 

                if (attendance.AttendanceDate.Year != currentYear ||
                    attendance.AttendanceDate < minAllowedDate ||
                    attendance.AttendanceDate > maxAllowedDate)
                {
                    return new GeneralMsgDto(IErrorMsgs.INVALID_ATTENDANCE_DATE,
                                             "تاريخ الحضور غير صالح",
                                             "تأكد من إدخال تاريخ صحيح (يجب أن يكون من اليوم أو خلال اليومين الماضيين، وألا يكون مستقبليًا أو من سنة سابقة).");
                }


                bool isAttendanceExists = await _dbContext.Attendance.AnyAsync(a =>
                                                              a.StudentId == attendance.StudentId &&
                                                              a.AttendanceDate == attendance.AttendanceDate);

                if (isAttendanceExists)
                {
                    return new GeneralMsgDto(IErrorMsgs.ATTENDANCE_ALREADY_EXISTS,
                                             "الحضور موجود مسبقًا",
                                             "لا يمكن تسجيل الحضور مرتين لنفس الطالب في نفس اليوم.");
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

     
        public async Task<GeneralMsgDto> AddAttendance(List<AddAttententsDto> attendances)
        {
            try
            {
                foreach (var attendance in attendances)
                {
                    var ValedationStudent = await _dbContext.students.AnyAsync(p => p.UserId == attendance.StudentId);
                    if (!ValedationStudent)
                        return new GeneralMsgDto(IErrorMsgs.STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE, "معرف الطالب غير صالح", "الرجاء إدخال معرف طالب صالح.");

                    var ValedationDoctor = await _dbContext.doctors.AnyAsync(p => p.UserId == attendance.DoctorId);
                    if (! ValedationDoctor)
                        return new GeneralMsgDto(IErrorMsgs.DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE, "معرف الطبيب غير صالح", "الرجاء إدخال معرف طبيب صالح.");

                    bool isAttendanceExists = await _dbContext.Attendance.AnyAsync(a =>
                                                              a.StudentId == attendance.StudentId &&
                                                              a.AttendanceDate == attendance.AttendanceDate);

                    if (isAttendanceExists)
                    {
                        return new GeneralMsgDto(IErrorMsgs.ATTENDANCE_ALREADY_EXISTS,
                                                 "الحضور موجود مسبقًا",
                                                 "لا يمكن تسجيل الحضور مرتين لنفس الطالب في نفس اليوم.");
                    }

                    var validStatuses = new List<string> { "حاضر", "غائب", "غائب بعذر" };
                    if (!validStatuses.Contains(attendance.AttendanceStatus))
                        return new GeneralMsgDto(IErrorMsgs.INVALID_ATTENDANCE_STATUS, "حالة الحضور غير صالحة", $"يجب أن تكون حالة الحضور واحدة من: {string.Join(", ", validStatuses)}.");


                }


                foreach (var attendance in attendances)
                {
                    string sql = @"
                             insert into Attendance 
                                    (StudentId , DoctorId , CourseId , AttendanceDate , AttendanceStatus, Notes)
                                values (
                                    {0}, 
                                    {1}, 
                                    (
                                        SELECT TOP 1 d.CourseId 
                                        FROM Distributions d
                                        JOIN Appointments a ON d.AppointmentId = a.AppointmentId
                                        WHERE 
                                            d.DoctorId = {1}
                                            AND
                                            CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                                            BETWEEN CAST(a.StartSessionDate AS DATE) 
                                                AND CAST(a.EndSessionDate AS DATE)
                                    ),
                                    {2},
                                    {3},
                                    {4}
                                    )            
                            ";

                    await _dbContext.Database.ExecuteSqlRawAsync(sql,
                        attendance.StudentId,
                        attendance.DoctorId,
                        attendance.AttendanceDate,
                        attendance.AttendanceStatus,
                        attendance.Notes);
                }

                return new GeneralMsgDto(SuccessfullyMsgs.ATTENDANCE_ADDED_SUCCESSFULLY, "تمت العملية", "تم تسجيل الحضور بنجاح.");
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }


        public async Task<GeneralMsgDto> UpdateAttendance(Attendance attendance)
        {
            try
            {
                if (string.IsNullOrEmpty(attendance.StudentId) || !await _dbContext.Users.AnyAsync(u => u.UserId == attendance.StudentId)) /// انا هنا 
                    return new GeneralMsgDto(IErrorMsgs.STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE, "معرف الطالب غير صالح", "الرجاء إدخال معرف طالب صالح.");

                var validStatuses = new List<string> { "حاضر", "غائب", "غائب بعذر" };
                if (!validStatuses.Contains(attendance.AttendanceStatus))
                    return new GeneralMsgDto(IErrorMsgs.INVALID_ATTENDANCE_STATUS, "حالة الحضور غير صالحة", $"يجب أن تكون حالة الحضور واحدة من: {string.Join(", ", validStatuses)}.");


                _dbContext.Attendance.Update(attendance);
                await _dbContext.SaveChangesAsync();

                return new GeneralMsgDto(
                    SuccessfullyMsgs.ATTENDANCE_UPDATED_SUCCESSFULLY,
                    "200",
                    "تم تحديث حضور الطالب بنجاح."
                );
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<Attendance?> ExistingByDate(DateOnly attendanceDate)
        {
            try
            {
                return await _dbContext.Attendance.FirstOrDefaultAsync(a => a.AttendanceDate == attendanceDate);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }


        public async Task<GeneralMsgDto> DeleteAttendance(int id)
        {
            try
            {
                if (id <= 0)
                    return new GeneralMsgDto(IErrorMsgs.INVALID_ATTENDANCE_ID, "معرف الحضور غير صالح", "الرجاء إدخال معرف صحيح.");

                var attendance = await _dbContext.Attendance.FindAsync(id);
                if (attendance == null)
                    return new GeneralMsgDto(IErrorMsgs.ATTENDANCE_NOT_FOUND_IN_DB, "حضور غير موجود", "لم يتم العثور على الحضور بهذا المعرف.");

                _dbContext.Attendance.Remove(attendance);
                await _dbContext.SaveChangesAsync();

                return new GeneralMsgDto(SuccessfullyMsgs.ATTENDANCE_DELETED_SUCCESSFULLY, "تمت العملية", "تم حذف سجل الحضور بنجاح.");
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<IEnumerable<GetAttendanceByStudentIdQDto>> GetAttendanceByStudentId(string studentId)
        {
            try
            {
                var query = @"
                     SELECT 
                            a.StudentId,
                            u.FullName AS StudentName,
                            u.Email AS StudentEmail,
                            a.AttendanceId, 
                            a.CourseId,
                            c.CourseName AS CourseName,
                            a.DoctorId,
                            d.FullName AS DoctorName, 
                            a.AttendanceStatus,
                            a.AttendanceDate,
                            a.Notes 
                    FROM Attendance a
                        INNER JOIN Users u ON a.StudentId = u.UserId    
                        INNER JOIN Course c ON a.CourseId = c.CouresId
                        INNER JOIN Users d ON a.DoctorId = d.UserId
                    WHERE a.StudentId = {0}";
          
                var result = await _dbContext.Database
                                                .SqlQueryRaw<GetAttendanceByStudentIdQDto>(query,studentId)
                                            .ToListAsync();                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
       
        public async Task<IEnumerable<GetAttendanceByDoctorIdQDto>> GetAttendanceByDoctorId(string doctorId)
        {
            try
            {
                var query = @"
                SELECT
                    a.DoctorId,
                    d.FullName AS DoctorName,
                    d.Email AS DoctorEmail,
                    a.AttendanceId,
                    a.CourseId,
                    c.CourseName,
                    a.StudentId,
                    s.FullName AS StudentName,
                    s.Email AS StudentEmail,
                    a.AttendanceStatus,
                    a.AttendanceDate,
                    a.Notes
                FROM Attendance a
                INNER JOIN Course c ON a.CourseId = c.CouresId
                INNER JOIN Users d ON a.DoctorId = d.UserId
                INNER JOIN Users s ON a.StudentId = s.UserId
                WHERE a.DoctorId = {0} ";
           
            var result = await _dbContext.Database
                                             .SqlQueryRaw<GetAttendanceByDoctorIdQDto>(query, doctorId)
                                             .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
       public async Task<IEnumerable<GetAttendanceByCourseIdQDto>> GetAttendanceByCourseId(int  courseId)
        {
            try
            {
                var query = @"
                SELECT 
                    a.AttendanceId, 
                    a.CourseId,
                    c.CourseName AS CourseName,
                    a.DoctorId,
                    d.FullName AS DoctorName, 
                    a.StudentId,
                    u.FullName AS StudentName,
                    a.AttendanceStatus,
                    a.AttendanceDate,
                    a.Notes 
                FROM Attendance a
                    INNER JOIN Course c ON a.CourseId = c.CouresId
                    INNER JOIN Users d ON a.DoctorId = d.UserId
                    INNER JOIN Users u ON a.StudentId = u.UserId    
                WHERE a.CourseId = {0}";

                var result = await _dbContext.Database
                                             .SqlQueryRaw<GetAttendanceByCourseIdQDto>(query, courseId)
                                             .ToListAsync();
                return result; 
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<IEnumerable<GetSubGroupForDoctorIdQDto>> GetSubGroupForDoctorId(string doctorId)
        {
            try
            {
                var query = @"
                SELECT 
	                    w.UserId AS StudentId, 
                        w.FullName AS StudentName,
                        w.Email,
                        w.PhoneNumber,
                        s.SubGroupSympole ,
	                    p.MainGroupSympole,
	                    t.CouresId,
	                    t.CourseName,           
	                    t.CourseIevel
	
               FROM 
              Distributions a
              JOIN SubGroup s ON s.SubGroupId = a.SubGroupId
              JOIN Divisions d ON d.SubGroupId = s.SubGroupId
              JOIN Students q ON q.UserId = d.StudentId	
              JOIN Users w ON q.UserId = w.UserId
              JOIN Appointments r ON r.AppointmentId = a.AppointmentId
              JOIN Doctors e on e.UserId = a.DoctorId
              JOIN MainGroup p on p.MainGroupId = s.MainGroupId
              JOIN Course t on  t.CouresId = a.CourseId

              WHERE 
                        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                        BETWEEN CAST(r.StartSessionDate AS DATE) AND DATEADD(DAY, 0, CAST(r.EndSessionDate AS DATE))
                        AND e.UserId = {0}";

                var result = await _dbContext.Database
                                             .SqlQueryRaw<GetSubGroupForDoctorIdQDto>(query, doctorId)
                                             .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
      
        public async Task<IEnumerable<GetAttendanceLastWeekQDto>> GetAttendanceByDateRange(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var query = @"
                        SELECT
                            a.CourseId,
                            c.CourseName,
                            a.DoctorId,
                            d.FullName AS DoctorName,
                            a.StudentId,
                            u.FullName AS StudentName,
                            u.Email AS StudentEmail,
                            a.AttendanceStatus,
                            a.AttendanceDate,
                            a.Notes
                        FROM Attendance a
                        INNER JOIN Course c ON a.CourseId = c.CouresId
                        INNER JOIN Users d ON a.DoctorId = d.UserId
                        INNER JOIN Users u ON a.StudentId = u.UserId
                        WHERE a.AttendanceDate BETWEEN @startDate AND @endDate";

                var result = await _dbContext.Database
                    .SqlQueryRaw<GetAttendanceLastWeekQDto>(query,
                        new SqlParameter("@startDate", startDate),
                        new SqlParameter("@endDate", endDate))
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<IEnumerable<GetAttendanceByDateANDDoctorIdQDto>> GetAttendanceByDateAndDoctorId(DateOnly date, string doctorId)
        {
            try
            {
                    var query = @"
                            SELECT 
                                a.AttendanceId,
                                a.StudentId,
                                u.FullName AS StudentName,
                                a.AttendanceStatus,
                                a.Notes
                            FROM Attendance a 
                            INNER JOIN Users u ON a.StudentId = u.UserId
                            WHERE a.AttendanceDate = {0} AND a.DoctorId = {1}";

                var result = await _dbContext.Database
                    .SqlQueryRaw<GetAttendanceByDateANDDoctorIdQDto>(query, date, doctorId)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

using System.Reflection.Metadata.Ecma335;
using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.Interface;
using DevetionStudetns.Mappers.AttendanceMapper;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.AttendanceDTO;

namespace DevetionStudetns.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepositry _attendanceRepositry;
        private readonly DBC _dBC; 
        public AttendanceService(IAttendanceRepositry attendanceRepositry, DBC dBC)
        {
            _attendanceRepositry = attendanceRepositry;
            _dBC = dBC;
        }

        public async Task<IEnumerable<AttendanceDTO>> GetAllAttendance()
        {
            var attendances =  await _attendanceRepositry.GetAllAtendance();
            return attendances.Select(u=> AttendanceMapper.ShowAttendance(u)).ToList();

        }
        
        public async Task<AttendanceDTO?> GetAttendanceById(int id)
        {
            var attendance = await _attendanceRepositry.GetAttendanceById(id);
            return attendance != null ? AttendanceMapper.ShowAttendance(attendance) : null; 
        }

        public async Task<GeneralMsgDto> AddAttendance(List<AddAttententsDto> attendanceList)
        {
        
            return await _attendanceRepositry.AddAttendance(attendanceList);
        }


        public async Task<GeneralMsgDto> UpdateAttendance(int attendanceId, AttendanceUpdateDTO attendanceDto)
        {
            var attendanceExists = await _attendanceRepositry.GetAttendanceById(attendanceId);

            if (attendanceExists == null)
            {
                return new GeneralMsgDto(
                    IErrorMsgs.ATTENDANCE_RECORD_NOT_FOUND,
                    "لم يتم العثور على السجل",
                    "الرجاء التأكد من معرف الحضور المدخل."
                );
            }

            attendanceExists.StudentId = attendanceDto.StudentId;
            attendanceExists.AttendanceStatus = attendanceDto.AttendanceStatus;
            attendanceExists.Notes = attendanceDto.Notes;

            return await _attendanceRepositry.UpdateAttendance(attendanceExists);
        }


        public async Task<GeneralMsgDto> DeleteAttendance(int id)
        {
            return await _attendanceRepositry.DeleteAttendance(id);
        }

        public async Task<object> GetAttendanceByStudentId(string studnetId)
        {
            var attendanceRecords = await _attendanceRepositry.GetAttendanceByStudentId(studnetId);
            if (!attendanceRecords.Any()) 
            {
                return new GeneralMsgDto(
                IErrorMsgs.STUDENT_ID_NOT_FOUND,
                "رقم الطالب غير صالح",
                "لم يتم العثور على أي سجل حضور لهذا الطالب، الرجاء التحقق من رقم الطالب المدخل."
            );
            }
            return attendanceRecords;
        }

        public async Task<object> GetAttendanceByDoctorId(string doctorId)
        {
            bool isDoctorExists = await _dBC.Users.AnyAsync(d => d.UserId == doctorId);
            if (!isDoctorExists)
            {
                return new GeneralMsgDto(IErrorMsgs.DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE,
                                         "معرف الطبيب غير صالح",
                                         "لم يتم العثور على الطبيب، الرجاء إدخال معرف طبيب صالح.");
            }

            var attendanceRecords = await _attendanceRepositry.GetAttendanceByDoctorId(doctorId);
            if (!attendanceRecords.Any())
            {
                return new GeneralMsgDto(
               IErrorMsgs.DOCTOR_ID_NOT_FOUND,
               "رقم الطبيب غير صالح",
               "لم يتم العثور على أي سجل حضور لهذا الطبيب، الرجاء التحقق من رقم الطبيب المدخل."
              );
            }
           

            return attendanceRecords;

        }

        public async Task<object> GetAttendanceByCourseId(int courseId)
        {
            var attendanceRecords = await _attendanceRepositry.GetAttendanceByCourseId(courseId);
            if (!attendanceRecords.Any())
            {
                return new GeneralMsgDto(
               IErrorMsgs.COURSE_ID_NOT_FOUND,
               "رقم الكورس غير صالح",
               "لم يتم العثور على أي سجل حضور لهذا الكورس، الرجاء التحقق من رقم الكورس المدخل."
              );
            }
            return attendanceRecords;

        }
       public async Task<object> GetSubGroupForDoctorId(string doctorId)
        {
            var attendanceRecords = await _attendanceRepositry.GetSubGroupForDoctorId(doctorId);

            return attendanceRecords;
        }

       
        public async Task<object> GetAttendanceByDateRange(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
            {
                return new GeneralMsgDto(
                    IErrorMsgs.INVALID_DATE_RANGE,
                    "التواريخ غير صحيحة",
                    "تاريخ البداية يجب أن يكون أصغر من تاريخ النهاية"
                );
            }

            var attendanceRecords = await _attendanceRepositry.GetAttendanceByDateRange(startDate, endDate);

            if (!attendanceRecords.Any())
            {
                return new GeneralMsgDto(
                    IErrorMsgs.NO_ATTENDANCE_RECORDS_FOUND,
                    "لا يوجد بيانات",
                    "لم يتم العثور على أي سجلات حضور في الفترة المحددة"
                );
            }

            return attendanceRecords;
        }
        public async Task<object> GetAttendanceByDateAndDoctorId(DateOnly date, string doctorId)
        {
            var attendaceRecords = await _attendanceRepositry.GetAttendanceByDateAndDoctorId(date, doctorId);


            return attendaceRecords;
        }

    }
}

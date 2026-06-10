using System.Runtime.InteropServices;
using database.models;
using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using V1.DTO.AttendanceDTO;

namespace DevetionStudetns.Interface
{
    public interface IAttendanceRepositry
    {
        Task<IEnumerable<Attendance>> GetAllAtendance();
        Task<Attendance?> GetAttendanceById(int id);
        Task<GeneralMsgDto> AddAttendance(List<AddAttententsDto> attendances);
        Task<GeneralMsgDto> UpdateAttendance (Attendance attendance);
        Task<Attendance?> ExistingByDate(DateOnly attendanceDate);
        Task<GeneralMsgDto> DeleteAttendance (int id);
        Task<IEnumerable<GetAttendanceByStudentIdQDto>> GetAttendanceByStudentId(string studentId);
        Task<IEnumerable<GetAttendanceByDoctorIdQDto>> GetAttendanceByDoctorId(string doctorId);
        Task<IEnumerable<GetAttendanceByCourseIdQDto>> GetAttendanceByCourseId(int courseId);
        Task<IEnumerable<GetSubGroupForDoctorIdQDto>> GetSubGroupForDoctorId(string doctorId);
        Task<IEnumerable<GetAttendanceLastWeekQDto>> GetAttendanceByDateRange(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<GetAttendanceByDateANDDoctorIdQDto>> GetAttendanceByDateAndDoctorId(DateOnly date, string doctorId);
    }
}

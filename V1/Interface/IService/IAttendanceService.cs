using database.models;
using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using V1.DTO.AttendanceDTO;

namespace DevetionStudetns.Interface
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDTO>> GetAllAttendance();
        Task<AttendanceDTO?> GetAttendanceById (int id);
        Task<GeneralMsgDto> AddAttendance(List<AddAttententsDto> attendanceList);
        Task<GeneralMsgDto> UpdateAttendance(int attendanceId, AttendanceUpdateDTO updateAttendanceDTO);
        Task<GeneralMsgDto> DeleteAttendance(int id);
        Task<object> GetAttendanceByStudentId(string studnetId);
        Task<object> GetAttendanceByDoctorId(string doctorId);
        Task<object> GetAttendanceByCourseId(int courseId);
        Task<object> GetSubGroupForDoctorId(string doctorId);
        Task<object> GetAttendanceByDateRange(DateOnly startDate, DateOnly endDate);
        Task<object> GetAttendanceByDateAndDoctorId(DateOnly date, string doctorId);
    }
}

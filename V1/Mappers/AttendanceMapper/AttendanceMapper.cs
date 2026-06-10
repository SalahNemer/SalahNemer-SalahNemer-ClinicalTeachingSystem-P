using database.models;
using DevetionStudetns.DTO.AttendanceDTO;

namespace DevetionStudetns.Mappers.AttendanceMapper
{
    public static class AttendanceMapper
    {
        public static Attendance UpdateAttendance (this AttendanceUpdateDTO attendance )
        {
            return new Attendance
            {           
                StudentId = attendance.StudentId,
                AttendanceStatus = attendance.AttendanceStatus,
                Notes = attendance.Notes,
            };
        }
       
        public static AttendanceDTO ShowAttendance(this Attendance attendance) 
        {
            return new AttendanceDTO
            {
                AttendanceId = attendance.AttendanceId,
                CourseId = attendance.CourseId,
                StudentId = attendance.StudentId,
                DoctorId = attendance.DoctorId,
                AttendanceDate = attendance.AttendanceDate,
                AttendanceStatus = attendance.AttendanceStatus,
                Notes = attendance.Notes,
            };
        }

        public static Attendance AddAttendance(this CreateAttendanceDTO createAttendanceDTO)
        {
            return new Attendance
            {
                CourseId = createAttendanceDTO.CourseId,
                StudentId = createAttendanceDTO.StudentId,
                DoctorId = createAttendanceDTO.DoctorId,
                AttendanceDate = createAttendanceDTO.AttendanceDate,
                AttendanceStatus = createAttendanceDTO.AttendanceStatus,
                Notes = createAttendanceDTO.Notes,
            };
        }

        public static List<Attendance> AddAttendanceList(this List<CreateAttendanceDTO> createAttendanceDTOs)
        {
            return createAttendanceDTOs.Select(dto => dto.AddAttendance()).ToList();
        }


    }
}


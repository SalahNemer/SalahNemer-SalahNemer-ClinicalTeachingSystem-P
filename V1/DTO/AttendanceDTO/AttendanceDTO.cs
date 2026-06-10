using database.models;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.DTO.AttendanceDTO
{
    public class AttendanceDTO
    {
        public int AttendanceId { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public string DoctorId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public string AttendanceStatus { get; set; }
        public string? Notes { get; set; }
    }
}

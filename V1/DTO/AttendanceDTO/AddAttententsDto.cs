using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace V1.DTO.AttendanceDTO
{
    public class AddAttententsDto
    {        
        public string StudentId { get; set; }
        public string DoctorId { get; set; }   
        public string AttendanceStatus { get; set; } 
        public DateOnly AttendanceDate { get; set; }
        public string? Notes { get; set; }
    }
}

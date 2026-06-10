using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace V1.DTO.DistributionDTO
{
    public class GetDistbutionsQDto
    {
        public int DistributionId { get; set; } 
        public string? MainGroupSympole { get; set; }
        public string? SubGroupSympole { get; set;}
        public string? HospitalName { get; set; }
        public string? DepartmentName { get; set; }
        public string? RotationName { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? WeekName { get; set; }
        public DateOnly StartSessionDate { get; set; } 
        public DateOnly EndSessionDate { get; set; }
        public TimeOnly SessionStartTime { get; set; }
        public TimeOnly SessionEndTime { get; set; }
    }
}

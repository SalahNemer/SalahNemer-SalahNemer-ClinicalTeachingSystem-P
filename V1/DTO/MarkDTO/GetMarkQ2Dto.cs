using System.Collections.Concurrent;

namespace V1.DTO.MarkDTO
{
    public class GetMarkQ2Dto
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double CombinedFinalScore { get; set; }
        public double FinalWeeklyScore { get; set; }
    }
}

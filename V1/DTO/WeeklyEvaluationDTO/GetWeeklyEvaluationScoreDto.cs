namespace V1.DTO.WeeklyEvaluationDTO
{
    public class GetWeeklyEvaluationScoreDto
    {
        public string DoctorId { get; set; }
        public string FullName { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int NumberOfWeeks { get; set; }
        public float WeeklyRatingPercentage { get; set; }
        public double FinalWeeklyScore { get; set; }
    }
}

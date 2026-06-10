namespace V1.DTO.WeeklyEvaluationDTO
{
    public class UpdateWeeklyEvaluationDto
    {
        public string StudentId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public int AppointmentId { get; set; }
        public int EvaluationFormId { get; set; }
        public int EvaluationQuestionId { get; set; }
        public float TotalPoint { get; set; }
    }
}



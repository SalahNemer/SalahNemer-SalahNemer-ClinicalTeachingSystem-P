namespace V1.DTO.WeeklyEvaluationDTO
{
    public class AddWeeklyEvaluationDto
    {
        public float AnswerTheQuestion { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public int EvaluationFormId { get; set; }
        public int EvaluationQuestionId { get; set; }
    }
}

namespace V1.DTO.WeeklyEvaluationDTO
{
    public class GetEvaluationMarkByStudentIdQDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string DoctorId {  get; set; } = string.Empty;
        public string DoctorName { get; set;} = string.Empty;
        public string CourseCode {  get; set; } = string.Empty;
        public int EvaluationFormId { get; set; } 
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int QuestionMark {  get; set; } 
        public float AnswerTheQuestion { get; set; }
        public double TotalMark { get; set; }
        public int MarkCount { get; set; }
    }
}

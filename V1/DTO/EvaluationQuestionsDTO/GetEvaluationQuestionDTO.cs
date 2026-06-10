namespace V1.DTO.EvaluationQuestionsDTO
{
    public class GetEvaluationQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? QuestionMark { get; set; }
    }
}

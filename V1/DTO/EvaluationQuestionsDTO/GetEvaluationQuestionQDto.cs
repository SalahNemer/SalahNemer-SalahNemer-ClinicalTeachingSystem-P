namespace FinalProject.DTO.EvaluationQuestionsDTO
{
    public class GetEvaluationQuestionQDto
    {
        public int EvaluationFormId { get; set; }
        public string EvaluationFormType { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? QuestionMark { get; set; }

    }
}

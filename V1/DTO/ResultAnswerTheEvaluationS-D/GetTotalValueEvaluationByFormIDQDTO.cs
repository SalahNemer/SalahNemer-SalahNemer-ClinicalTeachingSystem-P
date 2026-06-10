namespace V1.DTO.ResultAnswerTheEvaluationS_D
{
    public class GetTotalValueEvaluationByFormIDQDTO
    {
        public int EvaluationFormId { get; set; }
        public string EvaluationFormType { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int CountAnswerTheQuestion { get; set; }
        public int TotalValue { get; set; }
        public int AverageValue { get; set; }
    }
}

namespace V1.DTO.AnswerTheEvaluationDTO
{
    public class GetDataValueAnswerTheEvaluationQDto
    {
        public int EvaluationFormId { get; set; }
        public string EvaluationFormType { get; set; }
        public int CountQuestion { get; set; }
        public int NumberOfStudent { get; set; }
        public int MaxPossibleScore { get; set; }
        public int TotalValue { get; set; }
        public decimal PercentageValue { get; set; }
    }
}

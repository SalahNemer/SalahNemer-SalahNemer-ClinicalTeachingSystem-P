namespace V1.DTO.AnswerTheEvaluationDTO
{
    public class GetValueAnswerTheEvaluationQDto
    {
        public int AnswerId { get; set; }
        public string EvaluatorPersonId { get; set; }
        public string EvaluatorFirstName { get; set; }  
        public string EvaluatorLastName { get; set; }  
        public string EvaluatorFullName { get; set; }  
        public string EvaluatedPersonId { get; set; }
        public string EvaluatedFirstName { get; set; }  
        public string EvaluatedLastName { get; set; }  
        public string EvaluatedFullName { get; set; } 
        public int EvaluationFormId { get; set; }
        public string EvaluationFormType { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string TheAnswer { get; set; }
        public int Value { get; set; }
    }
}

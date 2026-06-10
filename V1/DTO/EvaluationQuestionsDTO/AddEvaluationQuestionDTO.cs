using System.ComponentModel.DataAnnotations;

namespace V1.DTO.EvaluationQuestionsDTO
{
    public class AddEvaluationQuestionDTO
    {
        [Required]
        [MaxLength(500)]
        public string QuestionText { get; set; }
        [Required]
        public string QuestionType { get; set; }
        [Required]
        public int QuestionMark { get; set; }
    }
}

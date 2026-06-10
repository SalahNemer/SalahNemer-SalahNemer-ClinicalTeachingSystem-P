using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataBase.Entity;

namespace DevetionStudetns.entitys
{
    [Table("EvaluationQuestions")]
    public class EvaluationQuestion
    {
        [Key]
        [Column("QuestionId")]
        public int QuestionId { get; set; }
        [Required]
        [Column("QuestionText")]
        [MaxLength(500)]
        public string QuestionText { get; set; }
        [Required]
        [Column("QuestionType")]
        public string QuestionType { get; set; }
        public int? QuestionMark { get; set; }

        public ICollection<EvaluationForm_EvaluationQuestion> EvaluationForm_EvaluationQuestions { get; set; }
        public ICollection<AnswerTheEvaluation> AnswerTheEvaluation { get; set; }
        public ICollection<WeeklyEvaluation> WeeklyEvaluation { get; set; } 

    }
}

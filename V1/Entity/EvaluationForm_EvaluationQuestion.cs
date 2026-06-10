using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevetionStudetns.entitys
{
    [Table("EvaluationForm_EvaluationQuestions")]
    public class EvaluationForm_EvaluationQuestion
    {
        [Required]
        [Column("EvaluationFormId")]
        public int EvaluationFormId { get; set; }
        [ForeignKey("EvaluationFormId")]
        public EvaluationForm EvaluationForm { get; set; }
        [Required]
        [Column("EvaluationQuestionId ")]
        public int EvaluationQuestionId { get; set; }
        [ForeignKey("EvaluationQuestionId")]
        public EvaluationQuestion EvaluationQuestions { get; set; }
    }
}

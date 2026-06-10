using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataBase.Entity;

namespace DevetionStudetns.entitys
{
    [Table("EvaluationForm")]
    public class EvaluationForm
    {
        [Key]
        [Column("EvaluationFormId")]
        public int EvaluationFormId { get; set; }
        [Required]
        [Column("EvaluationFormType")] 
        public string EvaluationFormType { get; set; }
        public ICollection<EvaluationForm_EvaluationQuestion> EvaluationForm_EvaluationQuestions { get; set; }
        public ICollection<AnswerTheEvaluation> AnswerTheEvaluation { get; set; }
        public ICollection<WeeklyEvaluation> weeklyEvaluations { get; set; }
    }
}

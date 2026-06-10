using DataBase.entitys;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.entitys
{
    [Table("AnswerTheEvaluation")]
    public class AnswerTheEvaluation
    {
        [Key]
        [Column("AnswerId ")]
        public int AnswerId { get; set; }

        [Required]
        [Column("EvaluatorPersonId")]
        [MaxLength(20)]
        public string EvaluatorPersonId { get; set; } 
        [ForeignKey("EvaluatorPersonId")]
        public Users EvaluatorPerson { get; set; }

        [Required]
        [Column("EvaluatedPersonId")]
        [MaxLength(20)]
        public string EvaluatedPersonId { get; set; } 
        [ForeignKey("EvaluatedPersonId")]
        public Users EvaluatedPerson { get; set; }

        [Required]
        [Column("EvaluationFormId")] 
        public int EvaluationFormId { get; set; }
        [ForeignKey("EvaluationFormId")]
        public EvaluationForm EvaluationForm { get; set; }

        [Required]
        [Column("QuestionId")]
        public int QuestionId { get; set; } 
        [ForeignKey("QuestionId")]
        public EvaluationQuestion EvaluationQuestions { get; set; }

        [Required]
        [Column("TheAnswer")]
        public string TheAnswer { get; set; }
        public DateTime DateTimeAnswer { get; set; } = DateTime.Now;
        public int? Appointmentid { get; set; }
        [ForeignKey("Appointmentid")]
        public Appointment Appointment { get; set; }
    }
}

using DevetionStudetns.entitys;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace FinalProject.DTO.AnswerTheEvaluationDTO
{
    public class AddAnswerTheEvaluationDto
    {
        [Required]
        public string EvaluatorPersonId { get; set; } 
        [Required]
        public string EvaluatedPersonId { get; set; }
        [Required]
        public int EvaluationFormId { get; set; }
        [Required]
        public int QuestionId { get; set; } 
        [Required]
        public string TheAnswer { get; set; }
    }
}

using DevetionStudetns.entitys;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;
using System.Text.Json.Serialization;

namespace FinalProject.DTO.AnswerTheEvaluationDTO
{
    public class GetAnswerTheEvaluationQDto
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
        [JsonIgnore]
        public DateTime DateTimeAnswer { get; set; } = DateTime.Now;
        public string FormattedDateTime => DateTimeAnswer.ToString("yyyy-MM-dd / HH:mm:ss");
    }
}

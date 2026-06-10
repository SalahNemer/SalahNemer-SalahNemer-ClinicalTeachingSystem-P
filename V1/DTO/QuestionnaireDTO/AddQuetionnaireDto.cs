using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace FinalProject.DTO.QuestionnaireDTO
{
    public class AddQuetionnaireDto
    {
        [Required(ErrorMessage = "This column is required.")]
        [MinLength(8, ErrorMessage = "الاسم يجب أن يحتوي عل 8 خانات على الأقل.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public int ConcernedParty { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string QuestionnaireName { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string LinkQuestionnaire { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        [Range(0, 1, ErrorMessage = "must the Account Status  0 or 1 ")]
        public int QuestionnaireStatus { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string QuestionnaireType { get; set; }
    }
}

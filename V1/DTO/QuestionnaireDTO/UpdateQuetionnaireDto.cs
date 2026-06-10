using System.ComponentModel.DataAnnotations;

namespace V1.DTO.QuestionnaireDTO
{
    public class UpdateQuetionnaireDto
    {
        [Required(ErrorMessage = "This column is required.")]
        public int ConcernedParty { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string LinkQuestionnaire { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        [Range(0, 1, ErrorMessage = "must the Account Status  0 or 1 ")]
        public int QuestionnaireStatus { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string QuestionnaireName { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string QuestionnaireType { get; set; }
    }
}

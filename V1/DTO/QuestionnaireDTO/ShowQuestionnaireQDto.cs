using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.QuestionnaireDTO
{
    public class ShowQuestionnaireQDto
    {
        public int QuestionnaireId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int ConcernedParty { get; set; }
        public string LinkQuestionnaire { get; set; }
        public int QuestionnaireStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string QuestionnaireName { get; set; }
    }
}

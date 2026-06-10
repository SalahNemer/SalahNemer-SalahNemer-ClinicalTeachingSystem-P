using FinalProject.DTO.QuestionnaireDTO;
using loginpage.DBcon;
using V1.DTO.QuestionnaireDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IQuestionnaire
    {
        public GeneralMsgDto DeleteQuestionnair(int QuestionnairId);
        public GeneralMsgDto AddQuestionnaire(AddQuetionnaireDto addQuetionnaireDto);
        public GeneralMsgDto UpdateQuestionnair(UpdateQuetionnaireDto NewQuestionnairData, int QuestionnairId);
        public List<ShowQuestionnaireQDto> ShowAllQuestionnaire(string QuestionnaireType);
        public List<ShowQuestionnaireQDto> ShowQuestionnaireById(int QuestionnaireId);
        public List<ShowQuestionnaireQDto> ShowQuestionnaireByUserId(string UserId, string QuestionnaireType);
    }
}

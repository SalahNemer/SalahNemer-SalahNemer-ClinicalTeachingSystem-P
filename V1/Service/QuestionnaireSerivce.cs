using FinalProject.DTO.QuestionnaireDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.QuestionnaireDTO;

namespace FinalProject.Service
{
    public class QuestionnaireSerivce
    {
        private readonly IQuestionnaire _context;
        public QuestionnaireSerivce(IQuestionnaire context)
        {
            _context = context;
        }   

        public GeneralMsgDto DeleteQuestionnairService(int QuestionnairId)
        {
            return _context.DeleteQuestionnair(QuestionnairId);
        }
        public GeneralMsgDto AddQuestionnaireService(AddQuetionnaireDto addQuetionnaireDto)
        {
            return _context.AddQuestionnaire(addQuetionnaireDto);
        }
        public GeneralMsgDto UpdateQuestionnairService(UpdateQuetionnaireDto NewQuestionnairData, int QuestionnairId)
        {
            return _context.UpdateQuestionnair ( NewQuestionnairData , QuestionnairId);
        }
        public List<ShowQuestionnaireQDto> ShowAllQuestionnaireService(string QuestionnaireType)
        {
            return _context.ShowAllQuestionnaire( QuestionnaireType);
        }
        public List<ShowQuestionnaireQDto> ShowQuestionnaireByIdService(int QuestionnaireId)
        {
            return _context.ShowQuestionnaireById (QuestionnaireId);
        }
        public List<ShowQuestionnaireQDto> ShowQuestionnaireByUserIdService(string UserId, string QuestionnaireType)
        {
            return _context.ShowQuestionnaireByUserId (UserId,  QuestionnaireType);
        }

    }
}

using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationQuestionsDTO;
using loginpage.DBcon;
using V1.DTO.EvaluationQuestionsDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IEvaluationQuestionsRepo
    {
        public Task<List<GetEvaluationQuestionDTO>> GetEvaluationQuestionsAsync();
        public Task<GetEvaluationQuestionDTO> GetEvaluationQuestionsByIdAsync(int QuestionId);
        public Task<GeneralMsgDto> AddEvaluationQuestionsAsync(AddEvaluationQuestionDTO evaluationQuestions);
        public Task<GeneralMsgDto> DeleteEvaluationQuestionsAsync(int id);
        public Task<GeneralMsgDto> UpdateEvaluationQuestionsAsync(int EvaluationID, AddEvaluationQuestionDTO EvaluationQuestionsData);
        public Task<List<GetEvaluationQuestionQDto>> GetEvaluationQuestionByFormId(int formid);
        public Task<List<GetEvaluationQuestionQDto>> GetEvaluationQuestionByFormName(string formName);
    }
}

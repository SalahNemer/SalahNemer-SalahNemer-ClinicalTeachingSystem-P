using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.EvaluationQuestionsDTO;

namespace FinalProject.Service
{
    public class EvaluationQuestionsServes
    {
        readonly private IEvaluationQuestionsRepo _evaluationQuestionsRepo;
        public EvaluationQuestionsServes(IEvaluationQuestionsRepo evaluationQuestionsRepo)
        {
            _evaluationQuestionsRepo = evaluationQuestionsRepo;
        }

        public async Task<List<GetEvaluationQuestionDTO>> getEvaluationQuestions()
        {
            return await _evaluationQuestionsRepo.GetEvaluationQuestionsAsync();
        }
        public async Task<GeneralMsgDto> addEvaluationQuestionsAsync(AddEvaluationQuestionDTO evaluationQuestions)
        {
            return await _evaluationQuestionsRepo.AddEvaluationQuestionsAsync(evaluationQuestions);
        }
        public async Task<GeneralMsgDto> addEvaluationQuestionsHD_D_Async(AddEvaluationQuestionDTO evaluationQuestions)
        {
            return await _evaluationQuestionsRepo.AddEvaluationQuestionsAsync(evaluationQuestions);
        }
        public async Task<GeneralMsgDto> DeleteEvaluationQuestionsAsync(int EvaluationId)
        {
            return await _evaluationQuestionsRepo.DeleteEvaluationQuestionsAsync(EvaluationId);
        }
        public async Task<GeneralMsgDto> UpdateEvaluationQuestionsAsync(int EvaluationID, AddEvaluationQuestionDTO EvaluationQuestionsData)
        {
            return await _evaluationQuestionsRepo.UpdateEvaluationQuestionsAsync(EvaluationID, EvaluationQuestionsData);
        }
        public async Task<List<GetEvaluationQuestionQDto>> getEvaluationQuestionByFormId(int evaluationFormId)
        {
            return await _evaluationQuestionsRepo.GetEvaluationQuestionByFormId(evaluationFormId);
        }
        public async Task<GetEvaluationQuestionDTO> getEvaluationQuestionsByIdAsync(int QuestionId)
        {
            return await _evaluationQuestionsRepo.GetEvaluationQuestionsByIdAsync(QuestionId);
        }

        public async Task<List<GetEvaluationQuestionQDto>> getEvaluationQuestionByFormName(string formName)
        {
            return await _evaluationQuestionsRepo.GetEvaluationQuestionByFormName(formName);
        }



    }
}

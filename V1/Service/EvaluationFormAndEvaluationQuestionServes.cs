using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationForm_EvaluationQuestionDTO;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace FinalProject.Service
{
    public class EvaluationFormAndEvaluationQuestionServes
    {
        readonly private IEvaluationFormAndQuestionRepo _repo;
        public EvaluationFormAndEvaluationQuestionServes(IEvaluationFormAndQuestionRepo repo)
        {
            _repo = repo;
        }
        public async Task<List<GetEvaluationFormAndEvaluationQuestionDto>> GetEvaluationFormAndEvaluationQuestion()
        {
            return await _repo.GetEvaluationFormAndEvaluationQuestion();
        }
        public async Task<GetEvaluationFormAndEvaluationQuestionDto> getEvaluationFormAndEvaluationQuestionById(int FormId, int QuestionId)
        {
            return await _repo.GetEvaluationFormAndEvaluationQuestionById(FormId,QuestionId);
        }

        public async Task<GeneralMsgDto> AddEvaluationFormAndEvaluationQuestion(AddEvaluationFormAndEvaluationQuestionDto evaluationQuestions)
        {
            return await _repo.AddEvaluationFormAndEvaluationQuestion(evaluationQuestions);
        }
        public async Task<GeneralMsgDto> UpdateEvaluationFormAndEvaluationQuestion(int EvaluationFormId, int EvaluationQuestionId, UpdateEvaluationFormAndEvaluationQuestionDto evaluationFormQuestionDto)
        {
            return await _repo.UpdateEvaluationFormAndEvaluationQuestion(EvaluationFormId, EvaluationQuestionId, evaluationFormQuestionDto);
        }
        public async Task<GeneralMsgDto> DeleteEvaluationFormAndEvaluationQuestion(int FormId,int QuestionId)
        {
            return await _repo.DeleteEvaluationFormAndEvaluationQuestion(FormId, QuestionId);
        }
    }
}

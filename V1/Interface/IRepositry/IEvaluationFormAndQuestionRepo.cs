using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationForm_EvaluationQuestionDTO;
using FinalProject.DTO.EvaluationFormDto;
using loginpage.DBcon;
using Microsoft.EntityFrameworkCore.Query;

namespace FinalProject.Interface.IRepositry
{
    public interface IEvaluationFormAndQuestionRepo
    {
        public Task<List<GetEvaluationFormAndEvaluationQuestionDto>> GetEvaluationFormAndEvaluationQuestion();
        public Task<GetEvaluationFormAndEvaluationQuestionDto> GetEvaluationFormAndEvaluationQuestionById(int FormId,int QuestionId);
        public Task<GeneralMsgDto> AddEvaluationFormAndEvaluationQuestion(AddEvaluationFormAndEvaluationQuestionDto evaluationFormDto);
        public Task<GeneralMsgDto> DeleteEvaluationFormAndEvaluationQuestion(int QuestionId,int FormId);
        public Task<GeneralMsgDto> UpdateEvaluationFormAndEvaluationQuestion(int EvaluationFormId,int EvaluationQuestionId, UpdateEvaluationFormAndEvaluationQuestionDto evaluationFormQuestionDto);
    }
}

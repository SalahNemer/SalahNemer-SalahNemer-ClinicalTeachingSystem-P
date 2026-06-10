using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.DTO.EvaluationQuestionsDTO;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface IEvaluationFormRepo
    {
        public Task<List<GetEvaluationFormDto>> GetEvaluationFormAsync();
        public Task<GeneralMsgDto> AddEvaluationFormAsync(AddEvaluationFormDto evaluationFormDto);
        public Task<GeneralMsgDto> DeleteEvaluationFormAsync(int id);
        public Task<GeneralMsgDto> UpdateEvaluationFormAsync(int EvaluationID, AddEvaluationFormDto evaluationFormDto);
        public Task<GetEvaluationFormDto> GetEvaluationForByIdAsync(int EvaluationFormID);
    }
}

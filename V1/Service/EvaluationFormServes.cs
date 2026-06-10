using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace FinalProject.Service
{
    public class EvaluationFormServes
    {
        readonly private IEvaluationFormRepo _EvaluationFormRepo;
        public EvaluationFormServes(IEvaluationFormRepo evalFormRepo)
        {
            _EvaluationFormRepo = evalFormRepo;
        }

        public async Task<List<GetEvaluationFormDto>> getEvaluationForm()
        {
            return await _EvaluationFormRepo.GetEvaluationFormAsync();
        }
        public async Task<GeneralMsgDto> addEvaluationFormAsync(AddEvaluationFormDto evaluationQuestions)
        {
            return await _EvaluationFormRepo.AddEvaluationFormAsync(evaluationQuestions);
        }
        public async Task<GeneralMsgDto> DeleteEvaluationFormAsync(int EvaluationId)
        {
            return await _EvaluationFormRepo.DeleteEvaluationFormAsync(EvaluationId);
        }
        public async Task<GeneralMsgDto> UpdateEvaluationFormAsync(int EvaluationID, AddEvaluationFormDto EvaluationQuestionsData)
        {
            return await _EvaluationFormRepo.UpdateEvaluationFormAsync(EvaluationID, EvaluationQuestionsData);
        }
        public async Task<GetEvaluationFormDto> getEvaluationForByIdAsync(int EvaluationFormID)
        {
            return await _EvaluationFormRepo.GetEvaluationForByIdAsync(EvaluationFormID);
        }

    }
}

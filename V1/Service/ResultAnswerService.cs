using testDtoAndmapper.Entity;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.ResultAnswerTheEvaluationS_D;
using V1.DTO.StudentsDTO;
using V1.Interface.IRepositry;

namespace V1.Service
{
    public class ResultAnswerService
    {
        private readonly IResultAnswer _resultAnswer;
        public ResultAnswerService(IResultAnswer resultAnswer)
        {
            _resultAnswer = resultAnswer;
        }
        public async Task<List<GetValueAnswerTheEvaluationQDto>> GetData(int formid)
        {
            return await _resultAnswer.GetResultAnswerTheEvaluationByFormId(formid);
        }
        public async Task<List<GetTotalValueEvaluationByFormIDQDTO>> GetTotalByFormID(int formid)
        {
            return await _resultAnswer.GetTotalValueEvaluationByFormId(formid);
        }
        public async Task<List<GetDataValueAnswerTheEvaluationQDto>> ShowTotalEvaluationDoctorId(int formid, string doctorid)
        {
            return await _resultAnswer.ShowTotalEvaluationDoctorByForm(formid, doctorid);
        }
    }
}

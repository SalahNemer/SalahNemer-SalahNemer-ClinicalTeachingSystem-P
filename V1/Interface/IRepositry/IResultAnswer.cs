using Microsoft.AspNetCore.Mvc;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.ResultAnswerTheEvaluationS_D;
using V1.DTO.StudentsDTO;

namespace V1.Interface.IRepositry
{
    public interface IResultAnswer
    {
        public Task<List<GetValueAnswerTheEvaluationQDto>> GetResultAnswerTheEvaluationByFormId(int formid);
        public Task<List<GetTotalValueEvaluationByFormIDQDTO>> GetTotalValueEvaluationByFormId(int formid);
        public Task<List<GetDataValueAnswerTheEvaluationQDto>> ShowTotalEvaluationDoctorByForm(int formid, string doctorid);
    }
}

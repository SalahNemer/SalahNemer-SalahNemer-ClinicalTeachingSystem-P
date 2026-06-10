using FinalProject.DTO.AnswerTheEvaluationDTO;
using FinalProject.DTO.EvaluationFormDto;
using loginpage.DBcon;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.StudentsDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IAnswerTheEvaluationRepo
    {
        public Task<List<GetAnswerTheEvaluationQDto>> GetAnswerTheEvaluaion();
        public Task<GetAnswerTheEvaluationQDto> GetAnswerTheEvaluaionByAnswerId(int id);
        public Task<GeneralMsgDto> AddAnswerTheEvaluation(AddAnswerTheEvaluationDto evaluationAnswerDto);
        public Task<GeneralMsgDto> DeleteAnswerTheEvaluation(int id);
        public Task<List<GetDataDoctorsByStudentIdQDTO>> ShowDataDoctorByStudentId(string studentid);
        public List<GetAllDoctorInTheSameDepartmentFroEvaluationQDto> ShowAllDoctorInTheDepartmentByDepartmentHeadId(string DepartmentHeadId);
        public Task<GeneralMsgDto> AddAnswerTheEvaluationForTheDepartmentHead(AddAnswerTheEvaluationDto evaluationAnswerDto);
    }
}

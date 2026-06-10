using FinalProject.DTO.AnswerTheEvaluationDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Repositry;
using loginpage.DBcon;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.StudentsDTO;
using V1.Interface.IRepositry;

namespace FinalProject.Service
{
    public class AnswerTheEvaluationServes
    {
        readonly private IAnswerTheEvaluationRepo _answerTheEvakluationReposoitry;
        public AnswerTheEvaluationServes(IAnswerTheEvaluationRepo answerTheEvakluationReposoitry)
        {
            _answerTheEvakluationReposoitry = answerTheEvakluationReposoitry;
        }
        public async Task<List<GetAnswerTheEvaluationQDto>> getAnswerTheEvaluaion()
        {
            return await _answerTheEvakluationReposoitry.GetAnswerTheEvaluaion();
        }
        public async Task<List<GetDataDoctorsByStudentIdQDTO>> ShowDataDoctorByStudentId(string studentid)
        {
            return await _answerTheEvakluationReposoitry.ShowDataDoctorByStudentId(studentid);
        }
        public async Task<GeneralMsgDto> addAnswerTheEvaluation(AddAnswerTheEvaluationDto evaluationAnswerDto)
        {
            return await _answerTheEvakluationReposoitry.AddAnswerTheEvaluation(evaluationAnswerDto);
        }
        public async Task<GetAnswerTheEvaluationQDto> getAnswerTheEvaluaionByAnswerId(int id)
        {
            return await _answerTheEvakluationReposoitry.GetAnswerTheEvaluaionByAnswerId(id);
        }
        public async Task<GeneralMsgDto> deleteAnswerTheEvaluation(int id)
        {
            return await _answerTheEvakluationReposoitry.DeleteAnswerTheEvaluation(id);
        }
        public List<GetAllDoctorInTheSameDepartmentFroEvaluationQDto> ShowAllDoctorInTheDepartmentByDepartmentHeadId(string DepartmentHeadId)
        {
            return _answerTheEvakluationReposoitry.ShowAllDoctorInTheDepartmentByDepartmentHeadId(DepartmentHeadId);
        }
        public async Task<GeneralMsgDto> AddAnswerTheEvaluationForTheDepartmentHead(AddAnswerTheEvaluationDto evaluationAnswerDto)
        {
            return await _answerTheEvakluationReposoitry.AddAnswerTheEvaluationForTheDepartmentHead(evaluationAnswerDto);

        }


    }
}

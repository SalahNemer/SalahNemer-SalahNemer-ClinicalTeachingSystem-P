using DataBase.Entity;
using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using V1.DTO.WeeklyEvaluationDTO;

namespace V1.Interface.IRepositry
{
    public interface IWeeklyEvaluationRepo
    {
        Task<IEnumerable<GetWeeklyEvaluationDto>> GetAllWeeklyEvaluations();
        Task<WeeklyEvaluation?> GetWeeklyEvaluationById(int id);
        Task<GeneralMsgDto> AddWeeklyEvaluation (WeeklyEvaluation weeklyEvaluation);
        Task<GeneralMsgDto> UpdateWeeklyEvaluation(WeeklyEvaluation weeklyEvaluation); 
        Task <GeneralMsgDto> DeleteWeeklyEvaluation (int id);
        Task<IEnumerable<GetEvaluationMarkByStudentIdQDto>> GetEvaluationMarkByStudentId (string studentId);
        Task<IEnumerable<GetSubGroupForDoctorIdQDto>> GetAllSubGroupByDoctroId(string doctorId);
        public List<GetWeeklyEvaluationScoreDto> GetSumOfEvaluationWeekByStudentsId(string StudentsId);
    }
}

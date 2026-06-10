using DataBase.Entity;
using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using V1.DTO.WeeklyEvaluationDTO;

namespace V1.Interface.IService
{
    public interface IWeeklyEvaluationService
    {
        Task<IEnumerable<GetWeeklyEvaluationDto>> GetAllWeeklyEvaluations();
        Task<GetWeeklyEvaluationDto?> GetWeeklyEvaluationById(int id);
        Task<GeneralMsgDto> AddWeeklyEvaluation(AddWeeklyEvaluationDto weeklyEvaluation);
        Task<GeneralMsgDto> UpdateWeeklyEvaluation(int id,UpdateWeeklyEvaluationDto weeklyEvaluation);
        Task<GeneralMsgDto> DeleteWeeklyEvaluation(int id);
        Task<object> GetEvaluationMarkByStudentId(string studnetId);
        Task<object> GetSubGroupByDoctroId(string docotrId);
        public List<GetWeeklyEvaluationScoreDto> GetSumOfEvaluationWeekByStudentsId(string StudentsId);
    }
}

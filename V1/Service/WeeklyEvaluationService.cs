using DataBase.Entity;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.Mappers.TAMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using V1.DTO.WeeklyEvaluationDTO;
using V1.Interface.IRepositry;
using V1.Interface.IService;
using V1.Mappers.WeeklyEvaluationMapper;

namespace V1.Service
{
    public class WeeklyEvaluationService : IWeeklyEvaluationService
    {

        private readonly IWeeklyEvaluationRepo _weeklyEvaluationRepo;

        public WeeklyEvaluationService(IWeeklyEvaluationRepo weeklyEvaluationRepo)
        {
            _weeklyEvaluationRepo = weeklyEvaluationRepo;
        }



        public async Task<IEnumerable<GetWeeklyEvaluationDto>> GetAllWeeklyEvaluations()
        {
            return await _weeklyEvaluationRepo.GetAllWeeklyEvaluations();
        }
        public async Task<GetWeeklyEvaluationDto?> GetWeeklyEvaluationById(int id)
        {
            var getById = await _weeklyEvaluationRepo.GetWeeklyEvaluationById(id);
            return getById != null ? WeeklyEvaluationMapper.GetWeeklyEvaluation(getById) : null;
        }
        public async Task<GeneralMsgDto> AddWeeklyEvaluation(AddWeeklyEvaluationDto weeklyEvaluation)
        {
            return await _weeklyEvaluationRepo.AddWeeklyEvaluation(WeeklyEvaluationMapper.AddWeeklyEvaluation(weeklyEvaluation));
        }
        public async Task<GeneralMsgDto> UpdateWeeklyEvaluation(int id ,UpdateWeeklyEvaluationDto weeklyEvaluation)
        {
           var existingEval = await _weeklyEvaluationRepo.GetWeeklyEvaluationById(id);
            if (existingEval == null)
                return new GeneralMsgDto(IErrorMsgs.WEEKLY_EVALUATION_NOT_FOUND,"خطأ في التقييم الأسبوعي", "لم يتم العثور على التقييم الأسبوعي الذي تبحث عنه. من الممكن أن الرقم المدخل غير صحيح أو أنه لم يتم إضافته بعد. يرجى التحقق من المدخلات."    );


            if (weeklyEvaluation.TotalPoint < 2 || weeklyEvaluation.TotalPoint > 10)
            {
                return new GeneralMsgDto(
                    IErrorMsgs.INVALID_TOTAL_POINTS,
                    "خطأ في مجموع النقاط",
                    "يجب أن يكون مجموع النقاط بين 2 و 10 فقط."
                );
            }
            existingEval.StudentId = weeklyEvaluation.StudentId;
            existingEval.CourseId = weeklyEvaluation.CourseId;
            existingEval.DoctorId  =    weeklyEvaluation.DoctorId;
            existingEval.AppointmentId =  weeklyEvaluation.AppointmentId;
            existingEval.EvaluationFormId = weeklyEvaluation.EvaluationFormId;
            existingEval.EvaluationQuestionId = weeklyEvaluation.EvaluationQuestionId;
            
            return (await _weeklyEvaluationRepo.UpdateWeeklyEvaluation(existingEval));  
        }
        public async Task<GeneralMsgDto> DeleteWeeklyEvaluation(int id)
        {
            return await _weeklyEvaluationRepo.DeleteWeeklyEvaluation(id);
        }
        public async Task<object> GetEvaluationMarkByStudentId(string studentId)
        {
            var evaluationMark = await _weeklyEvaluationRepo.GetEvaluationMarkByStudentId(studentId);
            if(!evaluationMark.Any())
            {
                return new GeneralMsgDto(
                IErrorMsgs.STUDENT_ID_NOT_FOUND,
                "رقم الطالب غير صالح",
                "لم يتم العثور على الطالب، الرجاء التحقق من رقم الطالب المدخل."
                    );
            }
            return evaluationMark;

        }
        public async Task<object> GetSubGroupByDoctroId(string docotrId)
        {
            var subGroups = await _weeklyEvaluationRepo.GetAllSubGroupByDoctroId(docotrId); 
            if(!subGroups.Any())
            {
                return new GeneralMsgDto(
                               IErrorMsgs.NOT_EXIST_SUB_GROUP_WITH_DOCTOR,
                       "لا يوجد بيانات",
               "لم يتم العثور على أي مجموعة مع معرف الطبيب المدخل, الرجاء التأكد من معرف الطبيب المدخل"
              );

            }
            return subGroups;

        }

        public List<GetWeeklyEvaluationScoreDto> GetSumOfEvaluationWeekByStudentsId(string StudentsId)
        {
            return _weeklyEvaluationRepo.GetSumOfEvaluationWeekByStudentsId(StudentsId);
        }


    }
}

using DataBase.Entity;
using V1.DTO.WeeklyEvaluationDTO;

namespace V1.Mappers.WeeklyEvaluationMapper
{
    public static class WeeklyEvaluationMapper
    {

        public static WeeklyEvaluation AddWeeklyEvaluation(this AddWeeklyEvaluationDto dto)
        {
            return new WeeklyEvaluation
            {
                DoctorId = dto.DoctorId,
                StudentId = dto.StudentId,
                EvaluationFormId = dto.EvaluationFormId,
                EvaluationQuestionId = dto.EvaluationQuestionId,
                AnswerTheQuestion = dto.AnswerTheQuestion,
            };
        }
        public static WeeklyEvaluation UpdateWeeklyEvaluation(this UpdateWeeklyEvaluationDto dto)
        {
            return new WeeklyEvaluation
            {
                DoctorId = dto.DoctorId,
                StudentId = dto.StudentId,
                EvaluationFormId = dto.EvaluationFormId,
                EvaluationQuestionId = dto.EvaluationQuestionId,
            };
        }

        public static GetWeeklyEvaluationDto GetWeeklyEvaluation (WeeklyEvaluation weeklyEvaluation)
        {
            return new GetWeeklyEvaluationDto
            {
                WeeklyEvaluationId = weeklyEvaluation.WeeklyEvaluationId,
                AppointmentId = weeklyEvaluation.AppointmentId,
                CourseId = weeklyEvaluation.CourseId,
                DoctorId = weeklyEvaluation.DoctorId,
                StudentId = weeklyEvaluation.StudentId,
                EvaluationFormId = weeklyEvaluation.EvaluationFormId,
                EvaluationQuestionId = weeklyEvaluation.EvaluationQuestionId,
                AnswerTheQuestion = weeklyEvaluation.AnswerTheQuestion,
                EntryDate = weeklyEvaluation.EntryDate,
            };
        }

    }
}

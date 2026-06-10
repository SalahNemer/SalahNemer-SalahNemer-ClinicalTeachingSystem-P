using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationForm_EvaluationQuestionDTO;

namespace FinalProject.Mappers.EvaluationForm_EvaluationQuestionMapper
{
    public static class EvaluationFormAndEvaluationQuestionMapp
    {
        public static GetEvaluationFormAndEvaluationQuestionDto GetEvaluationFormAndQuestionMapp(this EvaluationForm_EvaluationQuestion model)
        {
            return new GetEvaluationFormAndEvaluationQuestionDto
            {
                EvaluationFormId = model.EvaluationFormId,
                EvaluationQuestionId = model.EvaluationQuestionId,
            };
        }

        public static EvaluationForm_EvaluationQuestion AddEvaluationFormAndQuestionMapp(this AddEvaluationFormAndEvaluationQuestionDto model)
        {
            return new EvaluationForm_EvaluationQuestion
            {
                EvaluationFormId=model.EvaluationFormId,
                EvaluationQuestionId=model.EvaluationQuestionId,
            };
        }
        public static EvaluationForm_EvaluationQuestion UpdateEvaluationFormAndQuestionMapp(this UpdateEvaluationFormAndEvaluationQuestionDto model)
        {
            return new EvaluationForm_EvaluationQuestion
            {
                EvaluationQuestionId = model.EvaluationQuestionId,
            };
        }

    }
}

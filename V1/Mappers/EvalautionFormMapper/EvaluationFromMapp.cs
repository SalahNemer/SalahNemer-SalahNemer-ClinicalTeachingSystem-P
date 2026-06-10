using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationFormDto;

namespace FinalProject.Mappers.EvalautionFormMapper
{
    public static class EvaluationFromMapp
    {
        public static EvaluationForm evaluationForm(this AddEvaluationFormDto dto)
        {
            return new EvaluationForm
            {
                EvaluationFormType= dto.EvaluationFormType,
            };
        }
        public static GetEvaluationFormDto  GetEvaluationFormMapp(this EvaluationForm dto)
        {
            return new GetEvaluationFormDto
            {
                EvaluationFormId= dto.EvaluationFormId,
                EvaluationFormType = dto.EvaluationFormType,
            };
        }
    }
}

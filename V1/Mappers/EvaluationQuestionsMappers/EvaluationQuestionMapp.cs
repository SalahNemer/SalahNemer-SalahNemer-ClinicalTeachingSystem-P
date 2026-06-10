using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationQuestionsDTO;
using V1.DTO.EvaluationQuestionsDTO;

namespace FinalProject.Mappers.EvaluationQuestionsMappers
{
    public static class EvaluationQuestionMapp
    {
        public static GetEvaluationQuestionDTO GetEvaluationQuestionsMapp(this EvaluationQuestion evaluationQuestionsDto)
        {
            return new GetEvaluationQuestionDTO
            {
                QuestionId = evaluationQuestionsDto.QuestionId,
                QuestionType = evaluationQuestionsDto.QuestionType,
                QuestionText = evaluationQuestionsDto.QuestionText,
                QuestionMark = evaluationQuestionsDto.QuestionMark,

            };
        }
        public static EvaluationQuestion AddEvaluationQuestionMapper(this AddEvaluationQuestionDTO evaluationQuestionsDto)
        {
            return new EvaluationQuestion
            {
                QuestionType = evaluationQuestionsDto.QuestionType,
                QuestionText = evaluationQuestionsDto.QuestionText,
                QuestionMark = evaluationQuestionsDto.QuestionMark,
            };
        }
    }
}

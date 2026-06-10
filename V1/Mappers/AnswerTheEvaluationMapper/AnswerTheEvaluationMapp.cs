using DevetionStudetns.entitys;
using FinalProject.DTO.AnswerTheEvaluationDTO;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace FinalProject.Mappers.AnswerTheEvaluationMapper
{
    public static class AnswerTheEvaluationMapp
    {
        public static AnswerTheEvaluation AddAnswerDto(this AddAnswerTheEvaluationDto answerTheEvaluation)
        {
            return new AnswerTheEvaluation
            {
                EvaluatorPersonId = answerTheEvaluation.EvaluatorPersonId,
                EvaluatedPersonId= answerTheEvaluation.EvaluatedPersonId,
                EvaluationFormId = answerTheEvaluation.EvaluationFormId,
                QuestionId = answerTheEvaluation.QuestionId,
                TheAnswer= answerTheEvaluation.TheAnswer,
            };
        }
    }
}


using BuildDB_Team.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using FinalProject.DTO.QuestionnaireDTO;

namespace FinalProject.Mappers.QuestionnaireMapper
{
    public static class AddQuestionnaireMapper
    {
        public static Questionnaire AddQuestionnaire(this AddQuetionnaireDto addAppointmentsDto)
        {

            return new Questionnaire
            {
                UserId = addAppointmentsDto.UserId,
                ConcernedParty = addAppointmentsDto.ConcernedParty,
                LinkQuestionnaire = addAppointmentsDto.LinkQuestionnaire,
                QuestionnaireStatus = addAppointmentsDto.QuestionnaireStatus,
                QuestionnaireName = addAppointmentsDto.QuestionnaireName,
                QuestionnaireType = addAppointmentsDto.QuestionnaireType,
            };
        }
    }
}

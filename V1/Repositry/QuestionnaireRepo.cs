using BuildDB_Team.entitys;
using DataBase.DBcon;
using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.Entity;
using DevetionStudetns.Error.SuccessfullyMsg;
using FinalProject.DTO.QuestionnaireDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.QuestionnaireMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using testDtoAndmapper.Entity;
using V1.DTO.DistributionDTO;
using V1.DTO.QuestionnaireDTO;

namespace FinalProject.Repositry
{
    public class QuestionnaireRepo : IQuestionnaire
    {
        private readonly DBC _context;
        public QuestionnaireRepo(DBC context)
        {
            _context = context;
        }   

        public GeneralMsgDto AddQuestionnaire (AddQuetionnaireDto addQuetionnaireDto)
        {
            if ( addQuetionnaireDto == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            else
            {
                var getUserId = _context.Users.Where(p=>p.UserId ==  addQuetionnaireDto.UserId).ToList().Count;
                var getDublecateData  = _context.Questionnaire.Where(p => p.UserId == addQuetionnaireDto.UserId && p.QuestionnaireName == addQuetionnaireDto.QuestionnaireName
                && p.ConcernedParty == addQuetionnaireDto.ConcernedParty && p.LinkQuestionnaire == addQuetionnaireDto.LinkQuestionnaire ).ToList().Count;
                if (getDublecateData != 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUBLECATE_DATA,
                                              "DUBLECATE_DATA",
                                              "DUBLECATE_DATA"
                                              );
                    return ErrorMsg;
                }
                else
                {
                    if (getUserId != 0)
                    {
                        _context.Questionnaire.Add(addQuetionnaireDto.AddQuestionnaire());
                        _context.SaveChanges();
                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                         SuccessfullyMsgs.WORKSHOP_REGISTERED_SUCCESSFULLY,
                                         "Add Successfully",
                                         "You Are Add New Questionnaire "
                                         );
                        return SuccessfullyMsg;
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.INVALID_DATA_FORMAT,
                                                  "Enter the Courect sympole of the Main Group",
                                                  "Error in data entry. The data entered is not included "
                                                  );
                        return ErrorMsg;
                    }
                }

            }
        }
        public GeneralMsgDto DeleteQuestionnair (int QuestionnairId)
        {
            if (QuestionnairId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                           "Enter the requird filled",
                           Convert.ToString(QuestionnairId)
                           );
                return ErrorMsg;
            }
            else
            {
                var deleteQuestionnairById = _context.Questionnaire.FirstOrDefault(p => p.QuestionnaireId == QuestionnairId);
                if (deleteQuestionnairById == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_DATA,
                           "Enter the requird filled",
                           "Ther is not any data to delete it "
                           );
                    return ErrorMsg;
                }
                else
                {
                    _context.Questionnaire.Remove(deleteQuestionnairById);
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                   "Successfully Delete",
                                                   "You are delete this Questionnair "
                                                   );
                    return ErrorMsg;
                }
            }
        }
        public GeneralMsgDto UpdateQuestionnair(UpdateQuetionnaireDto NewQuestionnairData, int QuestionnairId)
        {
            if (QuestionnairId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                           "Enter the requird filled",
                           Convert.ToString(QuestionnairId)
                           );
                return ErrorMsg;
            }
            else
            {
                var OldQuestionnair = _context.Questionnaire.FirstOrDefault(p => p.QuestionnaireId == QuestionnairId);
                if (OldQuestionnair == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.ERROR_INPUT_DATE,
                           "Enter the requird filled",
                           "Ther is not any data to Update Questionnair "
                           );
                    return ErrorMsg;
                }
                else
                {
                    OldQuestionnair.LinkQuestionnaire = NewQuestionnairData.LinkQuestionnaire;
                    OldQuestionnair.QuestionnaireStatus = NewQuestionnairData.QuestionnaireStatus;
                    OldQuestionnair.ConcernedParty = NewQuestionnairData.ConcernedParty;
                    OldQuestionnair.QuestionnaireType = NewQuestionnairData.QuestionnaireType;
                    OldQuestionnair.QuestionnaireName = NewQuestionnairData.QuestionnaireName;
                    _context.SaveChanges();

                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                                                   "Successfully Delete",
                                                   "You are Update this Questionnair "
                                                   );
                    return ErrorMsg;
                }
            }
        }
        public  List <ShowQuestionnaireQDto> ShowAllQuestionnaire(string QuestionnaireType)
        {
            string sql = @"	 
                         select 
                            q.QuestionnaireId,
                            q.QuestionnaireName,
                            
                            u.FullName,
                            u.Email,
                            q.ConcernedParty ,
                            q.LinkQuestionnaire,
                            q.QuestionnaireStatus,
                            q.DeliveryDate
                          from 
                            Questionnaire q 
                            join Users u on u.UserId = q.UserId
                            where  q.QuestionnaireType = @QuestionnaireType
                        "
            ;

            var result = _context.Database
                .SqlQueryRaw<ShowQuestionnaireQDto>(sql
                , new SqlParameter("QuestionnaireType", QuestionnaireType))
                .ToList(); 
            if (result == null)
                return null;
            return result;
        }
        public List<ShowQuestionnaireQDto> ShowQuestionnaireById(int QuestionnaireId)
        {
            string sql = @"	 
                         select 
                           q.QuestionnaireId,
                            q.QuestionnaireName,

                           u.FullName,
                           u.Email,
                           q.ConcernedParty ,
                           q.LinkQuestionnaire,
                           q.QuestionnaireStatus,
                           q.DeliveryDate
                         from 
                           Questionnaire q 
                           join Users u on u.UserId = q.UserId

                         where   
                            q.QuestionnaireId =@QuestionnaireId
                

                        "
            ;
            var result = _context.Database.SqlQueryRaw<ShowQuestionnaireQDto>(
                sql,
                new SqlParameter("QuestionnaireId", QuestionnaireId)
                ).ToList();
            if (result == null)
                return null;
            return result;
        }
        public List<ShowQuestionnaireQDto> ShowQuestionnaireByUserId(string UserId , string QuestionnaireType)
        {
            string sql = @"	 
                         
	                    
                        SELECT 
                          q.QuestionnaireId,
                          q.QuestionnaireName,
                          u.FullName,
                          u.Email,
                          q.ConcernedParty,
                          q.LinkQuestionnaire,
                          q.QuestionnaireStatus,
                          q.DeliveryDate
                        FROM 
                          Questionnaire q 
                          JOIN Users u ON u.UserId = q.UserId
                        WHERE 
                          (
                            (q.ConcernedParty = 7 AND (SELECT RoulName FROM Users WHERE UserId = @UserId) IN (7, 6)) 
                            OR 
                            (q.ConcernedParty <> 7 AND q.ConcernedParty = (SELECT RoulName FROM Users WHERE UserId = @UserId))
                            OR q.ConcernedParty = (Select RoulName From Users Where UserId = @UserId)
                            
                          )
                        or (
                            (q.ConcernedParty = 2 AND (SELECT RoulName FROM Users WHERE UserId = @UserId) IN (2, 10)) 
                            OR 
                            (q.ConcernedParty <> 2 AND q.ConcernedParty = (SELECT RoulName FROM Users WHERE UserId = @UserId))
                            OR q.ConcernedParty = (Select RoulName From Users Where UserId = @UserId)
                            
                          )
                        or (
                            (q.ConcernedParty = 3 AND (SELECT RoulName FROM Users WHERE UserId = @UserId) IN (3, 11)) 
                            OR 
                            (q.ConcernedParty <> 3 AND q.ConcernedParty = (SELECT RoulName FROM Users WHERE UserId = @UserId))
                            OR q.ConcernedParty = (Select RoulName From Users Where UserId = @UserId)
                            
                          )
                        or (
                            (q.ConcernedParty = 5 AND (SELECT RoulName FROM Users WHERE UserId = @UserId) IN (5, 9)) 
                            OR 
                            (q.ConcernedParty <> 5 AND q.ConcernedParty = (SELECT RoulName FROM Users WHERE UserId = @UserId))
                            OR q.ConcernedParty = (Select RoulName From Users Where UserId = @UserId)
                            
                          )
                        or (
                            (q.ConcernedParty = 4 AND (SELECT RoulName FROM Users WHERE UserId = @UserId) IN (4, 12)) 
                            OR 
                            (q.ConcernedParty <> 4 AND q.ConcernedParty = (SELECT RoulName FROM Users WHERE UserId = @UserId))
                            OR q.ConcernedParty = (Select RoulName From Users Where UserId = @UserId)
                            
                          )
                           and q.QuestionnaireStatus = 1
                            and q.QuestionnaireType = @QuestionnaireType

                        "
            ;
            var result = _context.Database.SqlQueryRaw<ShowQuestionnaireQDto>(
                sql,
                new SqlParameter("UserId", UserId),
                new SqlParameter("QuestionnaireType", QuestionnaireType)
                ).ToList();
            if (result == null)
                return null;
            return result;
        }



    }
}

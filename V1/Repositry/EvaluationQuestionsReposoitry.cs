using DataBase.DBcon;
using DevetionStudetns.entitys;
using DevetionStudetns.Error.SuccessfullyMsg;
using FinalProject.DTO.DoctorDto;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.EvalautionFormMapper;
using FinalProject.Mappers.EvaluationQuestionsMappers;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;
using V1.DTO.EvaluationQuestionsDTO;

namespace FinalProject.Repositry
{
    public class EvaluationQuestionsReposoitry : IEvaluationQuestionsRepo
    {
        readonly private DBC _db;
        public EvaluationQuestionsReposoitry(DBC db)
        {
            _db = db;
        }

        public async Task<GeneralMsgDto> AddEvaluationQuestionsAsync(AddEvaluationQuestionDTO evaluation)
        {
             try
            {
                
                    var newQuestion = evaluation.AddEvaluationQuestionMapper();
                    _db.EvaluationQuestions.Add(newQuestion);                    
                    await _db.SaveChangesAsync();


                    int newId = newQuestion.QuestionId;


                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                        SuccessfullyMsgs.ADDED_QUESTION_SUCCESSFULLY,
                                        Convert.ToString( newId),
                                        "تم إضافة السؤال بنجاح"
                                      );    
                    return SucMsg;
                
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        
        public async Task<GeneralMsgDto> DeleteEvaluationQuestionsAsync(int EvaluationId)
        {
            var id =await _db.EvaluationQuestions.FirstOrDefaultAsync(id => id.QuestionId == EvaluationId);
            try
            {
                if (id != null)
                {
                    _db.EvaluationQuestions.Remove(id);
                    await _db.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                       SuccessfullyMsgs.The_deletion_was_completed_successfully,
                                       "Success",
                                       "Success"
                                     );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_Question_You_Want_To_Delete_Does_Not_Exist,
                           "Enter the requird filled",
                          "Ther is not any data to Update it "
                          );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n ممكن ان يكون هذه الخطأ ناتج عن وجود السؤال الذي تريد حذفة في نموذج ما \n", ex);
            }
        }

        public async Task<List<GetEvaluationQuestionQDto>> GetEvaluationQuestionByFormId(int evaluationFormId)
        {
            var EvulationId =await _db.EvaluationForm.FirstOrDefaultAsync(d => d.EvaluationFormId == evaluationFormId);
            try
            {
                if (EvulationId != null)
                {
                    string sql = @"	 
                        SELECT
                             ef.EvaluationFormId,
                             ef.EvaluationFormType,
                             eq.QuestionId,
                             eq.QuestionText,
                             eq.QuestionType,
                              eq.QuestionMark
                            FROM EvaluationForm ef
                             JOIN EvaluationForm_EvaluationQuestions efq
                             ON ef.EvaluationFormId = efq.EvaluationFormId
                            JOIN EvaluationQuestions eq
                            ON efq.EvaluationQuestionId = eq.QuestionId
                            WHERE ef.EvaluationFormId = @p0
                        ";

                    var result = await _db.Database.SqlQueryRaw<GetEvaluationQuestionQDto>(sql, evaluationFormId).ToListAsync();
                    if (result == null)
                        return null;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<List<GetEvaluationQuestionDTO>> GetEvaluationQuestionsAsync()
        {
            List<EvaluationQuestion> EvaluationQuestion = _db.EvaluationQuestions.ToList();
            List<GetEvaluationQuestionDTO> evaluatioDto = new List<GetEvaluationQuestionDTO>();
            try
            {
                foreach (EvaluationQuestion evaluationQuestions in EvaluationQuestion)
                {
                    evaluatioDto.Add(evaluationQuestions.GetEvaluationQuestionsMapp());
                    await _db.SaveChangesAsync();
                }
                return evaluatioDto;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<GetEvaluationQuestionDTO> GetEvaluationQuestionsByIdAsync(int QuestionId)
        {
            var evaluationQuestionId=await _db.EvaluationQuestions.FirstOrDefaultAsync(id=>id.QuestionId == QuestionId);
            try
            {
                if (evaluationQuestionId != null)
                {
                    GetEvaluationQuestionDTO getEvaluationQuestionDto = evaluationQuestionId.GetEvaluationQuestionsMapp();
                    return getEvaluationQuestionDto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<GeneralMsgDto> UpdateEvaluationQuestionsAsync(int EvaluationID, AddEvaluationQuestionDTO EvaluationQuestionsData)
        {
            var id =await _db.EvaluationQuestions.FirstOrDefaultAsync(id => id.QuestionId == EvaluationID);
            var question=_db.EvaluationQuestions.FirstOrDefault(q=>q.QuestionText == EvaluationQuestionsData.QuestionText && q.QuestionType == EvaluationQuestionsData.QuestionType);
            try
            {
                if (id != null)
                {
                  
                        id.QuestionText = EvaluationQuestionsData.QuestionText;
                        id.QuestionType = EvaluationQuestionsData.QuestionType;
                        id.QuestionMark = EvaluationQuestionsData.QuestionMark; 
                        _db.EvaluationQuestions.Update(id);
                        await _db.SaveChangesAsync();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                            SuccessfullyMsgs.The_operation_was_completed_successfully,
                            "Success",
                            "Successf"
                          );
                        return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                      IErrorMsgs.The_Question_You_Are_Trying_To_Uupdate_Does_Not_Exist,
                         "Enter the requird filled",
                        "Ther is not any data to Update it "
                        );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<List<GetEvaluationQuestionQDto>> GetEvaluationQuestionByFormName(string formName)
        {
            var EvulationId = await _db.EvaluationForm.FirstOrDefaultAsync(d => d.EvaluationFormType == formName);
            try
            {
                if (EvulationId != null)
                {
                    string sql = @"	 
                        SELECT
                             ef.EvaluationFormId,
                             ef.EvaluationFormType,
                             eq.QuestionId,
                             eq.QuestionText,
                             eq.QuestionType,
                              eq.QuestionMark
                            FROM EvaluationForm ef
                             JOIN EvaluationForm_EvaluationQuestions efq
                             ON ef.EvaluationFormId = efq.EvaluationFormId
                            JOIN EvaluationQuestions eq
                            ON efq.EvaluationQuestionId = eq.QuestionId
                            WHERE ef.EvaluationFormType = {0}
                        ";

                    var result = await _db.Database.SqlQueryRaw<GetEvaluationQuestionQDto>(sql, formName).ToListAsync();
                    if (result == null)
                        return null;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.DoctorDto;
using DevetionStudetns.entitys;
using DevetionStudetns.Error.SuccessfullyMsg;
using FinalProject.DTO.EvaluationForm_EvaluationQuestionDTO;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.EvaluationForm_EvaluationQuestionMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositry
{
    public class EvaluationFormAndEvaluationQuestionRepositry : IEvaluationFormAndQuestionRepo
    {
        readonly private DBC _db;
        public EvaluationFormAndEvaluationQuestionRepositry(DBC db)
        {
            _db = db;
        }

        public async Task<List<GetEvaluationFormAndEvaluationQuestionDto>> GetEvaluationFormAndEvaluationQuestion()
        {
            try
            {
                List<EvaluationForm_EvaluationQuestion> GetEvaluation = await _db.EvaluationForm_EvaluationQuestions.ToListAsync();
                List<GetEvaluationFormAndEvaluationQuestionDto> getevaluationdto = new List<GetEvaluationFormAndEvaluationQuestionDto>();
                foreach (EvaluationForm_EvaluationQuestion evaluation in GetEvaluation)
                {
                    getevaluationdto.Add(evaluation.GetEvaluationFormAndQuestionMapp());
                    _db.SaveChanges();
                }
                return getevaluationdto;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
 
        public async Task<GeneralMsgDto> AddEvaluationFormAndEvaluationQuestion(AddEvaluationFormAndEvaluationQuestionDto evaluationFormDto)
        {
            var evaluationformid =await _db.EvaluationForm_EvaluationQuestions.FirstOrDefaultAsync(ef => ef.EvaluationFormId == evaluationFormDto.EvaluationFormId && ef.EvaluationQuestionId == evaluationFormDto.EvaluationQuestionId);
            var evaluationquestion = await _db.EvaluationForm_EvaluationQuestions.FirstOrDefaultAsync(eq => eq.EvaluationQuestionId == evaluationFormDto.EvaluationQuestionId && eq.EvaluationFormId == evaluationFormDto.EvaluationFormId);
            var form =await _db.EvaluationForm.FirstOrDefaultAsync(f => f.EvaluationFormId == evaluationFormDto.EvaluationFormId);
            var question =await _db.EvaluationQuestions.FirstOrDefaultAsync(q => q.QuestionId == evaluationFormDto.EvaluationQuestionId);

            try
            {
                if (form != null)
                {
                    if (question != null)
                    {
                        if (evaluationformid == null)
                        {
                            if (evaluationquestion == null)
                            {
                                _db.EvaluationForm_EvaluationQuestions.Add(evaluationFormDto.AddEvaluationFormAndQuestionMapp());
                                await _db.SaveChangesAsync();
                                GeneralMsgDto SucMsg = new GeneralMsgDto(
                                    SuccessfullyMsgs.Added_successfully,
                                      "تمت الإضافة بنجاح",
                                     "السؤال كان موجودًا في نموذج آخر ولكنه أضيف للنموذج الجديد.:تمت إضافة السؤال إلى النموذج الجديد."
                                  );
                                return SucMsg;
                            }
                            else
                            {
                                return new GeneralMsgDto(
                                         IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                                         "هذا السؤال موجود بالفعل في النموذج المحدد.",
                                         "لم يتم الإضافة لأنه مكرر في نفس النموذج."
                                     );
                            }
                        }
                        else
                        {
                            return new GeneralMsgDto(
                                    IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                    "هذا السؤال موجود بالفعل في النموذج المحدد.",
                                    "لم يتم الإضافة لأنه مكرر في نفس النموذج."
                                );
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                               "Enter the requird filled",
                              "Ther is not any data to Update it "
                              );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                           "Enter the requird filled",
                          "Ther is not any data to Update it "
                          );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<GeneralMsgDto> DeleteEvaluationFormAndEvaluationQuestion(int FormId, int QuestionId)
        {
            var evaluationId =await _db.EvaluationForm_EvaluationQuestions.FirstOrDefaultAsync(evaluation => evaluation.EvaluationFormId == FormId && evaluation.EvaluationQuestionId == QuestionId);
            var form = await _db.EvaluationForm.FirstOrDefaultAsync(f => f.EvaluationFormId == FormId);
            var question = await _db.EvaluationQuestions.FirstOrDefaultAsync(q => q.QuestionId == QuestionId);

            try
            {
                if (form != null)
                {
                    if (question != null)
                    {
                        if (evaluationId != null)
                        {
                            _db.EvaluationForm_EvaluationQuestions.Remove(evaluationId);
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
                            IErrorMsgs.The_Question_You_Want_Does_Not_Exist_In_The_Selected_Form,
                               "Enter the requird filled",
                              "Ther is not any data to Update it "
                              );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                                   "Enter the requird filled",
                                  "Ther is not any data to Update it "
                                  );
                            return ErrorMsg;
                        }
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                     IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                        "Enter the requird filled",
                       "Ther is not any data to Update it "
                       );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> UpdateEvaluationFormAndEvaluationQuestion(int EvaluationFormId,int EvaluationQuestionId, UpdateEvaluationFormAndEvaluationQuestionDto evaluationFormQuestionDto)
        {
            var evaluationId =await _db.EvaluationForm_EvaluationQuestions.FirstOrDefaultAsync(id => id.EvaluationFormId == EvaluationFormId || id.EvaluationQuestionId == EvaluationQuestionId);

            try
            {
                if (evaluationId != null)
                {
                    evaluationId.EvaluationQuestionId = evaluationFormQuestionDto.EvaluationQuestionId;
                    _db.EvaluationForm_EvaluationQuestions.Update(evaluationId);
                    await _db.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                        SuccessfullyMsgs.The_operation_was_completed_successfully,
                        "Success",
                        "Success"
                      );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                      IErrorMsgs.The_Form_You_Want_To_Edit_Does_Not_Exist,
                         "Enter the requird filled",
                        "Ther is not any data to Update it "

                        );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<GetEvaluationFormAndEvaluationQuestionDto> GetEvaluationFormAndEvaluationQuestionById(int FormId,int QuestionId)
        {
            var id=await _db.EvaluationForm_EvaluationQuestions.FirstOrDefaultAsync(id=>id.EvaluationFormId==FormId && id.EvaluationQuestionId== QuestionId);
            try
            {
                if (id != null)
                {
                    GetEvaluationFormAndEvaluationQuestionDto getEvaluationFormAndEvaluationQuestionDto = id.GetEvaluationFormAndQuestionMapp();
                    return getEvaluationFormAndEvaluationQuestionDto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
    }
}

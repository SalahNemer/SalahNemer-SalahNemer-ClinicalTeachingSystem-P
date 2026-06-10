using DataBase.DBcon;
using DevetionStudetns.entitys;
using DevetionStudetns.Error.SuccessfullyMsg;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.EvalautionFormMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositry
{
    public class EvaluationFormRepositry:IEvaluationFormRepo
    {
        readonly private DBC _db;
        public EvaluationFormRepositry(DBC db)
        {
            _db = db;
        }
        public async Task<GeneralMsgDto> AddEvaluationFormAsync(AddEvaluationFormDto evaluation)
        {
            var evaluationform = _db.EvaluationForm.FirstOrDefault(e => e.EvaluationFormType == evaluation.EvaluationFormType);
            try
            {
                if (evaluationform == null)
                {
                    _db.EvaluationForm.Add(evaluation.evaluationForm());
                    await _db.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                        SuccessfullyMsgs.Added_successfully,
                                        "Success",
                                        "Success"
                                      );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_Form_You_Want_To_Add_Already_Exists,
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

        public async Task<GeneralMsgDto> DeleteEvaluationFormAsync(int EvaluationFormId)
        {
            var id =await _db.EvaluationForm.FirstOrDefaultAsync(id => id.EvaluationFormId == EvaluationFormId);
            try
            {
                if (id != null)
                {
                    _db.EvaluationForm.Remove(id);
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
                        IErrorMsgs.The_Form_You_Want_To_Delete_Does_Not_Exist,
                           "Enter the requird filled",
                          "Ther is not any data to Update it "
                          );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       "مشكله في الحذف",
                                       "Enter the requird filled",
                                       "Ther is not any data to Update it "
                                     );
                return ErrorMsg;
            }

        }

        public async Task <GetEvaluationFormDto> GetEvaluationForByIdAsync(int EvaluationFormID)
        {
            var evaluationFormId=await _db.EvaluationForm.FirstOrDefaultAsync(id=>id.EvaluationFormId==EvaluationFormID);
            try
            {
                if (evaluationFormId != null)
                {
                    GetEvaluationFormDto GetEvaluationFormDto = evaluationFormId.GetEvaluationFormMapp();
                    return GetEvaluationFormDto;
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

        public async Task<List<GetEvaluationFormDto>> GetEvaluationFormAsync()
        {
            List<EvaluationForm> EvaluationForm=_db.EvaluationForm.ToList();
            List<GetEvaluationFormDto> evaluatioDto=new List<GetEvaluationFormDto> ();
            try
            {
                foreach (EvaluationForm evaluationForms in EvaluationForm)
                {
                    evaluatioDto.Add(evaluationForms.GetEvaluationFormMapp());
                    await _db.SaveChangesAsync();
                }
                return evaluatioDto;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<GeneralMsgDto> UpdateEvaluationFormAsync(int EvaluationID, AddEvaluationFormDto EvaluationFormsData)
        {
            var id =await _db.EvaluationForm.FirstOrDefaultAsync(id => id.EvaluationFormId == EvaluationID);
            var evalu =await _db.EvaluationForm.FirstOrDefaultAsync(e => e.EvaluationFormType == EvaluationFormsData.EvaluationFormType);
            try
            {
                if (id != null)
                {
                    if (evalu == null)
                    {
                        id.EvaluationFormType = EvaluationFormsData.EvaluationFormType;
                        _db.EvaluationForm.Update(id);
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
                           IErrorMsgs.The_Form_You_Want_To_Add_Already_Exists,
                              "Enter the requird filled",
                             "Ther is not any data to Update it "
                             );
                        return ErrorMsg;
                    }
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
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

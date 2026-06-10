using DataBase.DBcon;
using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1.DTO.EvaluationQuestionsDTO;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationQuestionsController : ControllerBase
    {
        readonly EvaluationQuestionsServes _evaluationQuestionsServes;
        readonly private DBC _db;
        public EvaluationQuestionsController(EvaluationQuestionsServes evaluationQuestionsServes, DBC db)
        {
            _evaluationQuestionsServes = evaluationQuestionsServes;
            _db = db;
        }

        [HttpGet("GetEvaluationQuestions")]
        public async Task<IActionResult> GetEvaluationQuestions()
        {
            try
            {
                var rseult = await _evaluationQuestionsServes.getEvaluationQuestions();
                return Ok(rseult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("AddEvaluationQuestion")]
        public async Task<IActionResult> AddEvaluationQuestions(AddEvaluationQuestionDTO evaluationQuestions)
        {
            try
            {
                var add = await _evaluationQuestionsServes.addEvaluationQuestionsAsync(evaluationQuestions);
                if ( add.ErrorMsg == "تم اضافة السؤال بنجاح")
                    return Ok(add);
                return BadRequest(add);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteEvaluationQuestion")]
        public async Task<IActionResult> DeleteEvaluationQuestions(int EvaluationQuestionID)
        {
            try
            {
                var delete = await _evaluationQuestionsServes.DeleteEvaluationQuestionsAsync(EvaluationQuestionID);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateEvaluationQuestion")]
        public async Task<IActionResult> UpdateEvaluationQuestion(int EvaluationQuestionID, AddEvaluationQuestionDTO EvaluationQuestionsData)
        {
            try
            {
                var update = await _evaluationQuestionsServes.UpdateEvaluationQuestionsAsync(EvaluationQuestionID, EvaluationQuestionsData);
                if ( update.ErrorMsg == "تمت العملية بنجاح")
                    return Ok(update);
                return BadRequest(update);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetEvaluationQuestionByFromId")]
        public async Task<IActionResult> getEvaluationQuestionByFormId(int evaluationId)
        {
            var FormID = _db.EvaluationForm.FirstOrDefault(id => id.EvaluationFormId == evaluationId);
            try
            {
                if (FormID != null)
                {
                    var GetByFromId = await _evaluationQuestionsServes.getEvaluationQuestionByFormId(evaluationId);
                    return Ok(GetByFromId);
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                  IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                  "Enter the requird filled",
                  "there is not any data"
                  );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetEvaluationQuestionById")]
        public async Task<IActionResult> GetEvaluationQuestionById(int QuestionId)
        {
            var QuestionUd = _db.EvaluationQuestions.FirstOrDefault(q => q.QuestionId == QuestionId);
            try
            {
                if (QuestionUd != null)
                {
                    var GetQuestionById = await _evaluationQuestionsServes.getEvaluationQuestionsByIdAsync(QuestionId);
                    return Ok(GetQuestionById);
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
               IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
               "Enter the requird filled",
               "there is not any data"
               );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getEvaluationQuestionByFormName/{formName}")]
        public async Task<IActionResult> getEvaluationQuestionByFormName(string formName)
        {
            var getByFormName = _db.EvaluationForm.FirstOrDefault(name => name.EvaluationFormType == formName);
            try
            {
                if (getByFormName != null)
                {
                    var resultByFormName = await _evaluationQuestionsServes.getEvaluationQuestionByFormName(formName);
                    return Ok(resultByFormName);
                }
                else
                {
                    var error = new GeneralMsgDto(
                    IErrorMsgs.INVALID_EVALUATION_FORM_NAME, "خطأ", "لا يوجد أي نموذج بالاسم المدخل, يرجى التأكد من اسم النموذج المدخل"
                    );
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

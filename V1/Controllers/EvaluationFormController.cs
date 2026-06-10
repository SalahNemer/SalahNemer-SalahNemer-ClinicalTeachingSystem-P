using DataBase.DBcon;
using DevetionStudetns.entitys;
using FinalProject.DTO.EvaluationFormDto;
using FinalProject.DTO.EvaluationQuestionsDTO;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationFormController : ControllerBase
    {
        readonly private EvaluationFormServes _evaluationFormServes;
        private readonly DBC _db;
        public EvaluationFormController(EvaluationFormServes evaluationFormServes,DBC db)
        {
            _evaluationFormServes = evaluationFormServes;
            _db = db;
        }

        [HttpGet("GetEvaluationForm")]
        public async Task<IActionResult> GetEvaluationForm()
        {
            try
            {
                var rseult = await _evaluationFormServes.getEvaluationForm();
                return Ok(rseult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetEvaluationFormById")]
        public async Task<IActionResult> GetEvaluationFormById(int evaluationFormId)
        {
            var id=_db.EvaluationForm.FirstOrDefault(e=>e.EvaluationFormId==evaluationFormId);
            try
            {
                if (id != null)
                {
                    var rseult = await _evaluationFormServes.getEvaluationForByIdAsync(evaluationFormId);
                    return Ok(rseult);
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

        [HttpPost("AddEvaluationQuestions")]
        public async Task<IActionResult> AddEvaluationForm(AddEvaluationFormDto evaluationQuestions)
        {
            try
            {
                var add = await _evaluationFormServes.addEvaluationFormAsync(evaluationQuestions);
                if ( add.ErrorMsg == "تمت الاضافة بنجاح")
                     return Ok(add);
                return BadRequest(add);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteEvaluationQuestion")]
        public async Task<IActionResult> DeleteEvaluationForm(int EvaluationFormID)
        {
            try
            {
                var delete = await _evaluationFormServes.DeleteEvaluationFormAsync(EvaluationFormID);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateEvaluationQuestion")]
        public async Task<IActionResult> UpdateEvaluationForm(int EvaluationFormID, AddEvaluationFormDto EvaluationQuestionsData)
        {
            try
            {
                var update = await _evaluationFormServes.UpdateEvaluationFormAsync(EvaluationFormID, EvaluationQuestionsData);
                if ( update.ErrorMsg == "تمت العملية بنجاح")
                     return Ok(update);
                return BadRequest(update);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

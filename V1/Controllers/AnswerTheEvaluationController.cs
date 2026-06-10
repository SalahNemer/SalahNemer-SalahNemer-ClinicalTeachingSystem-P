using DataBase.DBcon;
using FinalProject.DTO.AnswerTheEvaluationDTO;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.Service;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerTheEvaluationController : ControllerBase
    {
        readonly private AnswerTheEvaluationServes _answerTheEvaluationServes;
        private readonly DBC _db;
        public AnswerTheEvaluationController(AnswerTheEvaluationServes answerTheEvaluationServes,DBC db)
        {    
                _answerTheEvaluationServes = answerTheEvaluationServes;
                _db = db;       
        }

        [HttpGet("GetAnswerTheEvaliationQ")]
        public async Task<IActionResult> GetEvaluationForm()
        {
            try
            {
                var rseult = await _answerTheEvaluationServes.getAnswerTheEvaluaion();
                return Ok(rseult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getdoctorsbystudentid")]
        public async Task<IActionResult> ShowDataDoctorByStudentId(string studentid)
        {
            var result =await _answerTheEvaluationServes.ShowDataDoctorByStudentId(studentid);

            if (result == null || !result.Any())
            {
                return BadRequest(result);
            }
            return Ok(await _answerTheEvaluationServes.ShowDataDoctorByStudentId(studentid));
        }

        [HttpGet("GetAnswerTheEvaliationQByAnswerId")]
        public async Task<IActionResult> GetAnswerTheEvaliationQByAnswerId(int id)
        {
            var answerid = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(a => a.AnswerId == id);
            try
            {
                if (answerid != null)
                {
                    var rseult = await _answerTheEvaluationServes.getAnswerTheEvaluaionByAnswerId(id);
                    return Ok(rseult);
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_Requested_Item_Was_Not_Found,
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

        [HttpPost("AddAnswerTheEvaluation")]
        public async Task<IActionResult> AddAnswerTheEvaluation(AddAnswerTheEvaluationDto addAnswerTheEvaluationDto)
        {
            try
            {
                var result = await _answerTheEvaluationServes.addAnswerTheEvaluation(addAnswerTheEvaluationDto);
                if (result.ErrorMsg == "تمت الاضافة بنجاح") 
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpPost("AddAnswerTheEvaluationForTheDepartmentHead")]
        public async Task<IActionResult> AddAnswerTheEvaluationForTheDepartmentHead(AddAnswerTheEvaluationDto evaluationAnswerDto)
        {
            try
            {
                var result = await _answerTheEvaluationServes.AddAnswerTheEvaluationForTheDepartmentHead(evaluationAnswerDto);
                if (result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteAnswer")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            try
            {
                var result = await _answerTheEvaluationServes.deleteAnswerTheEvaluation(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("ShowAllDoctorInTheDepartmentByDepartmentHeadId/{DepartmentHeadId}")]
        public IActionResult ShowAllDoctorInTheDepartmentByDepartmentHeadId(string DepartmentHeadId)
        {
            try
            {
                var getResult = _answerTheEvaluationServes.ShowAllDoctorInTheDepartmentByDepartmentHeadId(DepartmentHeadId);
                if (getResult == null || !getResult.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                  IErrorMsgs.DOCTOR_ID_NOT_FOUND,
                                  "Not Found",
                                  "Not Found"
                                  );
                    return BadRequest(ErrorMsg);
                }
                return Ok(getResult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

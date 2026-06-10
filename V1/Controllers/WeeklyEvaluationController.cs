using DevetionStudetns.DTO.AttendanceDTO;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using testDtoAndmapper.Entity;
using V1.DTO.WeeklyEvaluationDTO;
using V1.Interface.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyEvaluationController : ControllerBase
    {
        private readonly IWeeklyEvaluationService _weeklyEvaluationService;
        public WeeklyEvaluationController(IWeeklyEvaluationService weeklyEvaluationService)
        {
            _weeklyEvaluationService = weeklyEvaluationService;
        }

        [HttpGet("GetAllWeeklyEvaluations")]
        public async Task<IActionResult> GetAllWeeklyEvaluations ()
        {
            try
            {
                return Ok(await _weeklyEvaluationService.GetAllWeeklyEvaluations());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GettWeeklyEvaluationById{id}")]
        public async Task<IActionResult> GettWeeklyEvaluationById(int id)
        {
            try
            {
                var eval = await _weeklyEvaluationService.GetWeeklyEvaluationById(id);
                if (eval == null)
                {
                    var errorMsg = new GeneralMsgDto(IErrorMsgs.WEEKLY_EVALUATION_NOT_FOUND, "يوجد خطأ", "معرف التقييم الأسبوعي غير صالح, الرجاء التأكد من المعرف المدخل");
                    return BadRequest(errorMsg);
                }
                return Ok(eval);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddWeeklyEvaluation")]
        public async Task<IActionResult> AddWeeklyEvaluation(AddWeeklyEvaluationDto dto)
        {
            try
            {
                var result = await _weeklyEvaluationService.AddWeeklyEvaluation(dto);
                if (result.ErrorMsg == "تم إضافة التقييم الأسبوعي بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateWeeklyEvaluation{id}")]
        public async Task<IActionResult> UpdateWeeklyEvaluation(int id,  UpdateWeeklyEvaluationDto dto)
        {
            try
            {
                var result = await _weeklyEvaluationService.UpdateWeeklyEvaluation(id, dto);
                if (result.ErrorMsg == "تم تحديث التقييم الأسبوعي بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeleteWeeklyEvaluation{id}")]
        public async Task<IActionResult> DeleteWeeklyEvaluation (int id )
        {
            try
            {
                return Ok(await _weeklyEvaluationService.DeleteWeeklyEvaluation(id));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetEvaluationMarkByStudentId{studentId}")]
        public async Task<IActionResult> GetEvaluationMarkByStudentId(string studentId)
        {
            try
            {
                var result = await _weeklyEvaluationService.GetEvaluationMarkByStudentId(studentId);
                if(result is GeneralMsgDto error)
                {
                    return BadRequest(error); 
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetSubGroupByDoctroId/{DoctorId}")]
        public async Task<IActionResult> GetSubGroupByDoctroId(string DoctorId)
        {
            try
            {
                var result = await _weeklyEvaluationService.GetSubGroupByDoctroId(DoctorId);
                if(result is GeneralMsgDto error)
                {
                    return BadRequest(error);
                }
                return Ok(result); 
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetSumOfEvaluationWeekByStudentsId/{StudentsId}")]
        public IActionResult GetSumOfEvaluationWeekByStudentsId(string StudentsId)
        {
            try
            {
                var getResult = _weeklyEvaluationService.GetSumOfEvaluationWeekByStudentsId(StudentsId);
                if (getResult == null || !getResult.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                            IErrorMsgs.THER_IS_NOT_ANY_EVALUATION_WEEKLY,
                                                            "There is not found any data  ",
                                                            "There is not found any data " 
                                                            );
                    return BadRequest(ErrorMsg);
                } 
                return Ok(getResult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

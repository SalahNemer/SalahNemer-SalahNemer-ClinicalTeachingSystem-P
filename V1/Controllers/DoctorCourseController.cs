using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1.DTO.DoctorCrouseDTO;
using V1.Service;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorCourseController : ControllerBase
    {
        private readonly DoctorCourseService _context;
        public DoctorCourseController (DoctorCourseService context)
        {
            _context = context;
        }

        [HttpPost("AddDcotorCourse")]
        public IActionResult AddDcotorCourse([FromBody]AddDoctorCourseDto addDoctorCourseDto)
        {
            try
            {
                var result = _context.AddDcotorCourse(addDoctorCourseDto);
                if ( result.ErrorMsg == "تمت اضافة مشرف على المادة بنجاح") 
                    return Ok(result);  
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllDoctorCourse")]
        public IActionResult GetAllDoctorCourse()
        {
            try
            {
                var result  = _context.GetAllDoctorCourse();
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.NOT_FOUND_ANY_COURE_AND_DOCTRO,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDoctorCourseByDoctorIdAndCourseId/{DoctorId}/{CourseId}")]
        public IActionResult GetDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId)
        {
            try
            {
                var result = _context.GetDoctorCourseByDoctorIdAndCourseId(DoctorId, CourseId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.NOT_FOUND_ANY_COURE_AND_DOCTRO,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDoctorCourseByCurrentAcademicYear/{CurrentAcademicYearName}")]
        public IActionResult GetDoctorCourseByCurrentAcademicYear(string CurrentAcademicYearName)
        {
            try
            {
                var result = _context.GetDoctorCourseByCurrentAcademicYear(CurrentAcademicYearName);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.NOT_FOUND_ANY_COURE_AND_DOCTRO,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteDoctorCourseByDoctorIdAndCourseId/{DoctorId}/{CourseId}")]
        public IActionResult DeleteDoctorCourseByDoctorIdAndCourseId(string DoctorId, int CourseId)
        {
            try
            {
                return Ok(_context.DeleteDoctorCourseByDoctorIdAndCourseId(DoctorId, CourseId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }


        [HttpGet("GetAllSupervisedDoctor")]
        public IActionResult GetAllSupervisedDoctor()
        {
            var result = _context.GetSupervisedDoctor();
            if (result == null || !result.Any())
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_ANY_COURE_AND_DOCTRO,
                               "Not Found",
                               "Not Found"
                               );
                return BadRequest(ErrorMsg);
            }
            return Ok(result);
        }
    }
}

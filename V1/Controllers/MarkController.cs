using BuildDB_Team.entitys;
using database.models;
using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using testDtoAndmapper.Entity;
using V1.DTO.MarkDTO;
using V1.Entity;
using V1.Service;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly MarkService _context;
        private readonly DBC context;
        public MarkController(MarkService con, DBC con1)
        {
            _context = con;
            context = con1;
        }

        [HttpPost("AddMarks")]
        public IActionResult AddMarks([FromBody] List<AddMarkDto> addMarkDto)
        {
            try
            {
                var result = _context.AddMarksService(addMarkDto);
                if ( result.ErrorMsg == "تمت اضافة العلامة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeleteMark/{MarkId}")]
        public IActionResult DeleteMark(int MarkId)
        {
            try
            {
                return Ok(_context.DeleteMarkService(MarkId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
                
            }
        }

        [HttpPut("UpdateMark/{MarkId}")]
        public IActionResult UpdateMark([FromBody] UpdateMarkDto NewMarkDot, int MarkId)
        {
            try
            {
                var result = _context.UpdateMarkService(NewMarkDot, MarkId);
                if ( result.ErrorMsg == "تم تحديث العلامة ينجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetMarkById/{markId}")]
        public IActionResult GetMarkById(int markId)
        {
            try
            {
                var result = _context.GetMarkByIdService(markId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.MARK_NOT_FOUND,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetMarkByStudentsId/{StudentId}")]
        public IActionResult GetMarkByStudentsId(string StudentId)
        {
            try
            {
                var result = _context.GetMarkByStudentsIdService(StudentId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.MARK_NOT_FOUND,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetMarkBySCorseIdAndAcadimicYear/{AcademicYearName}/{CouresId}")]
        public IActionResult GetMarkBySCorseIdAndAcadimicYear(string AcademicYearName, int CouresId)
        {
            try
            {
                var result = _context.GetMarkBySCorseIdAndAcadimicYearService(AcademicYearName, CouresId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.MARK_NOT_FOUND,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetStudentsInOneCourseLastTheRotationByDoctorId/{DoctorId}/{AcademicYear}/{MarkType}/{RotationId}")]
        public IActionResult GetStudentsInOneCourseLastTheRotationByDoctorId(string DoctorId, string AcademicYear,string MarkType,int RotationId)
        {
            try
            {
                var result = _context.GetStudentsInOneCourseLastTheRotationByDoctorIdSerive(DoctorId, AcademicYear , MarkType , RotationId);
                if (result == null || !result.Any())
                {            
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetSumOfTheWeeklyEvaluation/{StudentsId}")]
        public IActionResult GetSumOfTheWeeklyEvaluation(string StudentsId)
        {
            try
            {
                var result = _context.GetSumOfTheWeeklyEvaluation(StudentsId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.MARK_NOT_FOUND,
                                   "Not Found",
                                   "Not Found"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetStudentsForTheUpdate/{DoctorId}/{AcademicYear}/{RotationId}/{MarkType}")]
        public IActionResult GetStudentsForTheUpdate(string DoctorId, string AcademicYear , int RotationId, string MarkType)
        {
            try
            {
                var result = _context.GetStudentsForTheUpdate( DoctorId,  AcademicYear, RotationId, MarkType);
                if (result == null || !result.Any())
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetMarkForTheStudentByIdAndCourseId/{StudentsId}/{CourseId}")]
        public IActionResult GetMarkForTheStudentByIdAndCourseId(string StudentsId, int CourseId)
        {
            try
            {
                var result = _context.GetMarkForTheStudentByIdAndCourseId(StudentsId, CourseId);
                if (result == null || !result.Any())
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

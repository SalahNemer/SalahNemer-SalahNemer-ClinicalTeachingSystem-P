using DataBase.DBcon;
using DevetionStudetns.DTO.DivisionsDTO;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly DBC context;
        private readonly DivisionService _context;

        public DivisionController(DivisionService con, DBC con1)
        {
            context = con1;
            _context = con;
        }

        [HttpPost("AddDivision")]
        public IActionResult AddDivision([FromBody] DivisionAddDto AddDivision)
        {
            try
            {
                
                var result =_context.AddDivisionService(AddDivision);
                if (result.ErrorMsg == "تم تسجيل الطالب بنجاح")
                    return Ok(result);
                return BadRequest(result);  
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteDivision/{DivisionId}")]
        public IActionResult DeleteDivision(int DivisionId)
        {
            try
            {
                return Ok(_context.DeleteDivisionService(DivisionId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateDivision/{DivisionId}")]
        public IActionResult UpdateDivision([FromBody] DivisionAddDto NewData, int DivisionId)
        {
            try
            {
                var result = _context.UpdateDivisionService(NewData, DivisionId);
                if ( result.ErrorMsg == "تم تحديث التقسيم بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllStudentsInOneSubGroupBySubGroupId/{SubGroupId}")]
        public IActionResult GetAllStudentsInOneSubGroupBySubGroupId(int SubGroupId)
        {
            try
            {
                var validationGetDataBySubGroup = context.Divisions.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                if (validationGetDataBySubGroup == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.STUDENTS_NOT_FOUND,
                                                          "Failed ",
                                                          "There is not any Students in this sub Group "
                                                          );
                    return BadRequest(ErrorMsg);

                }
                return Ok(_context.GetAllStudentsInOneSubGroupBySubGroupIdService(SubGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllStudentsInOneMainByMainGroupId/{MainGroupId}")]
        public IActionResult GetAllStudentsInOneMainByMainGroupId(int MainGroupId)
        {
            try
            {
                var validationGetDataByMainGroup = context.Divisions.Where(p => p.SubGroup.MainGroupId == MainGroupId).ToList().Count;
                if (validationGetDataByMainGroup == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.STUDENTS_NOT_FOUND,
                                                          "Failed ",
                                                          "There is not any Students in this main Group "
                                                          );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.GetAllStudentsInOneMainByMainGroupIdService(MainGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllStudentsInTheSameLevel/{Level}")]
        public IActionResult GetAllStudentsInTheSameLevel(int Level)
        {
            try
            {
                var validationGetDataByLevel = context.Divisions.Where(p => p.SubGroup.MainGrop.AcademicYearId == Level).ToList().Count;
                if (Level != 4 && Level != 5 && Level != 6)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.FAILED_ENTER_LEVEL_EXITING_LIMITS,
                                         "Not Found",
                                         "There is not any data in this Level : " + Level
                                         );
                    return BadRequest(ErrorMsg);
                }
                if (validationGetDataByLevel == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.STUDENTS_NOT_FOUND,
                                                          "Failed ",
                                                          "There is not any Students in this level"
                                                          );
                    return BadRequest(ErrorMsg);

                }
                return Ok(_context.GetAllStudentsInTheSameLevelService(Level));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDataForStudentId/{StudentId}")]
        public IActionResult GetDataForStudentId(string StudentId)
        {
            try
            {
                var validationGetDataByStudentId = context.Divisions.Where(p => p.StudentId == StudentId).ToList().Count;
                if (validationGetDataByStudentId == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.STUDENTS_NOT_FOUND,
                                                          "Failed ",
                                                          "There is not any Students have this id : " + StudentId
                                                          );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.GetDataForStudentIdService(StudentId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

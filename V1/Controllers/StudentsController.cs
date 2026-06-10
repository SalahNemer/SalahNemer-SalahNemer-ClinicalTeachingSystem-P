using DataBase.DBcon;
using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.DTO.UserDTO;
using DevetionStudetns.Mappers.StudentsMapper;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using FinalProject.DTO.StudentsDTO;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using testDtoAndmapper.Entity;
using V1.DTO.StudentsDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService _context;
        private readonly DBC context;
        public StudentsController(StudentsService context_, DBC con)
        {
            _context = context_;
            context = con;
        }

        [HttpGet("GetStudentsDataById/{StudentsId}")]
        public IActionResult getStudentsData(string StudentsId)
        {
            try
            {
                var GetAllStudents = context.students.FirstOrDefault(p => p.UserId == StudentsId);
                if (GetAllStudents != null)
                {
                    return Ok(_context.GetStudentsById(StudentsId));
                }
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                    IErrorMsgs.STUDENTS_NOT_FOUND,
                    "There is not any Students have this Students Id ",
                    "There is not any students have this id "
                    );
                return BadRequest(ErrorMsg);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetAllStudentsInOneSubGroupById/{SubGroupId}")]
        public IActionResult GetAllStudentsInOneSubGroupById(int SubGroupId)
        {
            try
            {
                var GetAllStudents = context.Divisions.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                if (GetAllStudents == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                    IErrorMsgs.STUDENTS_NOT_FOUND,
                    "Enter the Courect number for the  SubGroup",
                    "There is not any studnets in this subGroup "
                    );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.GetAllStudentsInOneSubGroupByIdSerive(SubGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetAllStudentsOnSameLevel/{Level}")]
        public IActionResult GetAllStudentsOnSameLevel(int Level)
        {
            var getResult = _context.GetAllStudentsInSameLevelService(Level);
            if (getResult == null || !getResult.Any())
                return BadRequest(getResult);
            return Ok(getResult);
            
        }

        [HttpGet("GetAllStudentsInSameLevelInAllSubGroup/{Level}")]
        public IActionResult GetAllStudentsInSameLevelInAllSubGroup(int Level)
        {
            var getResult = _context.GetAllStudentsInSameLevelInAllSubGroupService(Level);
            if (getResult == null || !getResult.Any())
                return BadRequest(getResult);
            return Ok(getResult);
            
        }

        [HttpPost("AddStudentds")]
        public IActionResult AddStudentds([FromBody] StudentsAddDto studentsAddDto)
        {
            try
            {
                var result = _context.AddStudentdsService(studentsAddDto);
                if ( result.ErrorMsg == "تم تسجيل الطالب بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeleteStudents/{StudentsId}")]
        public IActionResult DeleteStudents(string StudentsId)
        {
            try
            {
                return Ok(_context.DeleteStudentsService(StudentsId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var GetAllStudents = _context.GetAllStudentsService();
                if (GetAllStudents == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                    IErrorMsgs.STUDENTS_NOT_FOUND,
                    "There is not any data in the studnet table ",
                    "There is Not any Students in the Students table"
                    );
                    return BadRequest(ErrorMsg);
                }
                return Ok(GetAllStudents);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateStudentsData/{StudentId}")]
        public IActionResult UpdateStudnetsData([FromBody] UpdateStudentsDto NewStudentsData, string StudentId)
        {
            try
            {
                 var result = _context.UpdateStudentsService(NewStudentsData, StudentId);
                if (result.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateStudentAndUser/{StudentId}")]
        public async Task<IActionResult> UpdateStudentAndUser([FromBody] UpdateStudentAndUserDto student, string StudentId)
        {
            try
            {
                var result = await _context.UpdateStudentAndUserServes(student, StudentId);
                if (result.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

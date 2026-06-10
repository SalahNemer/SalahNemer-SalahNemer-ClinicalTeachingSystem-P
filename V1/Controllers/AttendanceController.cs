using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.Interface;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1.DTO.AttendanceDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("GetAllAttendances")]
        public async Task<IActionResult> GetAllAttendances ()
        {
            try
            {
                return Ok(await _attendanceService.GetAllAttendance());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAttendanceById")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceById(id);
                if (attendance == null)
                {
                    var errorMsg = new GeneralMsgDto(
                        IErrorMsgs.ATTENDANCE_NOT_FOUND_IN_DB, "حدث خطأ", "لا يوجد أي سجل حضور بالمعرف الذي أدخلته, يرجى التأكد من المعرف المدخل");
                    return BadRequest(errorMsg);
                }
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("AddMultipleAttendances")]
        public async Task<IActionResult> AddAttendance([FromBody] List<AddAttententsDto> createAttendanceDTOs)
        {
            try
            {
                var result = await _attendanceService.AddAttendance(createAttendanceDTOs);
                if (result.ErrorMsg == "تم تسجيل الحضور")
                    return Ok(result);
                return BadRequest(result);  
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateAttendance/{attendanceId}")]
        public async Task<IActionResult> UpdateAttendance(int attendanceId, AttendanceUpdateDTO attendanceUpdateDTO)
        {
            try
            {
                var result = await _attendanceService.UpdateAttendance(attendanceId, attendanceUpdateDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }        
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteAttendance")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            try
            {
                return Ok(await _attendanceService.DeleteAttendance(id));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllAttendanceByStudentId{studentId}")]
        public async Task<IActionResult> GetAllAttendanceByStudentId (string studentId)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceByStudentId(studentId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAttendanceByDoctorId{doctorId}")]
        public async Task<IActionResult> GetAttendanceByDoctorId (string doctorId)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceByDoctorId(doctorId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllAttendanceByCourseId{courseId}")]
        public async Task<IActionResult> GetAllAttendanceByCourseId (int courseId)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceByCourseId(courseId)); 
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetSubGroupForDoctorId{doctorId}")]
        public async Task<IActionResult> GetSubGroupForDoctorId (string doctorId)
        {
            try
            {
                return Ok(await _attendanceService.GetSubGroupForDoctorId(doctorId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAttendanceByDateRange{startDate}/{endDate}")]
        public async Task<IActionResult> GetAttendanceByDateRange(DateOnly startDate,DateOnly endDate)
        {
            try
            {
                var result = await _attendanceService.GetAttendanceByDateRange(startDate, endDate);
                if (result is GeneralMsgDto error)
                {
                    return BadRequest(error);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAttendanceByDateAndDoctorId/{date}/{doctorId}")]
        public async Task<IActionResult> GetAttendaceByDateAndDoctorId([FromRoute] DateOnly date, [FromRoute] string doctorId)
        {
            try
            {
                var result = await _attendanceService.GetAttendanceByDateAndDoctorId(date, doctorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using V1.DTO.AppointmentDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppotmentsController : ControllerBase
    {
        private readonly DBC context;
        private readonly AppotmentsServices _context;
        public AppotmentsController(AppotmentsServices con, DBC con1)
        {
            _context = con;
            context = con1;
        }

        [HttpPost("AddAppotments")]
        public IActionResult AddAppotments([FromBody] AddAppointmentsDto addAppointmentsDto)
        {
            try
            {
                var AddAppotmentsDetails = _context.AddAppotmentsService(addAppointmentsDto);
                if (AddAppotmentsDetails.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(AddAppotmentsDetails);
                return BadRequest(AddAppotmentsDetails);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getAppointmentsInTheOneSubGroup/{subGroupId}/{RotationId}")]
        public IActionResult GetAppointmentsInTheOneSubGroup(int subGroupId, int RotationId)
        {
            try
            {
                var getDataInAppointmentsBySubGroupId = context.Distributions.Where(p => p.SubGroup.SubGroupId == subGroupId && p.RotationId == RotationId).ToList().Count;
                var getData = _context.getAppointmentsInTheOneSubGroupService(subGroupId, RotationId);
                if (getDataInAppointmentsBySubGroupId == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   IErrorMsgs.SUBGROUP_NOT_FOUND,
                                                   "There is not any data  ",
                                                   "There is Not any Appointments for this subgroup "
                                                   );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    return Ok(getData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getAppointmentsInTheOneMainGroup/{MainGroupId}/{RotationId}")]
        public IActionResult GetAppointmentsInTheOneMainGroup(int MainGroupId, int RotationId)
        {
            try
            {
                var getDataInAppointmentsByMainGroupId = context.Distributions.Where(p => p.SubGroup.MainGroupId == MainGroupId && p.RotationId == RotationId).ToList().Count;
                var getData = _context.getAppointmentsInTheOneMainGroupService(MainGroupId, RotationId);
                if (getDataInAppointmentsByMainGroupId == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.MAINGROUP_NOT_FOUND,
                                                  "There is not any data  ",
                                                  "There is Not any Appointments for this mainGroup "
                                                  );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    return Ok(getData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteAppointments/{AppointmentsId}")]
        public IActionResult DeleteAppointments(int AppointmentsId)
        {
            try
            {
            return Ok(_context.DeleteAppointmentsService(AppointmentsId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateAppointment")]
        public IActionResult UpdateAppointment([FromBody] AddAppointmentsDto NewAppointmentData, int AppointmentId)
        {
            try
            {
                var UpdateAppointments = _context.UpdateAppointmentService(NewAppointmentData, AppointmentId);
                if (UpdateAppointments.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(UpdateAppointments);
                return BadRequest(UpdateAppointments);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAppointmentsByRotationId/{rotationId}")]
        public IActionResult GetAppointmentsByRotationIdService(int rotationId)
        {
            try
            {
                var getAppotments = _context.GetAppointmentsByRotationIdService(rotationId);
                var ifNull = context.Appointment.Where(p => p.RotationId == rotationId).ToList().Count;
                if (ifNull == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_APPOTMENT,
                                                  "There is not any data  ",
                                                  "There is Not any Appointments for this mainGroup "
                                                  );
                    return BadRequest(ErrorMsg);


                }
                return Ok(getAppotments);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAppointmentsByAppointmentId/{AppointmentId}")]
        public IActionResult GetAppointmentsByAppointmentId(int AppointmentId)
        {
            try
            {
                var getAppotments = _context.GetAppointmentsByAppointmentIdService(AppointmentId);
                var ifNull = context.Appointment.Where(p => p.AppointmentId == AppointmentId).ToList().Count;
                if (ifNull == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_APPOTMENT,
                                                  "There is not any data  ",
                                                  "There is Not any Appointments for this mainGroup "
                                                  );
                    return BadRequest(ErrorMsg);


                }
                return Ok(getAppotments);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllAppointmentsByAppointment")]
        public IActionResult GetAllAppointmentsByAppointment()
        {
            try
            {
                var getAppotments = _context.GetAllAppointmentsByAppointmentService();
                var ifNull = context.Appointment.ToList().Count;
                if (ifNull == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_APPOTMENT,
                                                  "There is not any data  ",
                                                  "There is Not any Appointments for this mainGroup "
                                                  );
                    return BadRequest(ErrorMsg);


                }
                return Ok(getAppotments);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

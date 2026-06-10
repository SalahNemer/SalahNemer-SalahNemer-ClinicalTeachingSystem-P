using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.DistributionsDTO;
using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.Entity;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.DistributionDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistriputionController : ControllerBase
    {

        private readonly DistriputionService _context;
        private readonly DBC context;

        public DistriputionController(DistriputionService con, DBC con1)
        {
            _context = con;
            context = con1;
        }

        [HttpPut("UpdateDistrbution/{DistrbutionId}")]
        public IActionResult UpdateDistrbution(AddDistibutionsDto NewDistrbutionData, int DistrbutionId)
        {
            try
            {
                var updateDistrbution = _context.UpdateDistrbutionService(NewDistrbutionData, DistrbutionId);
                if (updateDistrbution.ErrorMsg == "تم تحديث التقسيم بنجاح")
                    return Ok(updateDistrbution);
                return BadRequest(updateDistrbution);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("AddDistribution")]
        public IActionResult AddDistribution(AddDistibutionsDto addDistibutionsDto)
        {
            try
            {
                var addDistrbution = _context.AddDistributionService(addDistibutionsDto);
                if (addDistrbution.ErrorMsg == "تم علمية اضافة التقسيم بنجاح حيث ان الطبيب الذي تم اختيارة يدرس هذه المجموعة الفرعية  اكثر من  مرة خلال الفترة الدراسية الحالية" ||
                    addDistrbution.ErrorMsg == "تم عملية التوزيع بنجاح")
                    return Ok(addDistrbution);
                return BadRequest(addDistrbution);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteDistubution/{DistrbutionId}")]
        public IActionResult DeleteDistubution(int DistrbutionId)
        {
            try
            {
                var deleteDistrbution = _context.DeleteDistubutionService(DistrbutionId);
                if (deleteDistrbution == null)
                {
                    return BadRequest(deleteDistrbution);
                }
                else
                {
                    return Ok(deleteDistrbution);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getAllDistbutionByMainGroupIdAndRotationIdAndRotationId/{SubGroupId}/{RotationId}")]
        public IActionResult getAllDistbutionBySubGroupIdAndRotationId(int SubGroupId, int RotationId)
        {
            try
            {
                string sql = @"	 
                                select
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
                            	    r.RotationName,
                                    x.UserId ,
                                    x.FullName,
                                    x.Email,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId
                                    where p.SubGroupId = @SubGroupId
	                                and r.RotationId = @RotationId
                            ";
                var result = context.Database.SqlQueryRaw<GetDistbutionsQDto>(
                    sql, new SqlParameter("SubGroupId", SubGroupId)
                    , new SqlParameter("RotationId", RotationId)).ToList().Count;
                if (result == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.NOT_FOUND_DISTBUTION,
                                                      "There is not any data  ",
                                                      "There is Not any Distpution "
                                                      );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.getAllDistbutionBySubGroupIdService(SubGroupId, RotationId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }


        [HttpGet("getAllDistbutionByMainGroupIdAndRotationId/{mainGroupId}/{RotationId}")]
        public IActionResult getAllDistbutionByMainGroupIdAndRotationId(int mainGroupId, int RotationId)
        {
            try
            {
                var result = _context.getAllDistbutionByMainGroupIdService(mainGroupId, RotationId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.NOT_FOUND_DISTBUTION,
                                                      "There is not any data  ",
                                                      "There is Not any Distpution "
                                                      );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.getAllDistbutionByMainGroupIdService(mainGroupId,RotationId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getAllDistbutionToTheDoctorByDoctorId/{DoctorId}")]
        public IActionResult getAllDistbutionToTheDoctorByDoctorIdAndRotationId(string DoctorId)
        {
            try
            {
                string sql = @"	 
                                select
                                    d.DistributionId ,
                                    m.MainGroupSympole,
                                    p.SubGroupSympole,
                                    h.HospitalName,
                                    t.DepartmentName ,
	                                r.RotationName,
                                    c.CourseName,
                                    c.CourseCode,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    a.SessionStartTime,
                                    a.SessionEndTime 
                                    from 
                                    Distributions d 
                                    join Appointments a on d.AppointmentId = a.AppointmentId
                                    left join Doctors s on s.UserId = d.DoctorId 
                                    left join Hospitals h on h.HospitalId = s.HospitalId 
                                    left join Department t on t.DepartmentId = s.DepartmentId 
                                    left join Course c on d.CourseId = c.CouresId 
                                    left join Rotations r on r.RotationId = d.RotationId
                                    left join SubGroup p on p.SubGroupId = d.SubGroupId 
                                    left join MainGroup m on p.MainGroupId = m.MainGroupId
                                    left join Users x on x.UserId = s.UserId
	                                where s.UserId =@DoctorId
                                    And a.StartSessionDate >= DATEADD(DAY, -10, GETDATE()) 
                                    AND a.EndSessionDate >= GETDATE()	                            
                                    and d.DistributionStatus = 3
                            ";

                var result = context.Database.SqlQueryRaw<GetDistbutionsQ1Dto>(
                    sql,
                    new SqlParameter("DoctorId", DoctorId)
                    ).ToList().Count;

                if (result == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.NOT_FOUND_DISTBUTION,
                                                      "There is not any data  ",
                                                      "There is Not any Distpution "
                                                      );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.getAllDistbutionToTheDoctorByDoctorIdAndRotationIdService(DoctorId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDistbutionForTheStudentsByStudentsId/{StudentsId}")]
        public IActionResult GetDistbutionForTheStudentsByStudentsId(string StudentsId)
        {
            try
            {
                var result = _context.getDistbutionForTheStudentsByStudentsIdService(StudentsId);
                    if (result == null || !result.Any())
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.NOT_FOUND_DISTBUTION,
                                                          "Failed ",
                                                          "There is not any Distribution Main Group have the same level and rotationId "
                                                          );
                        return BadRequest(ErrorMsg);
                    }
                    else
                    {
                        return Ok(result);
                    }   
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("getAllDistbutionId/{DistributionId}")]
        public IActionResult getAllDistbutionId(int DistributionId)
        {
            try
            {
                var result = _context.getAllDistbutionIdService(DistributionId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                      IErrorMsgs.NOT_FOUND_DISTBUTION,
                                                      "Failed ",
                                                      "There is not any Distribution Main Group have the same level and rotationId "
                                                      );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    return Ok(_context.getAllDistbutionIdService(DistributionId));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDoctorByRotationIdMainGroupAndAcadmicYear/{MainGroup}/{RotationId}/{AcadmicYear}")]
        public IActionResult GetDoctorByRotationIdMainGroupAndAcadmicYear(int MainGroup, int RotationId, string AcadmicYear)
        {
            try
            {
                var result =_context.GetDoctorByRotationIdMainGroupAndAcadmicYear(MainGroup, RotationId, AcadmicYear);
                if (result == null || !result.Any()) {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetCourseByDctorIdAndAcadmicYear/{DoctorId}/{AcadmicYear}")]
        public IActionResult GetCourseByDctorIdAndAcadmicYear(string DoctorId, string AcadmicYear)
        {
            try
            {
                var result = _context.GetCourseByDctorIdAndAcadmicYear(DoctorId, AcadmicYear);
                if (result == null || !result.Any())
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}

using DataBase.DBcon;
using DevetionStudetns.DTO.DoctorDto;
using DevetionStudetns.Service;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V1.DTO.DoctorDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorServes _doctorServes;
        readonly private DBC _db;
        public DoctorController(DoctorServes doctorServes,DBC db)
        {
            _doctorServes = doctorServes;
            _db = db;
        }

        [HttpGet("Get Data Doctors")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _doctorServes.GetDoctors());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetByDepartmentId")]
        public async Task<IActionResult> GetByDepartement(int id)
        {
            var DepartementId =await _db.doctors.FirstOrDefaultAsync(d => d.DepartmentID == id);
            var Departement=await _db.Departments.FirstOrDefaultAsync(dep=>dep.DepartmentId == id);
            try
            {
                if (Departement != null)
                {
                    if (DepartementId != null)
                    {
                        return Ok(await _doctorServes.GetDoctorsByDepartement(id));
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                          IErrorMsgs.The_Department_Does_Not_Contain_Any_Doctors,
                          "Enter the requird filled",
                          "Ther is not any data to Update it "
                        );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                       IErrorMsgs.The_section_does_not_exist,
                       "Enter the requird filled",
                       "Ther is not any data to Update it "
                     );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("Get All Doctors Data")]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                return Ok(await _doctorServes.GetAllDatatDoctors());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("Add Doctor")]
        public async Task<IActionResult> Add(DoctorDto doctorDto)
        {
            try
            {
                var result = await _doctorServes.AddDoctors(doctorDto);
                if ( result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("Delete Doctor")]
        public async Task<IActionResult> Delete(string DoctorId)
        {
            try
            {
                return Ok(await _doctorServes.DeleteDoctors(DoctorId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("Update Doctor")]
        public async Task<IActionResult> Update(string DoctorId, UpdateDoctorDto doctorDto)
        {
            try
            {
                var result = await _doctorServes.UpdateDoctors(DoctorId, doctorDto);
                if ( result.ErrorMsg == "تمت العملية بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDoctorById/{DcotroId}")]
        public IActionResult GetDoctorById(string DcotroId)
        {
            try
            {
                var result = _doctorServes.GetDoctorById(DcotroId);
                    if ( result == null || !result.Any())
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateDoctorAndUser/{DoctorId}")]
        public async Task<IActionResult> UpdateDoctorAndUser([FromBody] UpdateDoctorAndUserDto Doctor, string DoctorId)
        {
            try
            {
                var result = await _doctorServes.UpdateDoctorAndUserServes(Doctor, DoctorId);
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

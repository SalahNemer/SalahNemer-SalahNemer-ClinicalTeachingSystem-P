using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.entitys;
using DevetionStudetns.Interface;
using DevetionStudetns.Service;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalCountroller : ControllerBase
    {
        private readonly HospitalServes _hospital;
        private readonly DBC _context;

        public HospitalCountroller(HospitalServes hospital,DBC context)
        {
            _hospital = hospital;
            _context = context;
        }

        [HttpGet("Get All Hospital")]
        public async Task<IActionResult> Get()
        {
            var Hospital=await _context.hospitals.ToListAsync();
            try
            {
                if (Hospital != null)
                {
                    return Ok(await _hospital.getHospital());
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.There_Are_No_Hospitals,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("Add Hospital")]
        public async Task<IActionResult> add(HospitalDto hospital)
        {
            try
            {
                var result = await _hospital.addHospital(hospital);
                if (result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest (result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("Update Hospital")]
        public async Task<IActionResult> Update(int HospitalId, [FromBody] HospitalDto hospitalDto)
        {
            try
            {
                var result = await _hospital.UpdateHospital(HospitalId, hospitalDto);
                if (result.ErrorMsg == "تمت العملية بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpDelete("Delete Hospital")]
        public async Task<IActionResult> Delete(int HospitalId)
        {
            try
            {
                return Ok(await _hospital.deleteHospital(HospitalId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetHospitalById/{hospitalId}")]
        public async Task<IActionResult> GetHospitalById(int hospitalId)
        {
            return Ok(await _hospital.GetHospitalById(hospitalId));
        }

    }
}

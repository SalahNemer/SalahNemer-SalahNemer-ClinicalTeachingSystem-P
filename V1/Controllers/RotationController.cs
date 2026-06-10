using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.RotationsDTO;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotationController : ControllerBase
    {
        private readonly DBC context;
        private readonly RotationsService _context;
        public RotationController(RotationsService context1, DBC con)
        {
            context = con;
            _context = context1;
        }

        [HttpPost("AddRotations")]
        public IActionResult AddRotations([FromBody] AddRotaionsDto addRotaionsDto)
        {
            try
            {
                var result = _context.AddRotationsService(addRotaionsDto);
                if (result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest(result);  
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        [HttpDelete("DeleteRotations/{RotationsId}")]
        public IActionResult DeleteRotations(int RotationsId)
        {
            try
            {
                return Ok(_context.DeleteRotationService(RotationsId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateRotations/{RotationsId}")]
        public IActionResult UpdateRotations([FromBody] AddRotaionsDto NewRotaionsData, int RotationsId)
        {
            try
            {
                var result = _context.UpdateRotationsService(NewRotaionsData, RotationsId);
                if (result.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("ShowDateRotaionInOneYear/{Academic_Year}")]
        public IActionResult ShowDateRotaionInOneYear(string Academic_Year)
        {
            try
            {
                return Ok(_context.ShowDateRotaionInOneYearService(Academic_Year));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("ShowDateRotaionByRotationId/{RotationId}")]
        public IActionResult ShowDateRotaionByRotationId(int RotationId)
        {
            try
            {
                var GetRotationById = context.Rotations.Where(p => p.RotationId == RotationId).ToList().Count;
                if (GetRotationById == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.ROTATION_NOT_FOUND,
                                   "Fialed",
                                   "there is not any data"
                                   );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.ShowDateRotaionByRotationIdService(RotationId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}



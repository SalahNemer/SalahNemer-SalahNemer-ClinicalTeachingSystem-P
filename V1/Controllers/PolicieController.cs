using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1.DTO.PolicieDTO;
using V1.Entity;
using V1.Service;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicieController : ControllerBase
    {
        private readonly PolicieService _context;
        public PolicieController ( PolicieService context)
        {
            _context = context;
        }

        [HttpPost("AddPolicie")]
        public IActionResult AddPolicie([FromBody]AddPolicieDto AddPolicie)
        {
            try
            {
                var result = _context.AddPolicieService(AddPolicie);
                if (result.ErrorMsg == "تم ادراج السياسة بنجاح")
                    return Ok(result);
                return BadRequest(result);  
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdatePolicie/{PolicieId}")]
        public IActionResult UpdatePolicie([FromBody]UpdatePolicieDto newDataPolicieDto, int PolicieId)
        {
            try
            {
                var result = _context.UpdatePolicieService(newDataPolicieDto, PolicieId);
                if ( result.ErrorMsg == "تم تحديث  السياسة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeletePolicie/{policieId}")]
        public IActionResult DeletePolicie(int policieId)
        {
            try
            {
                return Ok(_context.DeletePolicieService(policieId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetAllPolicie")]
        public IActionResult GetAllPolicie()
        {
            try
            {
                var result = _context.GetAllPolicieService();
                if (result == null || !result.Any()) 
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   IErrorMsgs.NOT_FOUBD_POLICIE,
                                                   "There is not any POLICIE ",
                                                   "There is not any POLICIE"
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

        [HttpGet("GetAllPolicieById/{PolicieId}")]
        public IActionResult GetAllPolicieById(int PolicieId)
        {
            try
            {
                var result = _context.GetAllPolicieByIdService(PolicieId);
                if (result == null || !result.Any())
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                   IErrorMsgs.NOT_FOUBD_POLICIE,
                                   "There is not any POLICIE ",
                                   "There is not any POLICIE"
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

    }
}

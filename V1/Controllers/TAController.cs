using DevetionStudetns.DTO.TADTO;
using DevetionStudetns.Interface;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1.DTO.TADTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TAController : ControllerBase
    {
        private readonly ITAService _tAService;
        public TAController(ITAService tAService)
        {
            _tAService = tAService;
        }

        [HttpGet("GetAllTAs")]
        public async Task<IActionResult> GetAllTAs()
        {
            try
            {
              return Ok(await _tAService.GetAllTAs());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetTAById{id}")]
        public async Task<IActionResult> GetTAById(string id)
        {
            try
            {
                var ta = await _tAService.GetTAById(id);
                if (ta == null)
                {
                    GeneralMsgDto errorMsg = new GeneralMsgDto(
                       IErrorMsgs.TA_NOT_FOUND, "خطأ أثناء العرض", "لا يوجد أي قسم بالمعرف المدخل, يرجى التأكد من المعرف المدخل"
                       );
                    return BadRequest(errorMsg);
                }
                return Ok(ta);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("Add_TA")]
        public async Task<IActionResult> Add_TA(AddTADto tADTO)
        {
            try
            {
                var result = await _tAService.AddTA(tADTO);
                if (result.ErrorMsg == "تم إضافة البيانات بنجاح. يمكنك الآن متابعة الإجراءات التالية")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("Update_TA{id}")]
        public async Task<IActionResult> Update_TA(string id, UpdateTaDto tADTO)
        {
            try
            {
                var result = await _tAService.UpdateTA(id, tADTO);
                if (result.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("Delete_TA{id}")]
        public async Task<IActionResult> Delete_TA(string id)
        {
            try
            {
                 return Ok(await _tAService.DeleteTA(id));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateTaAndUser/{TaId}")]
        public async Task<IActionResult> UpdateTaAndUser([FromBody] UpdateTaAndUserDto ta, string TaId)
        {
            try
            {
                var result = await _tAService.UpdateTaAndUser(ta, TaId);
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

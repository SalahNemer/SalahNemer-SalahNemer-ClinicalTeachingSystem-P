using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using loginpage.DBcon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AcademicYearControll : ControllerBase
    {
        private readonly AcademicYearService _context;
        public AcademicYearControll(AcademicYearService context)
        {
            _context = context;
        }

        [HttpGet("GetAllAcademicYear")]
        public IActionResult GetAllAcademicYear()
        {
            try
            {
                var GetAcademicYear = _context.ShwoAcademicYear();
                if (GetAcademicYear != null)
                    return Ok(GetAcademicYear);

                return BadRequest(GetAcademicYear);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("AddAcademicYear")]
        public IActionResult AddAcademicYearDto([FromBody]GetAcademicYearDto academicYear)
        {
            try
            {
                var GetAcademicYear = _context.AddAcademicYearDto(academicYear);
                if (GetAcademicYear.ErrorMsg == "تمت إضافة المستوى بنجاح")
                return Ok(GetAcademicYear);
                return BadRequest(GetAcademicYear);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllAcademicYearWithId")]
        public IActionResult GetAllAcademicYearWithId()
        {
            try
            {
                var GetAcademicYear = _context.GetAllAcademicYear();
                if (GetAcademicYear == null || !GetAcademicYear.Any())
                    return BadRequest(GetAcademicYear);
                return Ok(GetAcademicYear);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpDelete("DeleteAcademicYear/{AcadmicYearId}")]
        public IActionResult DeleteAcademicYear(int AcadmicYearId)
        {
            var getResult = _context.DeleteAcademicYear(AcadmicYearId);
            if (getResult.ErrorMsg == "تمت عملية الحذف بنجاح   ")
                return Ok(getResult);
            return BadRequest(getResult);
        }
    }
}

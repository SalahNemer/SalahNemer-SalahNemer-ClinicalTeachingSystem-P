using DataBase.DBcon;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.abed.AllAcademicYearDTO;

namespace V1.abed
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllAcademicYearController : ControllerBase
    {
        private readonly AllAcademinYearsSerivce _context;
        private readonly DBC context;
        public AllAcademicYearController(AllAcademinYearsSerivce con, DBC con1)
        {
            _context = con;
            context = con1;
        }

        [HttpPost("AddCurrentAcademicYear")]
        public IActionResult AddCurrentAcademicYear(AddAllAcademicYearDto addAllAcademicYear)
        {
            try
            {
                var GetAcademicYear = _context.AddCurrentAcademicYearSerivce(addAllAcademicYear);
                if (GetAcademicYear.ErrorMsg == "تمت اضافة سنة دراسية بنجاح")
                    return Ok(GetAcademicYear);
                return BadRequest(GetAcademicYear);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("UpdateAcademicYear/{AcademicYearId}")]
        public IActionResult UpdateAcademicYear(AddAllAcademicYearDto NewAcademicYear, int AcademicYearId)
        {
            try
            {
                return Ok(_context.UpdateAcademicYearSerivce(NewAcademicYear, AcademicYearId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteAcademicYear/{AcademicYearId}")]
        public IActionResult DeleteAcademicYear(int AcademicYearId)
        {
            try
            {
                return Ok(_context.DeleteAcademicYearSerivce(AcademicYearId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAllAcademicYear")]
        public IActionResult GetAllAcademicYear()
        {
            try
            {
                string sql = @"	 
                            select *
                                from AllAcademicYear a 
                        ";
                var result = context.Database.SqlQueryRaw<GetAllAcademicYearDto>(
                    sql).ToList().Count;
                if (result == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                               "Not Found",
                               "not Found "
                               );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.GetAllAcademicYearSerivce());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetAcademicYearById/{AcademicYearId}")]
        public IActionResult GetAcademicYearById(int AcademicYearId)
        {
            try
            {
                string sql = @"	 
                            select *
                                from AllAcademicYear a 

                                where a.CurrentAcademicYearId = @AcademicYearId
                        ";
                var result = context.Database.SqlQueryRaw<GetAllAcademicYearDto>(
                    sql, new SqlParameter("AcademicYearId", AcademicYearId)).ToList().Count;
                if (result == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_ACADEMIC_YEAR,
                               "Not Found",
                               "not Found "
                               );
                    return BadRequest(ErrorMsg);
                }
                return Ok(_context.GetAcademicYearByIdSerivce(AcademicYearId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }        
        }

    }
}
    





    


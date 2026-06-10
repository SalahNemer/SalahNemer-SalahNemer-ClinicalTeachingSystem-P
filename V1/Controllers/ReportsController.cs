using System;
using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.Repotrs;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Interface;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UploadReportsCode.DTO;
using UploadReportsCode.Entity;

namespace UploadReportsCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DBC _context;
        public ReportsController(DBC db)
        {
            _context = db;

        }

        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                string sql = @"
                            SELECT 
                                u.UserId,
                                u.FullName,
                                r.ReportId,
                                r.ReportName,
                                r.ReportAttachment,
                                r.ConcernedParty
                            FROM 
                                Users u
                            JOIN 
                                Reports r ON u.UserId = r.UserId
                             ";
                var result = _context.Database.SqlQueryRaw<GetReportsDto>(sql).ToList();
                if (!result.Any()) 
                {
                    GeneralMsgDto errorMsg = new GeneralMsgDto(
                                 IErrorMsgs.NO_DATA_AVAILABLE,
                                "لا توجد بيانات متاحة حالياً.",  
                                "عذراً، لا توجد أي تقارير لعرضها." 
                        );
                    return BadRequest(errorMsg);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetReportById{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            try
            {
                var report = await _context.Reports.FindAsync(id);
                if (report == null)
                {
                    return NotFound("التقرير الذي تحاول البحث عنه غير موجود");
                }
                string sql = @"
                            SELECT 
                                u.UserId,
                                u.FullName,
                                r.ReportId,
                                r.ReportName,
                                r.ReportAttachment,
                                r.ConcernedParty
                            FROM 
                                Users u
                            JOIN 
                                Reports r ON u.UserId = r.UserId
                            WHERE r.ReportId = @id";
                var result = await _context.Database.SqlQueryRaw<GetReportsDto>(sql, new SqlParameter("@id", id)).ToListAsync();

                if (!result.Any()) 
                {
                    GeneralMsgDto errorMsg = new GeneralMsgDto(
                                 IErrorMsgs.NO_DATA_AVAILABLE,  
                                "لا توجد بيانات متاحة حالياً.",  
                                "عذراً، لا يوجد أي تقارير لعرضها." 
                        );
                    return BadRequest(errorMsg);
                }
                return Ok(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddReport")]
        public async Task<IActionResult> AddReport([FromForm] AddReportsDto report)
        {
            try
            {
                using var stream = new MemoryStream();
                await report.ReportAttachment.CopyToAsync(stream);
                var report2 = new Report
                {
                    UserId = report.UserId,
                    ConcernedParty = report.ConcernedParty,
                    ReportName = report.ReportName,
                    ReportAttachment = stream.ToArray()
                };
                await _context.Reports.AddAsync(report2);
                await _context.SaveChangesAsync();
                return Ok("تمت الاضافة ");
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                var report = await _context.Reports.FindAsync(id);
                if (report == null)
                {
                    GeneralMsgDto errorMsg = new GeneralMsgDto(
                        IErrorMsgs.INVALID_REPORT_ID,
                        "معرف تقرير خاطئ",
                        "لا يوجد أي تقرير يحمل المعرف الذي قمت بإدخاله. الرجاء التأكد من المعرف "
                        );
                    return BadRequest(errorMsg);
                }
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                var successMsg = new GeneralMsgDto(
                    SuccessfullyMsgs.REPORT_DELETED_SUCCESSFULLY,
                                        "تم الحذف بنجاح",
                                        "تم حذف التقرير بنجاح. شكراً لك"
                    );
                return Ok(successMsg);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateReportName{id}")]
        public async Task<IActionResult> UpdateReportName(int id,UpdateReportDTO updateReport)
        {
            try
            {
                var report = await _context.Reports.FindAsync(id);
                if (report == null)
                {
                    var errorMsg = new GeneralMsgDto(
                        IErrorMsgs.INVALID_REPORT_ID,
                        "معرف تقرير خاطئ",
                        "لا يوجد أي تقرير يحمل المعرف الذي قمت بإدخاله. الرجاء التأكد من المعرف "
                        );
                    return BadRequest(errorMsg);
                }
                report.ReportName = updateReport.ReportName;
                report.ConcernedParty = updateReport.ConcernedParty;
                await _context.SaveChangesAsync();
                var successMsg = new GeneralMsgDto(
                    SuccessfullyMsgs.UPDATED_REPORT_NAME_SUCCESSFULLY, "تم التحديث", "تم تحديث  التقرير بنجاح . شكراً لك"
                    );
                return Ok(successMsg);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

using BuildDB_Team.entitys;
using DataBase.DBcon;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using V1.DTO.MarkDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainGroupController : ControllerBase
    {
        private readonly MainGroupService _context;
        private readonly DBC context;
        public MainGroupController(MainGroupService context1, DBC con)
        {
            _context = context1;
            context = con;
        }

        [HttpGet("ShowMainGroupBySympole/{getMainGroupById}")]
        public IActionResult ShowMainGroupBySympole(int getMainGroupById)
        {
            try
            {
                var ValedationGetMainGroupById = context.mainGrops.Where(p => p.MainGroupId == getMainGroupById).ToList().Count;
                var getMainGroup = _context.GetMianGroupBySemolyService(getMainGroupById);
                if (ValedationGetMainGroupById == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                 IErrorMsgs.NOT_FOUND_MAINGROUP,
                                 "Not Found",
                                 "There is not any main Group have this id : " + getMainGroupById
                                 );
                    return BadRequest(ErrorMsg);
                }
                return Ok(getMainGroup);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        [HttpGet("ShowMainGroupByYearAndACY")]
        public IActionResult ShowMainGroupByYearAndACY(int level, string AcadimycYear)
        {
            try
            {
                if (level == null || AcadimycYear == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.MUST_FILL_ALL_FILLED,
                                "Enter the requird filled",
                                "there is not any data"
                                );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    var ValedationAcadimycYear = context.mainGrops.Where(p => p.AcademicYearName == AcadimycYear && p.AcademicYearId == level).ToList().Count;
                    if (ValedationAcadimycYear == 0 && level != 4 && level != 5 && level != 6)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_AcadimycYear + " , " +
                               IErrorMsgs.FAILED_ENTER_LEVEL_EXITING_LIMITS,
                               "Enter the requird filled",
                               "There is not any Academic Level : " + level +
                               " ,  Not Found Acadimic Year : " + AcadimycYear
                               );
                        return BadRequest(ErrorMsg);
                    }
                    if (level != 4 && level != 5 && level != 6)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.FAILED_ENTER_LEVEL_EXITING_LIMITS,
                                             "Not Found",
                                             "There is not any Academic Level : " + level
                                             );
                        return BadRequest(ErrorMsg);
                    }
                    if (ValedationAcadimycYear == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_AcadimycYear,
                               "Not Found ",
                               "Not Found Acadimic Year " + AcadimycYear
                               );
                        return BadRequest(ErrorMsg);
                    }
                    else
                    {
                        var getMainGroup = _context.GetMianGroupByYearAndACYRepo(AcadimycYear, level);
                        return Ok(getMainGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddMainGroup")]
        public IActionResult AddMainGroup([FromBody] AddMainGroupDto mainGroup)
        {
            try
            {
                var result = _context.AddMianGroupServiece(mainGroup);
                if ( result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeleteMainGroup/{mainGroupId}")]
        public IActionResult DeleteMainGroup(int mainGroupId)
        {
            try
            {
                return Ok(_context.DeleteMainGroupInService(mainGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateMainGoup/{MianGroupId}")]
        public IActionResult UpdateMinGoup([FromBody] AddMainGroupDto mainGroup, int MianGroupId)
        {
            try
            {
                 var result = _context.UpdateMainGroupService(mainGroup, MianGroupId);
                 if (result.ErrorMsg == "تم تحديث المجموعة الرئيسية ينجاح")
                    return Ok(result);
                 return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetMianGroupByYearAndACYRepoForTheDistpution/{acadimicYear}/{level}")]
        public IActionResult GetMianGroupByYearAndACYRepoForTheDistpution(string acadimicYear, int level)
        {
            try
            {
                var result = _context.GetMianGroupByYearAndACYRepoForTheDistputionSerive(acadimicYear, level);
                if (result == null || !result.Any())
                {            
                    return BadRequest(result);
                }
                    return Ok(_context.GetMianGroupByYearAndACYRepoForTheDistputionSerive(acadimicYear, level));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

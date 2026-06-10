using DataBase.DBcon;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.SubGroupDTO;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private readonly SubGroupService _context;
        private readonly DBC context;
        public SubGroupController(SubGroupService context1, DBC con12)
        {
            context = con12;
            _context = context1;
        }


        [HttpGet("GetSubGroupInOneMainGroup/{MainGroupId}")]
        public IActionResult GetSubGroupInOneMainGroup(int MainGroupId)
        {
            try
            {
                if (null == MainGroupId)
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
                    var ValedationGetAllSubGroupInOneMainGroupByMainGroupId = context.subGroups.Where(p => p.MainGroupId == MainGroupId).ToList().Count;
                    var getSubGroup = _context.GetSubGroupService(MainGroupId);
                    if (ValedationGetAllSubGroupInOneMainGroupByMainGroupId == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.NOT_FOUND_SUBGROUP,
                               "FAILED",
                               "Ther is not any SubGroup in this main Group" + Convert.ToString(MainGroupId)
                               );
                        return BadRequest(ErrorMsg);

                    }
                    else
                    {
                        return Ok(getSubGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddSubGroup")]
        public IActionResult AddSubGroup([FromBody] AddSubGroupDto SupGroup)
        {
            try
            {
                var result = _context.AddSubGroupService(SupGroup);
                if ( result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpDelete("DeleteSubGroup/{subGrooupId}")]
        public IActionResult DeleteSubGroup(int subGrooupId)
        {
            try
            {
                return Ok(_context.DeleteSubGroupService(subGrooupId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetSubGroupBySubGroupId")]
        public IActionResult GetSubGroupBySubGroupId(int subGroupId)
        {
            try
            {
                if (subGroupId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                       IErrorMsgs.INVALID_DATA_FORMAT,
                       "Enter the Courect sympole of the Main Group",
                       Convert.ToString(subGroupId)
                       );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    var ValedationGetSubGroupById = context.subGroups.Where(p => p.SubGroupId == subGroupId).ToList().Count;
                    if (ValedationGetSubGroupById == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                   IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                                                   "FAILED",
                                                                   "Ther is not any SubGroup have this id   :" + subGroupId
                                                                     );
                        return BadRequest(ErrorMsg);
                    }
                    else
                    {
                        return Ok(_context.GetSubGroupByIdService(subGroupId));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateSubGroupBySubGroupId")]
        public IActionResult UpdateSubGroupBySubGroupId([FromBody] AddSubGroupDto NewDataSubGroup, int subGroupId)
        {
            try
            {
                var NewData = _context.UpdateSubGroupByIdService(NewDataSubGroup, subGroupId);
                if (NewData.ErrorMsg == "تم تحديث المعلومات بنجاح")
                    return Ok(NewData);
                return BadRequest(NewData);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}



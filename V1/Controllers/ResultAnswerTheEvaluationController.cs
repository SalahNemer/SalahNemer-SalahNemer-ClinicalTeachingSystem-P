using DataBase.DBcon;
using DevetionStudetns.entitys;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using testDtoAndmapper.Entity;
using V1.Service;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultAnswerTheEvaluationController : ControllerBase
    {
        readonly private ResultAnswerService _ResultAnswerService;
        private readonly DBC _db;

        public ResultAnswerTheEvaluationController(DBC db, ResultAnswerService ResultAnswerService)
        {
            _ResultAnswerService = ResultAnswerService;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int formid)
        {
            var FormID = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid);
            var Form= await _db.EvaluationForm.FirstOrDefaultAsync(fo=>fo.EvaluationFormId==formid);
            try
            {
                if (Form != null)
                {
                    if (FormID != null)
                    {
                        return Ok(await _ResultAnswerService.GetData(formid));
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Form_Does_Not_Contain_Answers,
                                    "There is not any Students have this Students Id ",
                                    "There is not any students have this id "
                                    );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                                "There is not any Students have this Students Id ",
                                "There is not any students have this id "
                                );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("total")]
        public async Task<IActionResult> Gettotal(int formid)
        {
            var FormID = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid);
            var form = await _db.EvaluationForm.FirstOrDefaultAsync(fo =>fo.EvaluationFormId == formid);
            try
            {
                if (form != null)
                {
                    if (FormID != null)
                    {
                        return Ok(await _ResultAnswerService.GetTotalByFormID(formid));
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Form_Does_Not_Contain_Answers,
                                    "There is not any Students have this Students Id ",
                                    "There is not any students have this id "
                                    );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                                    "There is not any Students have this Students Id ",
                                    "There is not any students have this id "
                                    );
                    return BadRequest(ErrorMsg);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }

        [HttpGet("totalbydoctor")]
        public async Task<IActionResult> totalbydoctor(int formid, string doctorid)
        {
            var FormId = await _db.EvaluationForm.FirstOrDefaultAsync(id => id.EvaluationFormId == formid);
            var DoctorID = await _db.doctors.FirstOrDefaultAsync(id => id.UserId == doctorid);
            var Doctor = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(d => d.EvaluatedPersonId == doctorid);
            var Form = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid);
            try
            {
                if(FormId != null)
                {
                    if (Form != null)
                    {
                        if(DoctorID != null)
                        {
                            if (Doctor != null)
                            {
                                var FormID = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid && f.EvaluatedPersonId == doctorid);
                                if (FormID != null)
                                {
                                    return Ok(await _ResultAnswerService.ShowTotalEvaluationDoctorId(formid, doctorid));
                                }
                                else
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.The_Form_Does_Not_Contain_Answers,
                                                "There is not any Students have this Students Id ",
                                                "There is not any students have this id "
                                                );
                                    return BadRequest(ErrorMsg);
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.The_Doctor_Is_Not_Present_In_This_Form,
                                            "There is not any Students have this Students Id ",
                                            "There is not any students have this id "
                                            );
                                return BadRequest(ErrorMsg);
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DOCTOR_NOT_FOUND,
                                        "There is not any Students have this Students Id ",
                                        "There is not any students have this id "
                                        );
                            return BadRequest(ErrorMsg);
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Model_Not_Found,
                                    "There is not any Students have this Students Id ",
                                    "There is not any students have this id "
                                    );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                                "There is not any Students have this Students Id ",
                                "There is not any students have this id "
                                );
                    return BadRequest(ErrorMsg);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

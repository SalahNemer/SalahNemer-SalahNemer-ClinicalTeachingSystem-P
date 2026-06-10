using BuildDB_Team.entitys;
using DataBase.DBcon;
using FinalProject.DTO.QuestionnaireDTO;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.QuestionnaireDTO;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly DBC context;
        private readonly QuestionnaireSerivce _context;
        public QuestionnaireController(QuestionnaireSerivce context1, DBC con)
        {
            _context = context1;
            context = con;
        }

        [HttpDelete("DeleteQuestionnair/{QuestionnairId}")]
        public IActionResult DeleteQuestionnair(int QuestionnairId)
        {
            try
            {
                return Ok(_context.DeleteQuestionnairService(QuestionnairId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddQuestionnaire")]
        public IActionResult AddQuestionnaire([FromBody] AddQuetionnaireDto addQuetionnaireDto)
        {
            try
            {
                var result = _context.AddQuestionnaireService(addQuetionnaireDto);
                if ( result.ErrorMsg == "تمت الاضافة بنجاح")
                    return Ok(result);      
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateQuestionnair/{QuestionnairId}")]
        public IActionResult UpdateQuestionnair([FromBody] UpdateQuetionnaireDto NewQuestionnairData, int QuestionnairId)
        {
            try
            {
                var result = _context.UpdateQuestionnairService(NewQuestionnairData, QuestionnairId);
                if ( result.ErrorMsg == "تم تحديث بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("ShowAllQuestionnaire/{QuestionnaireType}")]
        public IActionResult ShowAllQuestionnaire(string QuestionnaireType)
        {
            try
            {
                 return Ok(_context.ShowAllQuestionnaireService(QuestionnaireType));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("ShowQuestionnaireById/{QuestionnaireId}")]
        public IActionResult ShowQuestionnaireById(int QuestionnaireId)
        {
            try
            {
                return Ok(_context.ShowQuestionnaireByIdService(QuestionnaireId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("ShowQuestionnaireByUserId/{UserId}")]
        public IActionResult ShowQuestionnaireByUserId(string UserId, string QuestionnaireType)
        {
            try
            {
              return Ok(_context.ShowQuestionnaireByUserIdService(UserId,  QuestionnaireType));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}

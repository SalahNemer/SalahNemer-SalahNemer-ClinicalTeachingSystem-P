using DataBase.DBcon;
using DataBase.Entity;
using DevetionStudetns.DTO.firebase;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.firebase;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DevetionStudetns.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly DBC _dBC;
        private readonly FirebaseClient _firebaseClient;
        public MessageController(DBC db)
        {
            _dBC = db;
            _firebaseClient = new FirebaseClient("https://YOUR_FIREBASE_URL");
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] AddMessDto message)
        {
            var senderExists = _dBC.Users.Any(s => s.UserId == message.SenderId);
            var receiverExists = _dBC.Users.Any(r => r.UserId == message.ReseverId);
            try
            {
                if (senderExists && receiverExists)
                {

                    await _firebaseClient
                         .Child("messages")
                         .PostAsync(message);
                    _dBC.Messages.Add(message.AddNess());
                    _dBC.SaveChanges();

                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
        SuccessfullyMsgs.The_operation_was_completed_successfully,
        "Enter the required filled",
        "there is not any data"
                                  );
                    return Ok(SuccessfullyMsg);
                }
                else if (!senderExists)
                {
                    Console.WriteLine("Sender not found");
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.Sender_not_found,
                                        "Enter the requird filled",
                                        "there is not any data"
                                        );
                    return BadRequest(ErrorMsg);
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.The_future_does_not_exist,
                                        "Enter the requird filled",
                                        "there is not any data"
                                        );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.ACCOUNT_NOT_FOUND,
                                    "Enter the requird filled",
                                    "there is not any data"
                                    );
                return BadRequest(ErrorMsg);
            }
        }

        [HttpGet("between")]
        public async Task<IActionResult> GetMessagesBetweenUsers([FromQuery] string senderId, [FromQuery] string receiverId)
        {
            try
            {
                var messages = await _firebaseClient
        .Child("messages")
        .OrderBy("Timestamp")
        .OnceAsync<GEtMessageDto>();

                var filteredMessages = messages
                    .Where(m => (m.Object.SenderId == senderId && m.Object.ReseverId == receiverId) ||
                                (m.Object.SenderId == receiverId && m.Object.ReseverId == senderId))
                    .Select(m => m.Object)
                    .ToList();

                return Ok(filteredMessages);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        [HttpGet("received/{receiverId}")]
        public async Task<IActionResult> GetReceivedMessages(string receiverId)
        {
            try
            {
                var messages = await _firebaseClient
                    .Child("messages")
                    .OrderBy("ReseverId")
                    .EqualTo(receiverId)
                    .OnceAsync<GEtMessageDto>();

                var messageList = messages.Select(m => m.Object).ToList();
                if (messageList.Count == 0)
                    return BadRequest();

                return Ok(messageList);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}
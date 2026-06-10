

using DataBase.DBcon;
using DataBase.Entity;
using DevetionStudetns.DTO.firebase;
using DevetionStudetns.Error.SuccessfullyMsg;
using Firebase.Database;
using Firebase.Database.Query;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using testDtoAndmapper.Entity;

public class MessageService
{
    private readonly FirebaseClient _firebaseClient;
    private readonly DBC _dBC;

    
    public MessageService(DBC dbC)
    {
        _dBC = dbC;
        _firebaseClient = new FirebaseClient("https://medical-training-system-default-rtdb.firebaseio.com/");
    }

    public async Task<List<AddMessDto>> GetMessagesAsync(string senderId, string receiverId)
    {
        var messages = await _firebaseClient
            .Child("messages")
            .OrderBy("Timestamp")
            .OnceAsync<AddMessDto>();

        var filteredMessages = messages
            .Where(m => (m.Object.SenderId == senderId && m.Object.ReseverId == receiverId) ||
                        (m.Object.SenderId == receiverId && m.Object.ReseverId == senderId))
            .Select(m => m.Object)
            .ToList();

        return filteredMessages;
    }
}

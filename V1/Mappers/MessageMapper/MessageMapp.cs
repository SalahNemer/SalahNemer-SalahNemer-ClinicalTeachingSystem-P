using DataBase.Entity;
using DevetionStudetns.DTO.firebase;

namespace DevetionStudetns.Mappers.firebase
{
    public static class MessageMapp
    {
        public static Message AddNess(this AddMessDto message)
        {
            return new Message
            {
                SenderId = message.SenderId,
                ReseverId = message.ReseverId,
                Contant = message.Contant,
            };
        }

        public static GEtMessageDto GetMess(this Message message)
        {
            return new GEtMessageDto
            {
                SenderId = message.SenderId,
                ReseverId = message.ReseverId,
                DateSend = message.DateSend,
            };
        }
    }
}

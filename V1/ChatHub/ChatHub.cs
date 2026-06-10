using DataBase.DBcon;
using DataBase.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace V1.ChatHub
{
    [Authorize] // 🔴 (تعديل 1)
    public class ChatHub : Hub
    {
        private readonly DBC _context;

        public ChatHub(DBC context)
        {
            _context = context;
        }

        public static Dictionary<string, string> ConnectedUsers = new();

        public override async Task OnConnectedAsync()
        {
            // 🔴 (تعديل 2) تحسين استخراج userId
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers[userId] = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers.Remove(userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string receiverId, string contant)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var message = new Message
            {
                SenderId = senderId,
                ReseverId = receiverId,
                Contant = contant,
                DateSend = DateTime.UtcNow // 🔴 (تعديل 3)
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            if (ConnectedUsers.TryGetValue(receiverId, out var connectionId))
            {
                await Clients.Client(connectionId)
                    .SendAsync("ReceiveMessage", new
                    {
                        SenderId = senderId,
                        Contant = contant,
                        DateSend = message.DateSend
                    });
            }
        }
    }
}
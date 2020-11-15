using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RawCoding_ChapAPp.Data;
using RawCoding_ChatApp.Hubs;
using RawCoding_ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RawCoding_ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _chat = hubContext;
        }

       [Route("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
       {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
       }

        [Route("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [Route("[action]")]
        public async Task<IActionResult> SendMessage(
            int chatId,
            string message,
            string roomName,
            [FromServices] ApplicationDbContext _ctx)
        {
            var userName = User.Identity.Name;

            var messageToSave = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = userName,
                Timestamp = DateTime.Now
            };

            _ctx.Messages.Add(messageToSave);
            await _ctx.SaveChangesAsync();

            await _chat.Clients.Group(roomName)
                .SendAsync("ReceiveMessage", new
                {
                    Text = messageToSave.Text,
                    Name = messageToSave.Name,
                    Timestamp = messageToSave.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });

            return Ok();
        }
    }
}

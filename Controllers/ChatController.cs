using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Database;
using SignalRChatApp.Hubs;
using SignalRChatApp.Models;
using System;
using System.Threading.Tasks;

namespace SignalRChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string message, int chatId, string roomName, [FromServices] AppDbContext appDbContext)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TimeSpan = DateTime.Now
            };
            appDbContext.Messages.Add(Message);
            await appDbContext.SaveChangesAsync();

            await _chat.Clients.Group(roomName).SendAsync("RecieveMessage", new
            {
                Text = Message.Text,
                TimeSpan = Message.TimeSpan.ToString("dd/MM/yyyy hh:mm:ss"),
                Name=Message.Name

            }); 
            return Ok();
        }
    }
}

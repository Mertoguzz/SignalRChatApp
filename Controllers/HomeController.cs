using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Database;
using SignalRChatApp.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SignalRChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var chats = _context.Chats
                .Include(r=>r.Users)
                .Where(r=>!r.Users.Any(t=>t.UserId==User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();

            return View(chats);
        }

        public IActionResult Find()
        {
            var users=_context.Users.Where(r=>r.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();
            return View(users);
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _context.Chats.Include(r => r.Messages).FirstOrDefault(r => r.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var _message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TimeSpan = DateTime.Now
            };
            _context.Messages.Add(_message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = chatId });
        }
        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Member
            };
            _context.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", "Home", new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {

            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };
            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Admin

            });
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
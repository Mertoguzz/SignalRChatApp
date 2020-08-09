using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Database;
using SignalRChatApp.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SignalRChatApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;

        public IActionResult Index() => View();
        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _context.Chats.Include(r => r.Messages).FirstOrDefault(r => r.Id == id);
            return View(chat);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            _context.Chats.Add(new Chat
            {
                Name = name,

                Type = ChatType.Room,
                //Password=password
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private AppDbContext _context;

        public RoomViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var chats = _context.Chats.ToList();
            return View(chats);
        }
    }
}

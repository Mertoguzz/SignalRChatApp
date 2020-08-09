using Microsoft.AspNetCore.Mvc;

namespace SignalRChatApp.Controllers{
    public class HomeController:Controller{
        public IActionResult Index()=>View();
    }
}
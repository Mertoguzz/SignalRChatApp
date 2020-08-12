using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace SignalRChatApp.Models
{
    public class User:IdentityUser
    {
        public ICollection<ChatUser> Chats { get; set; }
    }
}
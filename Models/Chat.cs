using System;
using System.Collections;
using System.Collections.Generic;

namespace SignalRChatApp.Models
{
    public class Chat
    {

        public Chat()
        {
            Messages = new List<Message>();
            Users = new List<ChatUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> Users { get; set; }
        public ChatType  Type { get; set; }
    }
}
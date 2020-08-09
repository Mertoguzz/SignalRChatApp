using System;
using System.Collections;
using System.Collections.Generic;

namespace SignalRChatApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
        public ChatType  Type { get; set; }
    }
}
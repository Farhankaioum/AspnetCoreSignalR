using RawCoding_ChapAPp.Data;
using System;
using System.Collections.Generic;

namespace RawCoding_ChapAPp.Models
{
    public enum ChatType
    {
        Room,
        Private
    }

    // for creating room/chat (private or group message)
    public class Chat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<User> Users { get; set; }

        public ChatType Type { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

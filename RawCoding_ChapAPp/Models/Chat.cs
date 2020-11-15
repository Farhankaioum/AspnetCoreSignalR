using RawCoding_ChapAPp.Data;
using RawCoding_ChatApp.Models;
using System.Collections.Generic;

namespace RawCoding_ChatApp.Models
{

    // for creating room/chat (private or group message)
    public class Chat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> Users { get; set; }

        public ChatType Type { get; set; }
    }
}


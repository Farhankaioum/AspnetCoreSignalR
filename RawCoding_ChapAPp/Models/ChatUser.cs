﻿using RawCoding_ChapAPp.Data;

namespace RawCoding_ChatApp.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public UserRole UserRole { get; set; }
    }
}

﻿namespace LorKingDom.Models
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string FromName { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
        
        // Thêm 2 dòng dưới đây
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
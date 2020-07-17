﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMultiplex.Models
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsInvalidated { get; set; }
        public string UserId { get; set; }

    }
}

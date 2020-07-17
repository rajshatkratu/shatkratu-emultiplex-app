using System;

namespace EMultiplex.Models.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public TimeSpan ExpiryTime { get; set; }
    }
}

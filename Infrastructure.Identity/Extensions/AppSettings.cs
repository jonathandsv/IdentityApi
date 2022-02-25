﻿namespace Infrastructure.Identity.Extensions
{
    public class AppSettings
    {
        public string? Secret { get; set; }
        public int HoursToExpire { get; set; }
        public string? Issuer { get; set; }
        public string? ValidIn { get; set; }

        public string? Url { get; set; }
    }
}

namespace Infrastructure.Identity.Models
{
    public class SecuritySettings
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public string? SigningKey { get; set; }
        public int HoursToExpire { get; set; }
    }
}

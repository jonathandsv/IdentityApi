namespace Infrastructure.Identity.Extensions
{
    public class EmailSettings
    {
        public string? From { get; set; }
        public int Port { get; set; }
        public string? SmtpServer { get; set; }
        public string? Password { get; set; }
    }
}

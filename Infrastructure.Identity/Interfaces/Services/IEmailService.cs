namespace Infrastructure.Identity.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string[] recipients, string subject, string content);
        Task SendConfirmationEmailAsync(string[] recipients, string subject, string userEmail, string code);
        Task SendEmailResetPasswordAsync(string[] recipients, string subject, string userEmail, string code);
    }
}

using Infrastructure.Identity.Extensions;
using Infrastructure.Identity.Interfaces.Services;
using Infrastructure.Identity.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Identity.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        private AppSettings _appSettings;
        public EmailService(IOptions<AppSettings> appSettings, IOptions<EmailSettings> emailSettings)
        {
            _appSettings = appSettings.Value;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string[] recipients, string subject, string content)
        {
            Message message = new Message(recipients, subject, content);
            var mensagemDeEmail = CreateEmailBody(message);
            await SendAsync(mensagemDeEmail);
        }
        
        public async Task SendConfirmationEmailAsync(string[] recipients, string subject, string userEmail, string code)
        {
            Message message = new Message(recipients, subject, string.Empty);
            message.CreateContentMessageConfirmationAccount(_appSettings.Url, userEmail, code);
            var mensagemDeEmail = CreateEmailBody(message);
            await SendAsync(mensagemDeEmail);
        }
        
        public async Task SendEmailResetPasswordAsync(string[] recipients, string subject, string userEmail, string code)
        {
            Message message = new Message(recipients, subject, string.Empty);
            message.CreateContentMessageResetAccount(_appSettings.Url, userEmail, code);
            var mensagemDeEmail = CreateEmailBody(message);
            await SendAsync(mensagemDeEmail);
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer,
                        _emailSettings.Port, true);

                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_emailSettings.From,
                        _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailBody(Message mensagem)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                _emailSettings.From,
                _emailSettings.From));
            emailMessage.To.AddRange(mensagem.Recipients);
            emailMessage.Subject = mensagem.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Content
            };
            return emailMessage;
        }
    }
}

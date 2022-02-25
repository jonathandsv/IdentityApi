using Infrastructure.Identity.Extensions;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Identity.Models
{
    public class Message
    {
        public Message(IEnumerable<string> recipients, string subject, string content)
        {
            Recipients = new List<MailboxAddress>();
            Recipients.AddRange(recipients.Select(d => new MailboxAddress(d, d)));
            Subject = subject;
            Content = content;
        }
        public List<MailboxAddress> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }


        internal void CreateContentMessageConfirmationAccount(string url, string userEmail, string code)
        {
            Content = $"{url}active-account?email={userEmail}&activationToken={code}";
        }

        internal void CreateContentMessageResetAccount(string url, string userEmail, string code)
        {
            Content = $"{url}reset-password?email={userEmail}&activationToken={code}";
        }
    }
}

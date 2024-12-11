
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace TravelExpertsMVC.EmailService
{
    public class EmailSenderService : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        public EmailSenderService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SmtpClient(emailSettings.ServerAddress, emailSettings.ServerPort)
                {
                    EnableSsl = emailSettings.ServerUseSsl,
                    Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.Password)
                };
                await client.SendMailAsync(
                    new MailMessage(
                    from: emailSettings.FromEmail,
                    to: email,
                    subject,
                    message
                    ));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}

using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace BlossomHotel.WebUI.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toName, string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Blossom Hotel", _configuration["EmailSettings:Email"]));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { TextBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _configuration["EmailSettings:SmtpHost"],
                int.Parse(_configuration["EmailSettings:SmtpPort"]),
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Email"],
                _configuration["EmailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

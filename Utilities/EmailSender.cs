using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SecondaApp.Abstract;

namespace SecondaApp.Utilities
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Qui puoi configurare l'invio tramite SMTP o qualsiasi altro servizio di email
            using var client = new SmtpClient(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:Username"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}

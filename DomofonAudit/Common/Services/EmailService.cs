using Common.DTOs;
using Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<EmailResult> SendEmailAsync(string email, string subject, string message)
        {
            var result = new EmailResult() { Success = true };

            using (var client = new SmtpClient())
            {
                try
                {
                    using var emailMessage = PrepareMessage(email, subject, message);
                    await client.ConnectAsync(SmtpSettings.Server, SmtpSettings.Port, SmtpSettings.EnableSsl);
                    await client.AuthenticateAsync(SmtpSettings.Username, SmtpSettings.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (SmtpCommandException ex)
                {
                    // Handle specific SMTP command exception
                    result.Success = false;
                    result.Message = $"SMTP command exception: {ex.Message}";
                    _logger.LogError(ex, result.Message);
                }
                catch (SmtpProtocolException ex)
                {
                    // Handle SMTP protocol exception
                    result.Success = false;
                    result.Message = $"SMTP protocol exception: {ex.Message}";
                    _logger.LogError(ex, result.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    result.Success = false;
                    result.Message = $"An error occurred while sending the email: {ex.Message}";
                    _logger.LogError(ex, result.Message);
                }
            }

            return result;
        }

        private MimeMessage PrepareMessage(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            using (var client = new SmtpClient())
            {
                try
                {
                    emailMessage.From.Add(new MailboxAddress("Domofon Audit", SmtpSettings.From));
                    emailMessage.To.Add(new MailboxAddress("", email));
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = message
                    };

                }
                catch (ArgumentNullException ex)
                {
                    // Handle other exceptions
                    _logger.LogError(ex, ex.Message);
                    throw new ArgumentNullException($"Couldn't compose an email to {email}.");
                }
            }

            return emailMessage;
        }
    }
}

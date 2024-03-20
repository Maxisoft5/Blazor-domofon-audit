using Common.DTOs;

namespace Common.Interfaces
{
    public interface IEmailService
    {
        Task<EmailResult> SendEmailAsync(string email, string subject, string message);
    }
}

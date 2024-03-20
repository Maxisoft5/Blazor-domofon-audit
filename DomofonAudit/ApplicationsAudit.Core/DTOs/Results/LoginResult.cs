using ApplicationsAudit.Domain.Managers;

namespace ApplicationsAudit.Core.DTOs.Results
{
    public class LoginResult
    {
        public Manager Manager { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}

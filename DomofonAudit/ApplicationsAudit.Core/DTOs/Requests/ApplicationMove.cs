using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.Core.DTOs.Requests
{
    public class ApplicationMove
    {
        public ApplicationStatus Status { get; set; }
        public Guid ApplicationId { get; set; }
        public int Index { get; set; }
    }
}

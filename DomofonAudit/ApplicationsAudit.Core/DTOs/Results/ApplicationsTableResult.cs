using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.Core.DTOs.Results
{
    public class ApplicationsTableResult
    {
        public List<ApplicationDTO>? Applications { get; set; }
        public int TotalCount { get; set; }

    }
}

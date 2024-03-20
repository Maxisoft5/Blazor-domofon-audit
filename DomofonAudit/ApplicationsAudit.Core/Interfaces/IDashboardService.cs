using ApplicationsAudit.Core.DTOs;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IDashboardService
    {
        public Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync();
    }
}

using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.DataAccess.DAOs;
using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IApplicationsDao
    {
        public Task<IEnumerable<Application>> GetAllForDashboard();
        public Task<IEnumerable<Application>> GetForTable(ApplicationsFiltersSorts filtersSorts);
        public Task<int> GetCount(ApplicationsFiltersSorts? managersFilters = null);
        public Task<IEnumerable<Application>> GetApplications();
        public Task<Application> GetById(Guid id);
        public Task<Application> Create(Application application);
        public Task MoveApplication(ApplicationStatus status, Guid appId, int index);
        public Task<Application?> UpdateApplication(Guid id, Application application);
    }
}

using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Filters;
using ApplicationsAudit.Core.DTOs.Results;
using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IApplicationService
    {
        public Task<ApplicationDTO> Create(ApplicationDTO application);
        public Task<ApplicationsTableResult> GetForTable(ApplicationTableFiltersDTO applicationTableFilters);
        public Task<int> GetApplicationsCount(ApplicationTableFiltersDTO filtersSort);
        public Task<ApplicationDTO> GetById(Guid id);
        public Task<IEnumerable<ApplicationDTO>> GetApplications();
        public Task MoveApplication(ApplicationStatus status, Guid appId, int index);
        public List<DropDownValue<ApplicationStatus>> GetApplicationStatusesDropdown();
        public Task<ApplicationDTO> Update(Guid id, ApplicationDTO application);
    }
}

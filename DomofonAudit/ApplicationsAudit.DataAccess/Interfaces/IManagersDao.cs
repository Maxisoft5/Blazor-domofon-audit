using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Domain.Managers;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IManagersDao
    {
        public Task<IEnumerable<Manager>> GetManagersAsync(ManagersFiltersSorts? managersFilters = null);
        public Task<IEnumerable<Manager>> GetManagersForDropDown();
        public Task<int> GetCount(ManagersFiltersSorts? managersFilters = null);
        public Task<Manager> GetById(Guid id);
        public Task<Manager?> UpdateManager(Guid id, Manager manager);
    }
}

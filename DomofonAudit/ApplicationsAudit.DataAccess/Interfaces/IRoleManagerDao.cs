using ApplicationsAudit.Domain.Managers;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IRoleManagerDao
    {
        public Task<IEnumerable<ManagerRole>> GetRolesFoDropDown();
        public Task<Guid> GetRoleByName(string name);
        public Task<string> GetRoleNameById(Guid id);

    }
}

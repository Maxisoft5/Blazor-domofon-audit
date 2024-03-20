using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Managers;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class RoleManagerDao : IRoleManagerDao
    {
        private readonly DataContext _dataContext;
        public RoleManagerDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<ManagerRole>> GetRolesFoDropDown()
        {
            var roles = await _dataContext.ManagerRoles.Select(x => new ManagerRole()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return roles;
        }

        public async Task<Guid> GetRoleByName(string name)
        {
            var role = await _dataContext.ManagerRoles.FirstOrDefaultAsync(x => x.NormalizedName == name.ToUpper());
            return role.Id;
        }

        public async Task<string> GetRoleNameById(Guid id)
        {
            var role = await _dataContext.ManagerRoles.FirstOrDefaultAsync(x => x.Id == id);
            return role.Name;
        }
    }
}

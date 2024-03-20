using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Filters;
using ApplicationsAudit.Core.DTOs.Results;
using ApplicationsAudit.Domain.Managers;
using System.Security.Claims;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IManagerService
    {
        public Task<ManagerDTO> GetById(Guid id);
        public Task<IEnumerable<ManagerRoleDTO>> GetRolesForDropDown();
        public Task<ManagerDTO> Register(ManagerDTO managerDTO);
        public Task<LoginResult> SignIn(ManagerDTO managerDTO);
        public Task<SignOutResult> SignOut();
        public Task<Manager?> GetIfAuthorized(ClaimsPrincipal currentUser);
        public Task<Manager?> GetManagerIdentity(ClaimsPrincipal currentUser);
        public Task<List<ManagerDTO>> GetManagers(ManagersFiltersSortsDTO filtersSort);
        public Task<int> GetManagersCount(ManagersFiltersSortsDTO filtersSort);
        public Task<List<ManagerDTO>> GetForDropDown();
    }
}

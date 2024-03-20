using ApplicationsAudit.Domain.Managers;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IManagerInfoDao : IBaseDao<ManagerInfo>
    {
        public Task<ManagerInfo?> UpdateManagerInfo(int id,  ManagerInfo managerInfo);
    }
}

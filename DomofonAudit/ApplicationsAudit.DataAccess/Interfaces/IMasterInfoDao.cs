using ApplicationsAudit.Domain.Masters;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IMasterInfoDao : IBaseDao<MasterInfo>
    {
        public Task<MasterInfo?> UpdateMasterInfo(int id, MasterInfo masterInfo);
    }
}

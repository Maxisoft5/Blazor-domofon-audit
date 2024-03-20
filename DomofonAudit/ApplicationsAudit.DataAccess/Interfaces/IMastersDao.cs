using ApplicationsAudit.Domain.Districts;
using ApplicationsAudit.Domain.Masters;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IMastersDao : IBaseDao<Master>
    {
        public Task<MasterDistrict> AssignDistrict(int masterId, int districtId);
        public Task<Master?> UpdateMaster(int id, Master master);
    }
}

using ApplicationsAudit.Domain.Districts;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IDistrictsDao : IBaseDao<District>
    {
        public Task<District> GetById(int id);
        public Task<District?> UpdateDistrict(int id, District district);
    }
}

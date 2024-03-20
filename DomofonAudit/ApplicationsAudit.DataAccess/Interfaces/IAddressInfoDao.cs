using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Applications;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IAddressInfoDao : IBaseDao<AddressInfo>
    {
        public Task<AddressInfo> GetById(int id);
        public Task<AddressInfo?> UpdateAddressInfo(int id, AddressInfo addressInfo);
        public Task<ApplicationAddress> Create(ApplicationAddress address);
    }
}

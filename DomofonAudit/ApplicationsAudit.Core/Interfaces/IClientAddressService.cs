using ApplicationsAudit.Core.DTOs;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IClientAddressService : IBaseService<ClientAddressDTO>
    {
        public Task<IEnumerable<ClientAddressDTO>> GetAllByClientId(int id);
    }
}

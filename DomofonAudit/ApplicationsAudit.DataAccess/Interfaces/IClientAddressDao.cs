using ApplicationsAudit.DataAccess.DAOs;
using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Clients;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IClientAddressDao : IBaseDao<ClientAddress>
    {
        public Task<ClientAddress> GetById(int id);
        public Task<IEnumerable<ClientAddress>> GetAllByClientId(int id);
        public Task<ClientAddress?> UpdateClientAddress(int id, ClientAddress clientAddress);
    }
}

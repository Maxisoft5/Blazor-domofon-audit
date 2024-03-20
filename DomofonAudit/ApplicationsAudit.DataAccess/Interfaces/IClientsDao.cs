using ApplicationsAudit.DataAccess.DAOs;
using ApplicationsAudit.Domain.Clients;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IClientsDao : IBaseDao<Client>
    {
        public Task<IEnumerable<Client>> LoadClients(string nameFilter);
        public Task<Client?> UpdateClient(int id, Client client);
    }
}

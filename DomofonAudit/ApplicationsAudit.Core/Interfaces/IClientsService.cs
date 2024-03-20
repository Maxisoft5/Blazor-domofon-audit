using ApplicationsAudit.Core.DTOs;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IClientsService : IBaseService<ClientDTO>
    {
        public Task CreateNewFromApplication(ApplicationNewClient client);
        public Task<IEnumerable<ClientDTO>> LoadClientsForDropDown(string nameFilter);
    }
}

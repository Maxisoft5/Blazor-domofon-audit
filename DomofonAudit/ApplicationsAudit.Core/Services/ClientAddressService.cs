using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Clients;

namespace ApplicationsAudit.Core.Services
{
    public class ClientAddressService : IClientAddressService
    {
        private readonly IClientAddressDao _clientAddressDao;
        public ClientAddressService(IClientAddressDao clientAddressDao)
        {
            _clientAddressDao = clientAddressDao;
        }

        public async Task<ClientAddressDTO> Create(ClientAddressDTO item)
        {
            var clientAddress = new ClientAddress()
            {
                AddressId = item.Address.Id,
                ClientId = item.Client.Id,
                DistrictId = item.District.Id,
            };
            var result = await _clientAddressDao.Create(clientAddress);
            item.Id = result.Id;
            return item;
        }

        public Task<IEnumerable<ClientAddressDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientAddressDTO>> GetAllByClientId(int id)
        {
            var all = await _clientAddressDao.GetAllByClientId(id);
            var list = new List<ClientAddressDTO>();
            foreach (var item in all)
            {
                list.Add(new ClientAddressDTO()
                {
                    Id = item.Id
                });
            }
            return list;
        }

        public Task<ClientAddressDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

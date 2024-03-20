using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Clients;

namespace ApplicationsAudit.Core.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsDao _clientsDao;
        private readonly IContactInfoService _contactInfoService;
        private readonly IAddressInfoService _addressInfoService;
        private readonly IClientAddressService _clientAddressService;
        public ClientsService(IClientsDao clientsDao, IContactInfoService contactInfoService,
            IClientAddressService clientAddressService,
            IAddressInfoService addressInfoService)
        {
            _clientsDao = clientsDao;
            _contactInfoService = contactInfoService;
            _addressInfoService = addressInfoService;
            _clientAddressService = clientAddressService;
        }

        public async Task<ClientDTO> Create(ClientDTO item)
        {
            var client = new Client()
            {
                ContactInfoId = item.ContactInfoId,
                AddedDate = DateTime.UtcNow,
            };
            var result = await _clientsDao.Create(client);
            var clientInfo = await _contactInfoService.GetById(item.ContactInfoId);
            var addresses = await _clientAddressService.GetAllByClientId(result.Id);

            var dto = new ClientDTO()
            {
                Id = result.Id,
                AddedDate = result.AddedDate,
                ContactInfo = clientInfo,
                Addresses = addresses
            };
            return dto;
        }

        public async Task CreateNewFromApplication(ApplicationNewClient client)
        {
            var clientInfo = new ContactInfoDTO()
            {
                Email = client.Email,
                CreatedDate = DateTime.UtcNow,
                FirstName = client.FirstName,
                HomePhone = client.HomePhone,
                LastName = client.LastName,
                IsActive = true,
                MobilePhone1 = client.MobilePhone1,
                MobilePhone2 = client.MobilePhone2,
                Patronymic = client.Patronymic
            };
            var info = await _contactInfoService.Create(clientInfo);
            var address = new AddressInfoDTO()
            {
                City = client.City,
                EntranceNumber = client.EntranceNumber,
                FlatNumber = client.FlatNumber,
                HomeNumber = client.HomeNumber,
                Region = client.Region,
                Street = client.Street
            };
            address = await _addressInfoService.Create(address);

            var createdClient = await Create(new ClientDTO()
            {
                ContactInfoId = info.Id,
            });

            var clientAddress = new ClientAddressDTO()
            {
                Address = address,
                District = client.District,
                Client = createdClient
            };
            await _clientAddressService.Create(clientAddress);

        }

        public Task<IEnumerable<ClientDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientDTO>> LoadClientsForDropDown(string nameFilter)
        {
            var clients = await _clientsDao.LoadClients(nameFilter);
            var result = new List<ClientDTO>();
            foreach (var client in clients)
            {
                result.Add(new ClientDTO()
                {
                    AddedDate = client.AddedDate,
                    Id = client.Id,
                    ContactInfo = new ContactInfoDTO()
                    {
                        CreatedDate = client.ContactInfo.CreatedDate,
                        Email = client.ContactInfo.Email,
                        FirstName = client.ContactInfo.FirstName,
                        HomePhone = client.ContactInfo.HomePhone,
                        LastName = client.ContactInfo.LastName,
                        MobilePhone1 = client.ContactInfo.MobilePhone1,
                        MobilePhone2 = client.ContactInfo.MobilePhone2,
                        Patronymic = client.ContactInfo.Patronymic
                    }
                });
            }
            return result;
        }
    }
}

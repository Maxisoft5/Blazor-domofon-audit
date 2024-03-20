using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Clients;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ClientAddressDao : IClientAddressDao
    {
        private readonly DataContext _dataContext;
        public ClientAddressDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ClientAddress> Create(ClientAddress item)
        {
            await _dataContext.ClientAddresses.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public Task<IEnumerable<ClientAddress>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientAddress>> GetAllByClientId(int id)
        {
            var all = await _dataContext.ClientAddresses
                .Include(x => x.Address)
                .Include(x => x.Client)
                .Include(x => x.District)
                .Where(x => x.ClientId == id)
                .ToListAsync();

            return all;
        }

        public async Task<ClientAddress> GetById(int id)
        {
            var address = await _dataContext.ClientAddresses.FirstOrDefaultAsync(x => x.Id == id);
            return address;
        }

        public async Task<ClientAddress?> UpdateClientAddress(int id, ClientAddress clientAddress)
        {
            var exists = await _dataContext.ClientAddresses.FirstOrDefaultAsync(x => x.Id == id);

            if (exists != null)
            {
                exists.AddressId = clientAddress.AddressId;
                exists.ClientId = clientAddress.ClientId;
                exists.DistrictId = clientAddress.DistrictId;
                _dataContext.ClientAddresses.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }

            return null;
        }
    }
}

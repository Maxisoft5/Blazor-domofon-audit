using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Clients;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ClientsDao : IClientsDao
    {
        private readonly DataContext _dataContext;
        public ClientsDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> Create(Client item)
        {
            await _dataContext.Clients.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var all = await _dataContext.Clients.AsNoTracking().Select(x => new Client()
            {
                ContactInfo = x.ContactInfo,
                Addresses = x.Addresses,
                Id = x.Id,
                AddedDate = DateTime.UtcNow
            }).ToListAsync();
            return all;
        }

        public async Task<IEnumerable<Client>> LoadClients(string nameFilter)
        {
            if (string.IsNullOrEmpty(nameFilter))
            {

                return await _dataContext.Clients.Include(x => x.ContactInfo)
                    .ToListAsync();
            } 
            else
            {
                return await _dataContext.Clients.Include(x => x.ContactInfo)
                    .Where(x => x.ContactInfo.FirstName.Contains(nameFilter) || x.ContactInfo.LastName.Contains(nameFilter)
                    || x.ContactInfo.Patronymic.Contains(nameFilter)).ToListAsync();
            }
        }

        public async Task<Client?> UpdateClient(int id, Client client)
        {
            var exists = await _dataContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.LockOutDate = client.LockOutDate;
                exists.AddedDate = client.AddedDate;
                exists.ContactInfoId = client.ContactInfoId;
                exists.LockOutEnabled = client.LockOutEnabled;
                _dataContext.Clients.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Applications;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class AddressInfoDao : IAddressInfoDao
    {
        private readonly DataContext _dataContext;
        public AddressInfoDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AddressInfo> Create(AddressInfo item)
        {
            await _dataContext.AddressInfos.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<ApplicationAddress> Create(ApplicationAddress address)
        {
            await _dataContext.ApplicationAddresses.AddAsync(address);
            await _dataContext.SaveChangesAsync();
            return address;
        }

        public Task<IEnumerable<AddressInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AddressInfo> GetById(int id)
        {
            var address = await _dataContext.AddressInfos.FirstOrDefaultAsync(x => x.Id == id);
            return address;
        }

        public async Task<AddressInfo?> UpdateAddressInfo(int id, AddressInfo addressInfo)
        {
            var exists = await _dataContext.AddressInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.Street = addressInfo.Street;
                exists.City = addressInfo.City;
                exists.EntranceNumber = addressInfo.EntranceNumber;
                exists.FlatNumber = addressInfo.FlatNumber;
                exists.Region = addressInfo.Region;
                exists.HomeNumber = addressInfo.HomeNumber;
                _dataContext.AddressInfos.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

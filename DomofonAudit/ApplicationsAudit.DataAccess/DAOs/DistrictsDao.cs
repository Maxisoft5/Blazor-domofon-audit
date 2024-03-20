using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Districts;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class DistrictsDao : IDistrictsDao
    {
        private readonly DataContext _dataContext;
        public DistrictsDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<District> Create(District item)
        {
            await _dataContext.Districts.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<District>> GetAll()
        {
            var all = await _dataContext.Districts.AsNoTracking().Select(x => new District()
            {
                Id = x.Id,
                Name = x.Name,
                City = x.City
            }).ToListAsync();
            return all;
        }

        public async Task<District> GetById(int id)
        {
            var item = await _dataContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<District?> UpdateDistrict(int id, District district)
        {
            var exists = await _dataContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.City = district.City;
                exists.Name = district.Name;
                _dataContext.Districts.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

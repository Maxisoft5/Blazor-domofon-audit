using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Districts;
using ApplicationsAudit.Domain.Masters;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class MastersDao : IMastersDao
    {
        private readonly DataContext _dataContext;
        public MastersDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MasterDistrict> AssignDistrict(int masterId, int districtId)
        {
            var masterDistrict = new MasterDistrict()
            {
                DistrictId = districtId,
                CreatedAt = DateTime.UtcNow,
                MasterId = masterId,
            };
            await _dataContext.MasterDistricts.AddAsync(masterDistrict);
            await _dataContext.SaveChangesAsync();
            return masterDistrict;
        }

        public async Task<Master> Create(Master item)
        {
            await _dataContext.Masters.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Master>> GetAll()
        {
            var all = await _dataContext.Masters.Include(x => x.MasterInfo)
                .ThenInclude(x => x.ContactInfo).AsNoTracking()
                .ToListAsync();

            return all;
        }

        public async Task<Master?> UpdateMaster(int id, Master master)
        {
            var exists = await _dataContext.Masters.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.MasterInfoId = master.MasterInfoId;
                exists.WorkingTimeId = master.WorkingTimeId;
                _dataContext.Masters.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
            
        }
    }
}

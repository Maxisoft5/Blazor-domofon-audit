using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Masters;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class MasterInfoDao : IMasterInfoDao
    {
        private readonly DataContext _dataContext;
        public MasterInfoDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MasterInfo> Create(MasterInfo item)
        {
            await _dataContext.MasterInfos.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public Task<IEnumerable<MasterInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<MasterInfo?> UpdateMasterInfo(int id, MasterInfo masterInfo)
        {
            var exists = await _dataContext.MasterInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.FiredDate = masterInfo.FiredDate;
                exists.IsFired = masterInfo.IsFired;
                exists.AdditionalYearlySalary = masterInfo.AdditionalYearlySalary;
                exists.ContactInfoId = masterInfo.ContactInfoId;
                exists.MonthlySalary = masterInfo.MonthlySalary;
                exists.StartedWorkAt = masterInfo.StartedWorkAt;
                _dataContext.MasterInfos.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

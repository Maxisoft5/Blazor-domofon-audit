using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Managers;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ManagerInfoDao : IManagerInfoDao
    {
        private readonly DataContext _dataContext;
        public ManagerInfoDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ManagerInfo> Create(ManagerInfo item)
        {
            await _dataContext.ManagerInfos.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public Task<IEnumerable<ManagerInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ManagerInfo?> UpdateManagerInfo(int id, ManagerInfo managerInfo)
        {
            var exists = await _dataContext.ManagerInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.FiredDate = managerInfo.FiredDate;
                exists.IsFired = managerInfo.IsFired;
                exists.AdditionalYearlySalary = managerInfo.AdditionalYearlySalary;
                exists.ContactInfoId = managerInfo.ContactInfoId;
                exists.MonthlySalary = managerInfo.MonthlySalary;
                exists.StartedWorkAt = managerInfo.StartedWorkAt;
                _dataContext.ManagerInfos.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

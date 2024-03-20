using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.WorkingTimes;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class WorkingTimesDao : IWorkingTimesDao
    {
        private readonly DataContext _dataContext;
        public WorkingTimesDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<WeeklyWorkingTime> Create(WeeklyWorkingTime item)
        {
            await _dataContext.WeeklyWorkingTimes.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public Task<IEnumerable<WeeklyWorkingTime>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<WeeklyWorkingTime> GetWorkingTimeByDays(WeeklyWorkingTime weeklyWorking)
        {
            var time = await _dataContext.WeeklyWorkingTimes.FirstOrDefaultAsync(x =>
                x.Saturday == weeklyWorking.Saturday 
                && x.Sunday == weeklyWorking.Sunday
                && x.Monday == weeklyWorking.Monday 
                && x.Tuesday == weeklyWorking.Tuesday
                && x.Wednesday == weeklyWorking.Wednesday 
                && x.Thursday == weeklyWorking.Thursday
                && x.Friday == weeklyWorking.Friday);

            return time;
        }

        public async Task<WeeklyWorkingTime?> UpdateWorkingTimes(int id, WeeklyWorkingTime weeklyWorking)
        {
            var exists = await _dataContext.WeeklyWorkingTimes.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.Monday = weeklyWorking.Monday;
                exists.Tuesday = weeklyWorking.Tuesday;
                exists.Wednesday = weeklyWorking.Wednesday;
                exists.Thursday = weeklyWorking.Thursday;
                exists.Friday = weeklyWorking.Friday;
                exists.Saturday = weeklyWorking.Saturday;
                exists.Sunday = weeklyWorking.Sunday;
                exists.IsTimeActive = weeklyWorking.IsTimeActive;
                _dataContext.WeeklyWorkingTimes.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

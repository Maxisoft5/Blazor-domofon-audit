using ApplicationsAudit.Domain.WorkingTimes;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IWorkingTimesDao : IBaseDao<WeeklyWorkingTime>
    {
        public Task<WeeklyWorkingTime> GetWorkingTimeByDays(WeeklyWorkingTime weeklyWorking);
        public Task<WeeklyWorkingTime?> UpdateWorkingTimes(int id, WeeklyWorkingTime weeklyWorking);
    }
}

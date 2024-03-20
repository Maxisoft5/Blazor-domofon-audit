using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.WorkingTimes;

namespace ApplicationsAudit.Core.Services
{
    public class WorkingTimeService : IWorkingTimeService
    {
        private readonly IWorkingTimesDao _workingTimesDao;

        public WorkingTimeService(IWorkingTimesDao workingTimesDao)
        {
            _workingTimesDao = workingTimesDao;
        }

        public async Task<WorkingTimeDTO> Create(WorkingTimeDTO item)
        {
            var time = new WeeklyWorkingTime()
            {
                Monday = item.Monday,
                Tuesday = item.Tuesday,
                Wednesday = item.Wednesday,
                Thursday = item.Thursday,
                Friday = item.Friday,
                IsTimeActive = true,
                Saturday = item.Saturday,
                Sunday = item.Sunday
            };
            var saved = await _workingTimesDao.Create(time);
            item.IsTimeActive = saved.IsTimeActive;
            item.Id = saved.Id;
            return item;
        }

        public Task<IEnumerable<WorkingTimeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<WorkingTimeDTO?> GetByDays(WorkingTimeDTO workingTime)
        {
            var time = await _workingTimesDao.GetWorkingTimeByDays(new WeeklyWorkingTime()
            {
                Friday = workingTime.Friday,
                Thursday = workingTime.Thursday,
                Wednesday = workingTime.Wednesday,
                Tuesday = workingTime.Tuesday,
                Monday = workingTime.Monday,
                Saturday= workingTime.Saturday,
                Sunday= workingTime.Sunday
            });
            if (time == null)
            {
                return null;
            }
            return new WorkingTimeDTO()
            {
                Id = time.Id,
                Monday = time.Monday,
                Tuesday = time.Tuesday,
                Wednesday = time.Wednesday,
                Thursday = time.Thursday,
                Friday = time.Friday
            };
        }

        public Task<WorkingTimeDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

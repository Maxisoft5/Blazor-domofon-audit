using ApplicationsAudit.Core.DTOs;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IWorkingTimeService : IBaseService<WorkingTimeDTO>
    {
        public Task<WorkingTimeDTO?> GetByDays(WorkingTimeDTO workingTime);
    }
}

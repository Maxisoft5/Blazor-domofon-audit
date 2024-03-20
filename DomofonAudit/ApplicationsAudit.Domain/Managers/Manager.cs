using ApplicationsAudit.Domain.Applications;
using ApplicationsAudit.Domain.WorkingTimes;
using Microsoft.AspNetCore.Identity;

namespace ApplicationsAudit.Domain.Managers
{
    public class Manager : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public Guid ManagerRoleId { get; set; }
        public ManagerRole ManagerRole { get; set; }
        public int ManagerInfoId { get; set; }
        public ManagerInfo ManagerInfo { get; set; }
        public IEnumerable<Application> Applications { get; set; }
        public int WorkingTimeId {  get; set; }
        public WeeklyWorkingTime WorkingTime { get; set; }
    }
}

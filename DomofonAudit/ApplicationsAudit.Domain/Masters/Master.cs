using ApplicationsAudit.Domain.Applications;
using ApplicationsAudit.Domain.Districts;
using ApplicationsAudit.Domain.WorkingTimes;

namespace ApplicationsAudit.Domain.Masters
{
    public class Master
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MasterInfoId { get; set; }
        public MasterInfo MasterInfo { get; set; }
        public IEnumerable<Application> Applications { get; set; }
        public int WorkingTimeId {  get; set; }
        public WeeklyWorkingTime WorkingTime { get; set; }
        public IEnumerable<MasterDistrict> MasterDistricts { get; set; }
    }
}

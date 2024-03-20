using ApplicationsAudit.Domain.Base;

namespace ApplicationsAudit.Domain.Managers
{
    public class ManagerInfo : EmployeeInfo
    {
        public int Id { get; set; }
        public int ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}

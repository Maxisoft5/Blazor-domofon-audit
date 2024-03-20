using ApplicationsAudit.Domain.Base;

namespace ApplicationsAudit.Domain.Masters
{
    public class MasterInfo : EmployeeInfo
    {
        public int Id { get; set; }
        public int ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}

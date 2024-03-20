using ApplicationsAudit.Domain.Base;

namespace ApplicationsAudit.Core.DTOs
{
    public class ManagerInfoDTO : EmployeeInfoDTO
    {
        public int Id { get; set; }
        public int ContactInfoId { get; set; }
        public string? Password { get; set; }
        public ContactInfoDTO ContactInfo { get; set; }
    }
}

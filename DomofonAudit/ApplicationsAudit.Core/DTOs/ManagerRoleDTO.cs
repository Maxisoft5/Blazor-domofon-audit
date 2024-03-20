using ApplicationsAudit.Core.DTOs.Enums;

namespace ApplicationsAudit.Core.DTOs
{
    public class ManagerRoleDTO
    {
        public Guid Id {  get; set; }
        public Roles Role { get; set; }
        public string? RoleName { get; set; }
    }
}

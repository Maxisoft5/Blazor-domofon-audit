using ApplicationsAudit.Core.DTOs.Enums;

namespace ApplicationsAudit.Core.DTOs.Filters
{
    public class ManagersFiltersSortsDTO
    {
        public Roles? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public (string sortField, int type)? Sort { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}

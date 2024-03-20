namespace ApplicationsAudit.Core.DTOs
{
    public class ManagersFiltersSorts
    {
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public (string sortField, int type)? Sort { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}

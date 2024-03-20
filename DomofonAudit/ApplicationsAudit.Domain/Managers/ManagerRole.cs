using Microsoft.AspNetCore.Identity;

namespace ApplicationsAudit.Domain.Managers
{
    public class ManagerRole : IdentityRole<Guid>
    {
        public DateTime CreatedAt { get; set; }
    }
}

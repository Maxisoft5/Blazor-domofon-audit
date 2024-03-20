using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationsAudit.Core.DTOs.Enums
{
    public enum Roles
    {
        [Description("Администратор")]
        [Display(Description = "Администратор")]
        Admin = 0,
        [Description("Диспетчер")]
        [Display(Description = "Диспетчер")]
        Dispatcher = 1
    }
}

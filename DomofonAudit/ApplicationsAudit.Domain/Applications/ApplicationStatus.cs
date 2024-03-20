using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApplicationsAudit.Domain.Applications
{
    public enum ApplicationStatus
    {
        [Description("Готов для работы")]
        [Display(Description = "Готов для работы")]
        ReadyForAssign = 0,
        [Description("В процессе")]
        [Display(Description = "В процессе")]
        Assigned = 1,
        [Description("Сделано")]
        [Display(Description = "Сделано")]
        Resolved = 2,
        [Description("Отклонено")]
        [Display(Description = "Отклонено")]
        Rejected = 3
    }
}

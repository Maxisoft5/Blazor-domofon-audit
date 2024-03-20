using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class CreateManagerEvent
    {
        public event CreateManagerDelegate? CreateManager;

        public void NotifyCreateManager(object sender, ManagerDTO manager)
        {
            if (CreateManager != null)
            {
                CreateManager(sender, manager);
            }
        }
    }
}

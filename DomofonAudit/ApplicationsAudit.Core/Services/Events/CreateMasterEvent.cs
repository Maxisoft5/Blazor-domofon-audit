using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class CreateMasterEvent
    {
        public event CreateMasterDelegate? CreateMaster;
        public void NotifyCreateMaster(object sender, MasterDTO master)
        {
            if (CreateMaster != null)
            {
                CreateMaster(sender, master);
            }
        }
    }
}

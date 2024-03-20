using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class ApplicationEvents
    {
        public event CreateApplicationDelegate? CreateApplication;
        public event UpdateApplicationDelegate? UpdateApplication;

        public void NotifyCreateApplication(object sender, ApplicationDTO application)
        {
            if (CreateApplication != null)
            {
                CreateApplication(sender, application);
            }
        }

        public void NotifyUpdateApplication(object sender, ApplicationDTO application)
        {
            if(UpdateApplication != null)
            {
                UpdateApplication(sender, application);
            }
        }

    }
}

using ApplicationsAudit.Core.Services.Delegates;
using ApplicationsAudit.Domain.Managers;

namespace ApplicationsAudit.Core.Services.Events
{
    public class SignInEvent
    {
        public event SignInDelegate? OnSignIn;
        public void NotifySignIn(object sender, Manager manager)
        {
            if (OnSignIn != null)
            {
                OnSignIn(sender, manager);
            }
        }
    }
}

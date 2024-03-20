using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class CreateClientEvent
    {
        public event CreateClientDelegate? CreateClient;

        public void NotifyCreateClient(object sender, ClientDTO client)
        {
            if (CreateClient != null)
            {
                CreateClient(sender, client);
            }
        }
    }
}

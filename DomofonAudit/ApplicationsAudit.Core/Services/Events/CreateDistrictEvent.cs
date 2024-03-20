using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class CreateDistrictEvent
    {
        public event CreateDistrictDelegate? CreateDistrict;

        public void NotifyCreateDistrict(object sender, DistrictDTO distrcit)
        {
            if (CreateDistrict != null)
            {
                CreateDistrict(sender, distrcit);
            }
        }
    }
}

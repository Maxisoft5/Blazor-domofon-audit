using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Districts;

namespace ApplicationsAudit.Domain.Applications
{
    public class ApplicationAddress
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int AddressId { get; set; }
        public AddressInfo Address { get; set; }
    }
}

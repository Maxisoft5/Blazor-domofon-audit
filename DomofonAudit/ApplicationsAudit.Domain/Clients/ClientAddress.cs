using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Districts;

namespace ApplicationsAudit.Domain.Clients
{
    public class ClientAddress
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int AddressId {  get; set; }
        public AddressInfo Address { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}

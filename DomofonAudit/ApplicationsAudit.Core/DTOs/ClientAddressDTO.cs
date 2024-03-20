namespace ApplicationsAudit.Core.DTOs
{
    public class ClientAddressDTO
    {
        public int Id { get; set; }
        public ClientDTO Client { get; set; }
        public int ClientId { get; set; }
        public AddressInfoDTO Address { get; set; }
        public int AddressId { get; set; }
        public DistrictDTO District { get; set; }
        public int DistrictId { get; set; }
    }
}

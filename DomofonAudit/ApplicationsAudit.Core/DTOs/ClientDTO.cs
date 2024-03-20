namespace ApplicationsAudit.Core.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public IEnumerable<ClientAddressDTO> Addresses { get; set; }
        public ContactInfoDTO ContactInfo { get; set; }
        public int ContactInfoId { get; set; }
        public bool LockOutEnabled { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? LockOutDate { get; set; }
    }
}

using ApplicationsAudit.Domain.Applications;
using ApplicationsAudit.Domain.Base;

namespace ApplicationsAudit.Domain.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public IEnumerable<ClientAddress> Addresses { get; set; }
        public IEnumerable<Application> Applications { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public int ContactInfoId { get; set; }
        public bool LockOutEnabled { get; set; }
        public DateTime AddedDate {  get; set; } 
        public DateTime? LockOutDate { get; set; }
    }
}

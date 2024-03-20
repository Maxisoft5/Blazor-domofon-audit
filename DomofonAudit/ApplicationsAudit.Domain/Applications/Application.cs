using ApplicationsAudit.Domain.Clients;
using ApplicationsAudit.Domain.Managers;
using ApplicationsAudit.Domain.Masters;

namespace ApplicationsAudit.Domain.Applications
{
    public class Application
    {
        public Guid Id { get; set; }
        public int PositionIndexInColumn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequestedById {  get; set; }
        public Client RequestedBy {  get; set; }
        public Guid? AssignedManagerId {  get; set; }
        public Manager? AssignedManager { get; set; }
        public int? AssignedMasterId {  get; set; }
        public Master? AssignedMaster {  get; set; }
        public int ApplicationAddressId { get; set; }
        public ApplicationAddress Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdateAt {  get; set; }
        public DateTime? ResolvedDate {  get; set; }
        public ApplicationStatus Status {  get; set; }
        public IEnumerable<ApplicationComment> Comments { get; set; }
    }
}

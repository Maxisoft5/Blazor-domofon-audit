using ApplicationsAudit.Domain.Applications;
using ApplicationsAudit.Domain.Masters;

namespace ApplicationsAudit.Core.DTOs
{
    public class ApplicationDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public ClientDTO RequestedBy { get; set; }
        public Guid? ManagerId { get; set; }
        public ManagerDTO? AssignedManager { get; set; }
        public int? MasterId {  get; set; }
        public MasterDTO? AssignedMaster { get; set; }
        public int AddressId { get; set; }
        public ApplicationAddress Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public List<ApplicationCommentDTO> Comments { get; set; }
    }
}

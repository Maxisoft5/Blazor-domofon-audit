using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.Core.DTOs
{
    public class ApplicationsFiltersSortsDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int RequestedById { get; set; }
        public Guid? AssignedManagerId { get; set; }
        public int? AssignedMasterId { get; set; }
        public int ApplicationAddressId { get; set; }
        public string? ApplicationStreet { get; set; }
        public int HomeNumber { get; set; }
        public int EntranceNumber { get; set; }
        public int FlatNumber { get; set; }
        public string? DistrictName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? CommentMessageWord { get; set; }
        public string? CommentedBy { get; set; }
        public DateTime CommentAddedAt { get; set; }
    }
}

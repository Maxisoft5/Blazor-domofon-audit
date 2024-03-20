namespace ApplicationsAudit.Core.DTOs
{
    public class ApplicationCommentDTO
    {
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime AddedAt { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
    }
}

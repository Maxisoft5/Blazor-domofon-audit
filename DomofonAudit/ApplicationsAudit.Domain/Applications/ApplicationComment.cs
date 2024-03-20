namespace ApplicationsAudit.Domain.Applications
{
    public class ApplicationComment
    {
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        public DateTime AddedAt { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
    }
}

namespace ApplicationsAudit.Core.DTOs
{
    public class WorkingTimeDTO
    {
        public int Id { get; set; }
        public bool IsTimeActive { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}

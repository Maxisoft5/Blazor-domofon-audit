namespace ApplicationsAudit.Core.DTOs
{
    public class MasterInfoDTO
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public MasterDTO Master { get; set; }
        public int ContactInfoId { get; set; }
        public ContactInfoDTO ContactInfo { get; set; }
    }
}

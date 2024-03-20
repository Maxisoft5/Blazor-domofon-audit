namespace ApplicationsAudit.Core.DTOs
{
    public class MasterDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public MasterInfoDTO MasterInfo { get; set; }
        public IEnumerable<ApplicationDTO> Applications { get; set; }
        public WorkingTimeDTO WorkingTime { get; set; }
        public IEnumerable<MasterDistrictDTO> MasterDistricts { get; set; }
    }
}

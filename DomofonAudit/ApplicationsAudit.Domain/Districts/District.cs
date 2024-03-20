using ApplicationsAudit.Domain.Applications;

namespace ApplicationsAudit.Domain.Districts
{
    public class District
    {
        public int Id { get; set; }
        public DateTime CreatedDate {  get; set; } 
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public IEnumerable<ApplicationAddress> ApplicationAddresses { get; set; }
        public IEnumerable<MasterDistrict> MasterDistricts { get; set; }
    }
}

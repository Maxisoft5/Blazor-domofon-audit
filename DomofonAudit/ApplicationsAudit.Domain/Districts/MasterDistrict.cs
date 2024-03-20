using ApplicationsAudit.Domain.Masters;

namespace ApplicationsAudit.Domain.Districts
{
    public class MasterDistrict
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public Master Master { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public DateTime CreatedAt {  get; set; }
    }
}

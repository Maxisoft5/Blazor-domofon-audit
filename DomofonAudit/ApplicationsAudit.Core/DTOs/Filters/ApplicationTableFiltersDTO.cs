namespace ApplicationsAudit.Core.DTOs.Filters
{
    public class ApplicationTableFiltersDTO
    {
        public (string sortField, int type)? Sort { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

namespace ApplicationsAudit.Core.DTOs
{
    public class ApplicationNewClient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        public string HomePhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string HomeNumber { get; set; }
        public string EntranceNumber { get; set; }
        public string FlatNumber { get; set; }
        public DistrictDTO District { get; set; }
    }
}

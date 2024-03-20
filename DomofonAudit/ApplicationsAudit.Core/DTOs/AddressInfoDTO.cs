namespace ApplicationsAudit.Core.DTOs
{
    public class AddressInfoDTO
    {
        public const int StreetMaxLength = 80;
        public const int CityMaxLength = 80;
        public const int RegionMaxLength = 80;
        public const int HomeNumberMaxLength = 99999;
        public const int EntranceNumberMaxLength = 99999;
        public const int FlatNumberMaxLength = 99999;
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string HomeNumber { get; set; }
        public string EntranceNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}

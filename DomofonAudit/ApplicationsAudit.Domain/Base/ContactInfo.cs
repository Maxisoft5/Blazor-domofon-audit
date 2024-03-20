using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationsAudit.Domain.Base
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Email { get; set; }
        public string? MobilePhone1 { get; set; }
        public string? MobilePhone2 { get; set; }
        public string? HomePhone { get; set; }
        public bool IsActive { get; set; }
        public byte[]? ProfileImage { get; set; }
        [NotMapped]
        public string FullName => FirstName + " " + LastName + " " + Patronymic;
    }
}

using Org.BouncyCastle.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationsAudit.Core.DTOs
{
    public class ContactInfoDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string MobilePhone1 { get; set; }
        public string MobilePhone2 { get; set; }
        public string HomePhone { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public byte[]? ProfileImage { get; set; }

        [NotMapped]
        public string FullName => LastName + " " + FirstName + " " + Patronymic;

    }
}

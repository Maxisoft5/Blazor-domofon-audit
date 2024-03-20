using ApplicationsAudit.Domain.Base;
using Common.Interfaces;

namespace ApplicationsAudit.DataAccess.Interfaces
{
    public interface IContactInfoDao : IBaseDao<ContactInfo>
    {
        public Task<ContactInfo> GetById(int id);
        public Task<ContactInfo> UploadProfileImage(byte[] image, ContactInfo contactInfo);
        public Task<ContactInfo?> UpdateContactInfo(int id, ContactInfo contactInfo);
        public Task DeleteCurrentProfileImage(ContactInfo info);
    }
}

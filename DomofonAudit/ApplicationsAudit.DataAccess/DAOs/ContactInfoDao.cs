using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Base;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ContactInfoDao : IContactInfoDao
    {
        private readonly DataContext _dataContext;
        public ContactInfoDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ContactInfo> Create(ContactInfo item)
        {
            await _dataContext.ContactInfos.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task DeleteCurrentProfileImage(ContactInfo contact)
        {
            contact.ProfileImage = null;
            await _dataContext.SaveChangesAsync();
        }

        public Task<IEnumerable<ContactInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ContactInfo> GetById(int id)
        {
            var info = await _dataContext.ContactInfos.FirstOrDefaultAsync(x => x.Id == id);
            return info;
        }

        public async Task<ContactInfo?> UpdateContactInfo(int id, ContactInfo contactInfo)
        {
            var exists = await _dataContext.ContactInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.FirstName = contactInfo.FirstName;
                exists.LastName = contactInfo.LastName;
                exists.Email = contactInfo.Email;
                exists.Patronymic = contactInfo.Patronymic;
                exists.HomePhone = contactInfo.HomePhone;
                exists.MobilePhone1 = contactInfo.MobilePhone1;
                exists.MobilePhone2 = contactInfo.MobilePhone2;
                _dataContext.ContactInfos.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }

        public async Task<ContactInfo> UploadProfileImage(byte[] image, ContactInfo contactInfo)
        {
            contactInfo.ProfileImage = image;
            await _dataContext.SaveChangesAsync();
            return contactInfo;
        }
    }
}

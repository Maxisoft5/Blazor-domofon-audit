using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Results;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Base;

namespace ApplicationsAudit.Core.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IContactInfoDao _contactInfoDao;
        public ContactInfoService(IContactInfoDao contactInfoDao)
        {
            _contactInfoDao = contactInfoDao;
        }
        public async Task<ContactInfoDTO> Create(ContactInfoDTO item)
        {
            var info = new ContactInfo()
            {
                CreatedDate = DateTime.UtcNow,
                Email = item.Email,
                FirstName = item.FirstName,
                HomePhone = item.HomePhone,
                IsActive = item.IsActive,
                LastName = item.LastName,
                MobilePhone1 = item.MobilePhone1,
                MobilePhone2 = item.MobilePhone2,
                Patronymic = item.Patronymic
            };
            var result = await _contactInfoDao.Create(info);
            return new ContactInfoDTO()
            {
                CreatedDate = result.CreatedDate,
                Email = result.Email,
                FirstName = result.FirstName,
                HomePhone = result.HomePhone,
                Id = result.Id,
                IsActive = result.IsActive,
                LastName= result.LastName,
                MobilePhone1 = result.MobilePhone1,
                MobilePhone2 = result.MobilePhone2,
                Patronymic = result.Patronymic
            };
        }

        public async Task DeleteCurrentProfileImage(int id)
        {
            var info = await _contactInfoDao.GetById(id);
            if (info is not null)
            {
                await _contactInfoDao.DeleteCurrentProfileImage(info);
            }
        }

        public Task<IEnumerable<ContactInfoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ContactInfoDTO> GetById(int id)
        {
            var info = await _contactInfoDao.GetById(id);
            return new ContactInfoDTO()
            {
                CreatedDate = info.CreatedDate,
                Email = info.Email,
                FirstName = info.FirstName,
                HomePhone = info.HomePhone,
                Id = id,
                IsActive = info.IsActive,
                LastName = info.LastName,
                MobilePhone1 = info.MobilePhone1,
                MobilePhone2 = info.MobilePhone2,
                Patronymic = info.Patronymic,
                ProfileImage = info.ProfileImage
            };
        }

        public async Task<UploadImageToManagerResult> UploadImageToManager(int contactInfoId,
            byte[] image,
            CancellationToken token)
        {
            var result = new UploadImageToManagerResult();
            var info = await _contactInfoDao.GetById(contactInfoId);
            if (info is null)
            {
                result.Success = false;
                result.ErrorMessage = $"Concact info was not found with id {contactInfoId}";
                return result;
            }
            info = await _contactInfoDao.UploadProfileImage(image, info);
            result.ContactInfo = new ContactInfoDTO()
            {
                ProfileImage = info.ProfileImage
            };
            result.Success = true;
            return result;
        }
    }
}

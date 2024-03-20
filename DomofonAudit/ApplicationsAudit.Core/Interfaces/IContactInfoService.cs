using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Results;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IContactInfoService : IBaseService<ContactInfoDTO>
    {
        public Task<UploadImageToManagerResult> UploadImageToManager(int contactInfoId, byte[] image,
            CancellationToken token);

        public Task DeleteCurrentProfileImage(int id);
    }
}

using ApplicationsAudit.Core.DTOs;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IAddressInfoService : IBaseService<AddressInfoDTO>
    {
        public Task<AddressInfoDTO> UpdateAddressInfo(int id,  AddressInfoDTO addressInfoDTO);
    }
}

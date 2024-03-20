using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Address;

namespace ApplicationsAudit.Core.Services
{
    public class AddressInfoService : IAddressInfoService
    {
        private readonly IAddressInfoDao _addressInfoDao;
        public AddressInfoService(IAddressInfoDao addressInfoDao)
        {
            _addressInfoDao = addressInfoDao;
        }

        public async Task<AddressInfoDTO> Create(AddressInfoDTO item)
        {
            var info = new AddressInfo()
            {
                City = item.City,
                EntranceNumber = string.IsNullOrEmpty(item.EntranceNumber) ? 0 : int.Parse(item.EntranceNumber),
                FlatNumber = string.IsNullOrEmpty(item.FlatNumber) ? 0 : int.Parse(item.FlatNumber),
                HomeNumber = item.HomeNumber,
                Region = item.Region,
                Street = item.Street
            };
            var result = await _addressInfoDao.Create(info);
            return new AddressInfoDTO()
            {
                City = result.City,
                EntranceNumber = item.EntranceNumber,
                FlatNumber = item.FlatNumber,
                HomeNumber = item.HomeNumber,
                Id = result.Id,
                Region = result.Region,
                Street = result.Street
            };
        }

        public Task<IEnumerable<AddressInfoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AddressInfoDTO> GetById(int id)
        {
            var info = await _addressInfoDao.GetById(id);
            return new AddressInfoDTO()
            {
                City = info.City,
                EntranceNumber = info.EntranceNumber.ToString(),
                FlatNumber = info.FlatNumber.ToString(),
                HomeNumber = info.HomeNumber.ToString(),
                Id = info.Id,
                Region= info.Region,
                Street= info.Street
            };
        }

        public async Task<AddressInfoDTO> UpdateAddressInfo(int id, AddressInfoDTO addressInfoDTO)
        {
            var address = new AddressInfo()
            {
                City = addressInfoDTO.City,
                EntranceNumber = string.IsNullOrWhiteSpace(addressInfoDTO.EntranceNumber) ? 0 
                : int.Parse(addressInfoDTO.EntranceNumber),
                FlatNumber = string.IsNullOrWhiteSpace(addressInfoDTO.FlatNumber) ? 0 
                : int.Parse(addressInfoDTO.FlatNumber),
                HomeNumber = addressInfoDTO.HomeNumber,
                Street = addressInfoDTO.Street,
                Region = addressInfoDTO.Region
            };
            await _addressInfoDao.UpdateAddressInfo(id, address);
            return addressInfoDTO;
        }
    }
}

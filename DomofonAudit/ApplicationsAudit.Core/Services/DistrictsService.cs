using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Districts;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Services
{
    public class DistrictsService : IDistrictsService
    {
        private readonly IDistrictsDao _districtsDao;
        public DistrictsService(IDistrictsDao districtsDao)
        {
            _districtsDao = districtsDao;
        }

        public async Task<DistrictDTO> Create(DistrictDTO item)
        {
            var district = new District()
            {
                City = item.City,
                Name = item.Name,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            var result = await _districtsDao.Create(district);
            return new DistrictDTO()
            {
                City = result.City,
                CreatedDate = result.CreatedDate,
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<IEnumerable<DistrictDTO>> GetAll()
        {
            var all = await _districtsDao.GetAll();
            var dto = new List<DistrictDTO>();
            foreach (var item in all)
            {
                dto.Add(new DistrictDTO()
                {
                    Id = item.Id,
                    City = item.City,
                    Name = item.Name
                });
            }
            return dto;
        }

        public async Task<DistrictDTO> GetById(int id)
        {
            var result = await _districtsDao.GetById(id);
            return new DistrictDTO()
            {
                City = result.City,
                CreatedDate = result.CreatedDate,
                Id = result.Id,
                Name = result.Name
            };
        }
      
    }
}

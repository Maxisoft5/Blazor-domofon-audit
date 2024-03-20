using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Managers;

namespace ApplicationsAudit.Core.Services
{
    public class ManagerInfoService : IManagerInfoService
    {
        private readonly IManagerInfoDao _managerInfoDao;
        public ManagerInfoService(IManagerInfoDao managerInfoDao)
        {
            _managerInfoDao = managerInfoDao;
        }

        public async Task<ManagerInfoDTO> Create(ManagerInfoDTO item)
        {
            var info = new ManagerInfo()
            {
                StartedWorkAt = item.StartedWorkAt,
                ContactInfoId = item.ContactInfoId,
            };
            var saved = await _managerInfoDao.Create(info);
            item.Id = saved.Id;
            return item;
        }

        public Task<IEnumerable<ManagerInfoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ManagerInfoDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

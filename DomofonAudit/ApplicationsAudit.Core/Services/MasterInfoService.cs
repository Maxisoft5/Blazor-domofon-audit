using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;

namespace ApplicationsAudit.Core.Services
{
    public class MasterInfoService : IMasterInfoService
    {

        public Task<ManagerInfoDTO> Create(ManagerInfoDTO item)
        {
            throw new NotImplementedException();
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

using ApplicationsAudit.Core.DTOs;
using Common.Interfaces;

namespace ApplicationsAudit.Core.Interfaces
{
    public interface IMasterService : IBaseService<MasterDTO>
    {
        public Task<MasterDTO> Create(MasterDTO item, IEnumerable<int> districtIds);
    }
}

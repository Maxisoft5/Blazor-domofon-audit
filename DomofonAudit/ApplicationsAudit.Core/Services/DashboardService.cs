using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;

namespace ApplicationsAudit.Core.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IApplicationsDao _applicationsDao;
        public DashboardService(IApplicationsDao applicationsDao)
        {
            _applicationsDao = applicationsDao;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsAsync()
        {
            var applications = await _applicationsDao.GetAllForDashboard();
            var result = new List<ApplicationDTO>();
            foreach (var application in applications)
            {
                result.Add(new ApplicationDTO()
                {
                    Id = application.Id,
                    Description = application.Description
                });
            }
            return result;
        }
    }
}

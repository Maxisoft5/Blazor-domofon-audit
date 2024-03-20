using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Applications;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ApplicationsDao : IApplicationsDao
    {
        private readonly DataContext _dataContext;
        public ApplicationsDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Application> Create(Application application)
        {
            await _dataContext.Applications.AddAsync(application);
            await _dataContext.SaveChangesAsync();
            return application;
        }

        public async Task<IEnumerable<Application>> GetAllForDashboard()
        {
            var applications = await _dataContext.Applications
                .Include(x => x.AssignedManager).ThenInclude(m => m.ManagerInfo)
                    .ThenInclude(x => x.ContactInfo)
                .Include(x => x.AssignedMaster).ThenInclude(x => x.MasterInfo)
                    .ThenInclude(x => x.ContactInfo)
                .Include(x => x.Address).ThenInclude(x => x.Address)
                .Include(x => x.Address).ThenInclude(x => x.District)
                .Include(x => x.RequestedBy).ThenInclude(x => x.ContactInfo)
                .Select(x => new Application()
                {
                    Id = x.Id,
                    Address = new ApplicationAddress()
                    {
                        Address = new Domain.Address.AddressInfo()
                        {
                            Street = x.Address.Address.Street,
                        },
                        District = new Domain.Districts.District()
                        {
                            Name = x.Address.District.Name,
                        }
                    },
                    AssignedManager = new Domain.Managers.Manager()
                    {
                        ManagerInfo = new Domain.Managers.ManagerInfo()
                        {
                            ContactInfo = new Domain.Base.ContactInfo()
                            {
                                FirstName = x.AssignedManager.ManagerInfo.ContactInfo.FirstName,
                                LastName = x.AssignedManager.ManagerInfo.ContactInfo.LastName
                            }
                        }
                    },
                    AssignedMaster = new Domain.Masters.Master()
                    {
                        MasterInfo = new Domain.Masters.MasterInfo()
                        {
                            ContactInfo = new Domain.Base.ContactInfo()
                            {
                                FirstName = x.AssignedManager.ManagerInfo.ContactInfo.FirstName,
                                LastName = x.AssignedManager.ManagerInfo.ContactInfo.LastName
                            }
                        }
                    },
                    RequestedBy = new Domain.Clients.Client()
                    {
                        ContactInfo = new Domain.Base.ContactInfo()
                        {
                            FirstName = x.RequestedBy.ContactInfo.FirstName,
                            LastName = x.RequestedBy.ContactInfo.LastName
                        }
                    },
                    ResolvedDate = x.ResolvedDate,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    LastUpdateAt = x.LastUpdateAt,
                    Status = x.Status,
                    Title = x.Title
                }).AsNoTracking().ToListAsync();

            return applications;
        }

        public async Task<int> GetCount(ApplicationsFiltersSorts? managersFilters = null)
        {
            var queryCount = _dataContext.Applications
                .Include(x => x.AssignedManager)
                  .ThenInclude(x => x.ManagerInfo)
                  .ThenInclude(x => x.ContactInfo)
                .Include(x => x.AssignedMaster)
                  .ThenInclude(x => x.MasterInfo)
                  .ThenInclude(x => x.ContactInfo)
                .Include(x => x.RequestedBy)
                  .ThenInclude(x => x.ContactInfo)
                .AsQueryable();

            queryCount = GetFiltered(queryCount, managersFilters);

            return await queryCount.CountAsync();
        }

        private IQueryable<Application> GetFiltered(IQueryable<Application> query, ApplicationsFiltersSorts? managersFilters = null)
        {

            //if (managersFilters?.Sort != null)
            //{
            //    var orderedField = _managersFilters[managersFilters.Sort.Value.sortField];
            //    if (managersFilters.Sort.Value.type == (int)SortDirection.Ascending)
            //    {
            //        query = query.OrderBy(orderedField).AsQueryable();
            //    }
            //    if (managersFilters.Sort.Value.type == (int)SortDirection.Descending)
            //    {
            //        query = query.OrderByDescending(orderedField).AsQueryable();
            //    }
            //}

            return query;
        }

        public async Task<IEnumerable<Application>> GetForTable(ApplicationsFiltersSorts filtersSorts)
        {
            var applications = await _dataContext.Applications.Include(x => x.Address).ThenInclude(x => x.Address)
                .Include(x => x.Address).ThenInclude(x => x.District)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.AssignedManager).ThenInclude(x => x.WorkingTime)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerRole)
                .Include(x => x.AssignedMaster).ThenInclude(x => x.MasterInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.RequestedBy).ThenInclude(x => x.ContactInfo).ToListAsync();

            return applications;
        }

        public async Task<IEnumerable<Application>> GetApplications()
        {
            var query = _dataContext.Applications.Include(x => x.Address).ThenInclude(x => x.Address)
                .Include(x => x.Address).ThenInclude(x => x.District)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.AssignedManager).ThenInclude(x => x.WorkingTime)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerRole)
                .Include(x => x.AssignedMaster).ThenInclude(x => x.MasterInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.RequestedBy).ThenInclude(x => x.ContactInfo).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task MoveApplication(ApplicationStatus status, Guid appId, int index)
        {
            var applicationAfterIndex = _dataContext
                .Applications.Where(x => x.PositionIndexInColumn > index 
                    && x.Status == status);

            foreach (var application in applicationAfterIndex)
            {
                application.PositionIndexInColumn++;
            }

            var currentApplication = _dataContext.Applications
                .FirstOrDefault(x => x.Id == appId);
            if (currentApplication != null)
            {
                var previousColumnApplications = _dataContext.Applications
                    .Where(x => x.Status == currentApplication.Status
                    && x.PositionIndexInColumn > currentApplication.PositionIndexInColumn);

                foreach (var previousApplication in previousColumnApplications)
                {
                    previousApplication.PositionIndexInColumn--;
                }

                currentApplication.PositionIndexInColumn = index;
                currentApplication.Status = status;
                await _dataContext.SaveChangesAsync();
            }

        }
        public async Task<Application> GetById(Guid id)
        {
            var app = await _dataContext.Applications.Include(x => x.Address).ThenInclude(x => x.Address)
                .Include(x => x.Address).ThenInclude(x => x.District)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.AssignedManager).ThenInclude(x => x.WorkingTime)
                .Include(x => x.AssignedManager).ThenInclude(x => x.ManagerRole)
                .Include(x => x.AssignedMaster).ThenInclude(x => x.MasterInfo).ThenInclude(x => x.ContactInfo)
                .Include(x => x.RequestedBy).ThenInclude(x => x.ContactInfo)
                .FirstOrDefaultAsync(x => x.Id == id);

            return app;
        }

        public async Task<Application?> UpdateApplication(Guid id, Application application)
        {
            var exists = await _dataContext.Applications.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.Status = application.Status;
                exists.ResolvedDate = application.ResolvedDate;
                exists.Description = application.Description;
                exists.Title = application.Title;
                exists.RequestedById = application.RequestedById;
                exists.AssignedMasterId = application.AssignedMasterId;
                exists.AssignedManagerId = application.AssignedManagerId;
                exists.ApplicationAddressId = application.ApplicationAddressId;
                exists.Comments = application.Comments;
                exists.PositionIndexInColumn = application.PositionIndexInColumn;
                exists.LastUpdateAt = DateTime.UtcNow;
                _dataContext.Applications.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }
    }
}

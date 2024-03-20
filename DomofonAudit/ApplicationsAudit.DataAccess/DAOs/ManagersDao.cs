using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Enums;
using ApplicationsAudit.Domain.Managers;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsAudit.DataAccess.DAOs
{
    public class ManagersDao : IManagersDao
    {
        private readonly DataContext _dataContext;
        private readonly Dictionary<string, Func<Manager, object>> _managersFilters = new Dictionary<string, Func<Manager, object>>()
        {
            { nameof(Manager.ManagerInfo.ContactInfo.FirstName).ToLower(), x => x.ManagerInfo.ContactInfo.FirstName },
            { nameof(Manager.ManagerInfo.ContactInfo.LastName).ToLower(), x => x.ManagerInfo.ContactInfo.LastName }
        };
        public ManagersDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Manager> GetById(Guid id)
        {
            var manager = await _dataContext.Managers.Include(x => x.WorkingTime).Include(x => x.ManagerInfo)
                .ThenInclude(x => x.ContactInfo)
                .Include(x => x.ManagerRole)
                .FirstOrDefaultAsync(x => x.Id == id);

            return manager;
        }

        public async Task<int> GetCount(ManagersFiltersSorts? managersFilters = null)
        {
            var queryCount = _dataContext.Managers.Include(x => x.ManagerInfo)
               .ThenInclude(x => x.ContactInfo)
               .Include(x => x.ManagerRole).AsQueryable();

            queryCount = GetFiltered(queryCount, managersFilters);

            return await queryCount.CountAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagersAsync(ManagersFiltersSorts? managersFilters = null)
        {
            var query = _dataContext.Managers.Include(x => x.ManagerInfo)
                .ThenInclude(x => x.ContactInfo)
                .Include(x => x.ManagerRole).AsQueryable();

            query = GetFiltered(query, managersFilters);

            var managers = await query.Take(managersFilters.Take).Skip(managersFilters.Skip)
                .AsNoTracking()
                .ToListAsync();


            return managers;
        }

        public async Task<IEnumerable<Manager>> GetManagersForDropDown()
        {
            var managers = await _dataContext.Managers.Include(x => x.ManagerInfo)
                .ThenInclude(x => x.ContactInfo).ToListAsync();

            return managers;
        }

        public async Task<Manager?> UpdateManager(Guid id, Manager manager)
        {
            var exists = await _dataContext.Managers.FirstOrDefaultAsync(x => x.Id == id);
            if (exists != null)
            {
                exists.LockoutEnd = manager.LockoutEnd;
                exists.LockoutEnabled = manager.LockoutEnabled;
                exists.ManagerInfoId = manager.ManagerInfoId;
                exists.Email = manager.Email;
                exists.ManagerRoleId = manager.ManagerRoleId;
                exists.WorkingTimeId = manager.WorkingTimeId;
                _dataContext.Managers.Update(exists);
                await _dataContext.SaveChangesAsync();
                return exists;
            }
            return null;
        }

        private IQueryable<Manager> GetFiltered(IQueryable<Manager> query, ManagersFiltersSorts? managersFilters = null)
        {
            if (!string.IsNullOrEmpty(managersFilters?.FirstName))
            {
                query = query.Where(x => x.ManagerInfo.ContactInfo.FirstName.Contains(managersFilters.FirstName));
            }

            if (!string.IsNullOrEmpty(managersFilters?.LastName))
            {
                query = query.Where(x => x.ManagerInfo.ContactInfo.LastName.Contains(managersFilters.LastName));
            }

            if (!string.IsNullOrEmpty(managersFilters?.Email))
            {
                query = query.Where(x => x.ManagerInfo.ContactInfo.Email.Contains(managersFilters.Email));
            }

            if (!string.IsNullOrEmpty(managersFilters?.PhoneNumber1))
            {
                query = query.Where(x => x.ManagerInfo.ContactInfo.MobilePhone1.Contains(managersFilters.PhoneNumber1));
            }

            if (!string.IsNullOrEmpty(managersFilters?.PhoneNumber2))
            {
                query = query.Where(x => x.ManagerInfo.ContactInfo.MobilePhone2.Contains(managersFilters.PhoneNumber2));
            }

            if (!string.IsNullOrEmpty(managersFilters?.Role))
            {
                query = query.Where(x => x.ManagerRole.Name == managersFilters.Role);
            }

            if (managersFilters?.Sort != null)
            {
                var orderedField = _managersFilters[managersFilters.Sort.Value.sortField];
                if (managersFilters.Sort.Value.type == (int)SortDirection.Ascending)
                {
                    query = query.OrderBy(orderedField).AsQueryable();
                }
                if (managersFilters.Sort.Value.type == (int)SortDirection.Descending)
                {
                    query = query.OrderByDescending(orderedField).AsQueryable();
                }
            }

            return query;
        }
    }
}

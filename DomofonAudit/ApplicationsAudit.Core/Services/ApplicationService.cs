using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Enums;
using ApplicationsAudit.Core.DTOs.Filters;
using ApplicationsAudit.Core.DTOs.Results;
using ApplicationsAudit.Core.Helpers;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Applications;
using Application = ApplicationsAudit.Domain.Applications.Application;

namespace ApplicationsAudit.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationsDao _applicationsDao;
        private readonly IAddressInfoDao _addressInfoDao;
        public ApplicationService(IApplicationsDao applicationsDao, IAddressInfoDao addressInfoDao)
        {
            _addressInfoDao = addressInfoDao;
            _applicationsDao = applicationsDao;
        }

        public async Task<ApplicationDTO> Create(ApplicationDTO application)
        {
            var address = new AddressInfo()
            {
                City = application.Address.Address.City,
                Region = application.Address.Address.Region,
                EntranceNumber = application.Address.Address.EntranceNumber,
                FlatNumber = application.Address.Address.FlatNumber,
                HomeNumber = application.Address.Address.HomeNumber,
                Street = application.Address.Address.Street
            };
            var createdAddress = await _addressInfoDao.Create(address);
            var appAddress = new ApplicationAddress()
            {
                AddressId = createdAddress.Id,
                DistrictId = application.Address.DistrictId
            };
            var createdAppAddress = await _addressInfoDao.Create(appAddress);

            var comments = new List<ApplicationComment>();
            foreach (var comment in application.Comments)
            {
                comments.Add(new ApplicationComment()
                {
                    AddedAt = DateTime.UtcNow,
                    Comment = comment.Comment,
                    CreatedBy = comment.CreatedBy
                });
            }

            var result = await _applicationsDao.Create(new Application()
            {
                ApplicationAddressId = createdAppAddress.Id,
                AssignedManagerId = application.AssignedManager.Id,
                AssignedMasterId = application.AssignedMaster.Id,
                RequestedById = application.RequestedBy.Id,
                Description = application.Description,
                Title = application.Title,
                CreatedAt = DateTime.UtcNow,
                PositionIndexInColumn = 0,
                Status = ApplicationStatus.ReadyForAssign,
                Comments = comments
            });

            application.Id = result.Id;

            return application;
        }

        public async Task<ApplicationsTableResult> GetForTable(ApplicationTableFiltersDTO applicationTableFilters)
        {
            var applications = await _applicationsDao.GetForTable(new ApplicationsFiltersSorts());
            var dtoResult = new List<ApplicationDTO>();

            foreach (var application in applications)
            {
                dtoResult.Add(new ApplicationDTO()
                {
                    Id = application.Id,
                    Title = application.Title,
                    Description = application.Description,
                    CreatedAt = application.CreatedAt,
                    Status = application.Status,
                    ResolvedDate = application.ResolvedDate,
                    LastUpdateAt = application.LastUpdateAt,
                    Address = application.Address,
                    AssignedManager = new ManagerDTO()
                    {
                        Id = application.AssignedManager.Id,
                        RoleId = application.AssignedManager.ManagerRoleId,
                        WorkingTime = new WorkingTimeDTO()
                        {
                            Monday = application.AssignedManager.WorkingTime.Monday,
                            Tuesday = application.AssignedManager.WorkingTime.Tuesday,
                            Wednesday = application.AssignedManager.WorkingTime.Wednesday,
                            Thursday = application.AssignedManager.WorkingTime.Thursday,
                            Friday = application.AssignedManager.WorkingTime.Friday,
                            Saturday = application.AssignedManager.WorkingTime.Saturday,
                            Sunday = application.AssignedManager.WorkingTime.Sunday,
                            IsTimeActive = application.AssignedManager.WorkingTime.IsTimeActive
                        },
                        CreatedAt = application.AssignedManager.CreatedAt,
                        ManagerInfo = new ManagerInfoDTO()
                        {
                            StartedWorkAt = application.AssignedManager.ManagerInfo.StartedWorkAt,
                            AdditionalYearlySalary = application.AssignedManager.ManagerInfo.AdditionalYearlySalary,
                            FiredDate = application.AssignedManager.ManagerInfo.FiredDate,
                            MonthlySalary = application.AssignedManager.ManagerInfo.MonthlySalary,
                            IsFired = application.AssignedManager.ManagerInfo.IsFired,
                            ContactInfo = new ContactInfoDTO()
                            {
                                Email = application.AssignedManager.ManagerInfo.ContactInfo.Email,
                                FirstName = application.AssignedManager.ManagerInfo.ContactInfo.FirstName,
                                LastName = application.AssignedManager.ManagerInfo.ContactInfo.LastName,
                                CreatedDate = application.AssignedManager.ManagerInfo.ContactInfo.CreatedDate,
                                HomePhone = application.AssignedManager.ManagerInfo.ContactInfo.HomePhone,
                                MobilePhone1 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone1,
                                MobilePhone2 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone2,
                                Patronymic = application.AssignedManager.ManagerInfo.ContactInfo.Patronymic
                            }
                        },
                        ManagerRole = new ManagerRoleDTO()
                        {
                            Role = EnumHelper.GetEnumValueFromDescription<Roles>(application.AssignedManager.ManagerRole.Name)
                        }
                    }
                });
            }

            return new ApplicationsTableResult()
            {
                Applications = dtoResult,
            };
        }

        public async Task<int> GetApplicationsCount(ApplicationTableFiltersDTO filtersSort)
        {
            var count = await _applicationsDao.GetCount(new ApplicationsFiltersSorts());

            return count;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplications()
        {
            var applications = await _applicationsDao.GetApplications();
            var dtoResult = new List<ApplicationDTO>();

            foreach (var application in applications)
            {
                dtoResult.Add(new ApplicationDTO()
                {
                    Id = application.Id,
                    Title = application.Title,
                    Description = application.Description,
                    CreatedAt = application.CreatedAt,
                    Status = application.Status,
                    ResolvedDate = application.ResolvedDate,
                    LastUpdateAt = application.LastUpdateAt,
                    Address = application.Address,
                    AssignedManager = new ManagerDTO()
                    {
                        Id = application.AssignedManager.Id,
                        RoleId = application.AssignedManager.ManagerRoleId,
                        WorkingTime = new WorkingTimeDTO()
                        {
                            Monday = application.AssignedManager.WorkingTime.Monday,
                            Tuesday = application.AssignedManager.WorkingTime.Tuesday,
                            Wednesday = application.AssignedManager.WorkingTime.Wednesday,
                            Thursday = application.AssignedManager.WorkingTime.Thursday,
                            Friday = application.AssignedManager.WorkingTime.Friday,
                            Saturday = application.AssignedManager.WorkingTime.Saturday,
                            Sunday = application.AssignedManager.WorkingTime.Sunday,
                            IsTimeActive = application.AssignedManager.WorkingTime.IsTimeActive
                        },
                        CreatedAt = application.AssignedManager.CreatedAt,
                        ManagerInfo = new ManagerInfoDTO()
                        {
                            StartedWorkAt = application.AssignedManager.ManagerInfo.StartedWorkAt,
                            AdditionalYearlySalary = application.AssignedManager.ManagerInfo.AdditionalYearlySalary,
                            FiredDate = application.AssignedManager.ManagerInfo.FiredDate,
                            MonthlySalary = application.AssignedManager.ManagerInfo.MonthlySalary,
                            IsFired = application.AssignedManager.ManagerInfo.IsFired,
                            ContactInfo = new ContactInfoDTO()
                            {
                                Email = application.AssignedManager.ManagerInfo.ContactInfo.Email,
                                FirstName = application.AssignedManager.ManagerInfo.ContactInfo.FirstName,
                                LastName = application.AssignedManager.ManagerInfo.ContactInfo.LastName,
                                CreatedDate = application.AssignedManager.ManagerInfo.ContactInfo.CreatedDate,
                                HomePhone = application.AssignedManager.ManagerInfo.ContactInfo.HomePhone,
                                MobilePhone1 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone1,
                                MobilePhone2 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone2,
                                Patronymic = application.AssignedManager.ManagerInfo.ContactInfo.Patronymic
                            }
                        },
                        ManagerRole = new ManagerRoleDTO()
                        {
                            Role = EnumHelper.GetEnumValueFromDescription<Roles>(application.AssignedManager.ManagerRole.Name)
                        }
                    }
                });
            }
            return dtoResult;
        }

        public async Task MoveApplication(ApplicationStatus status, Guid appId, int index)
        {
            await _applicationsDao.MoveApplication(status, appId, index);
        }

        public async Task<ApplicationDTO> GetById(Guid id)
        {
            var application = await _applicationsDao.GetById(id);
            var dto = new ApplicationDTO()
            {
                Id = application.Id,
                Title = application.Title,
                Description = application.Description,
                CreatedAt = application.CreatedAt,
                Status = application.Status,
                ResolvedDate = application.ResolvedDate,
                LastUpdateAt = application.LastUpdateAt,
                Address = application.Address,
                AddressId = application.ApplicationAddressId,
                ManagerId = application.AssignedManagerId,
                ClientId = application.RequestedById,
                MasterId = application.AssignedMasterId,
                AssignedManager = new ManagerDTO()
                {
                    Id = application.AssignedManager.Id,
                    RoleId = application.AssignedManager.ManagerRoleId,
                    WorkingTime = new WorkingTimeDTO()
                    {
                        Monday = application.AssignedManager.WorkingTime.Monday,
                        Tuesday = application.AssignedManager.WorkingTime.Tuesday,
                        Wednesday = application.AssignedManager.WorkingTime.Wednesday,
                        Thursday = application.AssignedManager.WorkingTime.Thursday,
                        Friday = application.AssignedManager.WorkingTime.Friday,
                        Saturday = application.AssignedManager.WorkingTime.Saturday,
                        Sunday = application.AssignedManager.WorkingTime.Sunday,
                        IsTimeActive = application.AssignedManager.WorkingTime.IsTimeActive
                    },
                    CreatedAt = application.AssignedManager.CreatedAt,
                    ManagerInfo = new ManagerInfoDTO()
                    {
                        StartedWorkAt = application.AssignedManager.ManagerInfo.StartedWorkAt,
                        AdditionalYearlySalary = application.AssignedManager.ManagerInfo.AdditionalYearlySalary,
                        FiredDate = application.AssignedManager.ManagerInfo.FiredDate,
                        MonthlySalary = application.AssignedManager.ManagerInfo.MonthlySalary,
                        IsFired = application.AssignedManager.ManagerInfo.IsFired,
                        ContactInfo = new ContactInfoDTO()
                        {
                            Email = application.AssignedManager.ManagerInfo.ContactInfo.Email,
                            FirstName = application.AssignedManager.ManagerInfo.ContactInfo.FirstName,
                            LastName = application.AssignedManager.ManagerInfo.ContactInfo.LastName,
                            CreatedDate = application.AssignedManager.ManagerInfo.ContactInfo.CreatedDate,
                            HomePhone = application.AssignedManager.ManagerInfo.ContactInfo.HomePhone,
                            MobilePhone1 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone1,
                            MobilePhone2 = application.AssignedManager.ManagerInfo.ContactInfo.MobilePhone2,
                            Patronymic = application.AssignedManager.ManagerInfo.ContactInfo.Patronymic
                        }
                    },
                    ManagerRole = new ManagerRoleDTO()
                    {
                        Role = EnumHelper.GetEnumValueFromDescription<Roles>(application.AssignedManager.ManagerRole.Name)
                    }
                }
            };

            if (application.AssignedMaster != null)
            {
                dto.AssignedMaster = new MasterDTO()
                {
                    Id = application.AssignedMaster.Id,
                    CreatedAt = application.AssignedMaster.CreatedAt,
                    MasterInfo = new MasterInfoDTO()
                    {
                        ContactInfo = new ContactInfoDTO()
                        {
                            FirstName = application.AssignedMaster.MasterInfo?.ContactInfo?.FirstName ?? "",
                            LastName = application.AssignedMaster.MasterInfo?.ContactInfo?.LastName ?? "",
                            Patronymic = application.AssignedMaster.MasterInfo?.ContactInfo?.Patronymic ?? ""
                        }
                    }
                };
            }

            if (application.RequestedBy != null)
            {
                dto.RequestedBy = new ClientDTO()
                {
                    Id = application.RequestedBy.Id,
                    AddedDate = application.RequestedBy.AddedDate,
                    ContactInfo = new ContactInfoDTO()
                    {
                        FirstName = application.RequestedBy.ContactInfo.FirstName ?? "",
                        LastName = application.RequestedBy.ContactInfo.LastName ?? "",
                        Patronymic = application.RequestedBy.ContactInfo.Patronymic ?? ""
                    }
                };
            }
            return dto;
        }

        public List<DropDownValue<ApplicationStatus>> GetApplicationStatusesDropdown()
        {
            var statuses = Enum.GetValues<ApplicationStatus>();
            var result = new List<DropDownValue<ApplicationStatus>>();
            foreach (var status in statuses)
            {
                result.Add(new DropDownValue<ApplicationStatus>()
                {
                    Value = status,
                    Label = status.ToString()
                });
            }
            return result;
        }

        public async Task<ApplicationDTO> Update(Guid id, ApplicationDTO applicationDto)
        {
            var application = new Application()
            {
                Status = applicationDto.Status,
                ApplicationAddressId = applicationDto.AddressId,
                Description = applicationDto.Description,
                AssignedManagerId = applicationDto.ManagerId,
                AssignedMasterId = applicationDto.MasterId,
                ResolvedDate = GetApplicationResolveDate(applicationDto.Status, applicationDto.ResolvedDate),
                LastUpdateAt = DateTime.UtcNow,
                RequestedById = applicationDto.ClientId,
                Title = applicationDto.Title
            };

            await _applicationsDao.UpdateApplication(id, application);

            return applicationDto;
        }

        private DateTime? GetApplicationResolveDate(ApplicationStatus status, DateTime? currentValue)
        {
            if ((status == ApplicationStatus.Resolved
                    || status == ApplicationStatus.Rejected)
                  && !currentValue.HasValue)
            {
                return DateTime.UtcNow;
            }

            if (status == ApplicationStatus.Rejected
                 && !currentValue.HasValue)
            {
                return DateTime.UtcNow;
            }

            if ((status != ApplicationStatus.Resolved && status != ApplicationStatus.Rejected)
                && currentValue.HasValue)
            {
                return null;
            }

            return null;
        }
    }
}

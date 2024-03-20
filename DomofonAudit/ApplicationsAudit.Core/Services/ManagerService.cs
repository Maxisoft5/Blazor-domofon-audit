using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.DTOs.Enums;
using ApplicationsAudit.Core.DTOs.Results;
using ApplicationsAudit.Core.Helpers;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;
using ApplicationsAudit.Domain.Managers;
using Common.Interfaces;
using EnumsNET;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Security.Claims;
using ManagersFiltersSortsDTO = ApplicationsAudit.Core.DTOs.Filters.ManagersFiltersSortsDTO;

namespace ApplicationsAudit.Core.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IRoleManagerDao _roleManagerDao;
        private readonly IManagersDao _managersDao;
        private readonly UserManager<Manager> _userManager;
        private readonly SignInManager<Manager> _signInManager;
        private readonly IContactInfoService _contactInfoService;
        private readonly IWorkingTimeService _workingTimeService;
        private readonly IManagerInfoService _managerInfoService;
        private readonly IEmailService _emailService;

        public ManagerService(IRoleManagerDao roleManagerDao, IManagersDao managersDao, UserManager<Manager> userManager,
            SignInManager<Manager> signInManager, IContactInfoService contactInfoService,
            IEmailService emailService,
            IWorkingTimeService workingTimeService, IManagerInfoService managerInfoService)
        {
            _roleManagerDao = roleManagerDao;
            _managersDao = managersDao;
            _userManager = userManager;
            _signInManager = signInManager;
            _contactInfoService = contactInfoService;
            _workingTimeService = workingTimeService;
            _managerInfoService = managerInfoService;
            _emailService = emailService;
        }

        public async Task<Manager?> GetManagerIdentity(ClaimsPrincipal currentUser)
        {
            if (currentUser == null)
            {
                return null;
            }

            return await _userManager.GetUserAsync(currentUser);
        }

        public async Task<ManagerDTO> GetById(Guid id)
        {
            var manager = await _managersDao.GetById(id);
            var dto = new ManagerDTO()
            {
                Id = id,
                ManagerInfo = new ManagerInfoDTO()
                {
                    StartedWorkAt = manager.ManagerInfo.StartedWorkAt,
                    AdditionalYearlySalary = manager.ManagerInfo.AdditionalYearlySalary,
                    FiredDate = manager.ManagerInfo.FiredDate,
                    MonthlySalary = manager.ManagerInfo.MonthlySalary,
                    IsFired = manager.ManagerInfo.IsFired,
                    ContactInfo = new ContactInfoDTO()
                    {
                        Id = manager.ManagerInfo.ContactInfo.Id,
                        Email = manager.ManagerInfo.ContactInfo.Email,
                        FirstName = manager.ManagerInfo.ContactInfo.FirstName,
                        LastName = manager.ManagerInfo.ContactInfo.LastName,
                        CreatedDate = manager.ManagerInfo.ContactInfo.CreatedDate,
                        HomePhone = manager.ManagerInfo.ContactInfo.HomePhone,
                        MobilePhone1 = manager.ManagerInfo.ContactInfo.MobilePhone1,
                        MobilePhone2 = manager.ManagerInfo.ContactInfo.MobilePhone2,
                        Patronymic = manager.ManagerInfo.ContactInfo.Patronymic,
                        ProfileImage = manager.ManagerInfo.ContactInfo.ProfileImage
                    }
                },
                WorkingTime = new WorkingTimeDTO()
                {
                    Monday = manager.WorkingTime.Monday,
                    Tuesday = manager.WorkingTime.Tuesday,
                    Wednesday = manager.WorkingTime.Wednesday,
                    Thursday = manager.WorkingTime.Thursday,
                    Friday = manager.WorkingTime.Friday,
                    Saturday = manager.WorkingTime.Saturday,
                    Sunday = manager.WorkingTime.Sunday,
                    IsTimeActive = manager.WorkingTime.IsTimeActive
                },
                ManagerRole = new ManagerRoleDTO()
                {
                    Id = manager.ManagerRole.Id,
                    Role = EnumHelper.GetEnumValueFromDescription<Roles>(manager.ManagerRole.Name ?? ""),
                    RoleName = manager.ManagerRole.Name,
                },
            };
            return dto;
        }

        public async Task<Manager?> GetIfAuthorized(ClaimsPrincipal currentUser)
        {
            if (currentUser != null &&
               this._signInManager.IsSignedIn(currentUser))
            {
                var user = await this.GetManagerIdentity(currentUser);
                if (user != null)
                {
                    var manager = await _managersDao.GetById(user.Id);
                    if (manager != null)
                    {
                        return manager;
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<ManagerRoleDTO>> GetRolesForDropDown()
        {
            var roles = await _roleManagerDao.GetRolesFoDropDown();
            var result = new List<ManagerRoleDTO>();
            foreach (var role in roles)
            {
                result.Add(new ManagerRoleDTO()
                {
                    Id = role.Id,
                    Role = EnumHelper.GetEnumValueFromDescription<Roles>(role.Name)
                });
            }
            return result;
        }

        public async Task<ManagerDTO> Register(ManagerDTO managerDTO)
        {
            var info = new ContactInfoDTO()
            {
                Email = managerDTO.ManagerInfo.ContactInfo.Email,
                FirstName = managerDTO.ManagerInfo.ContactInfo.FirstName,
                LastName = managerDTO.ManagerInfo.ContactInfo.LastName,
                MobilePhone1 = managerDTO.ManagerInfo.ContactInfo.MobilePhone1,
                CreatedDate = DateTime.UtcNow,
                Patronymic = managerDTO.ManagerInfo.ContactInfo.Patronymic,
                MobilePhone2 = managerDTO.ManagerInfo.ContactInfo.MobilePhone2
            };
            var savedInfo = await _contactInfoService.Create(info);
            var managerInfo = new ManagerInfoDTO()
            {
                ContactInfoId = savedInfo.Id,
                StartedWorkAt = DateTime.UtcNow
            };
            var managerInfoSaved = await _managerInfoService.Create(managerInfo);
            var time = await _workingTimeService.GetByDays(managerDTO.WorkingTime);
            if (time == null)
            {
                time = await _workingTimeService.Create(managerDTO.WorkingTime);
            }

            var manager = new Manager()
            {
                WorkingTimeId = time.Id,
                ManagerRoleId = managerDTO.RoleId,
                Email = managerDTO.ManagerInfo.ContactInfo.Email,
                CreatedAt = DateTime.UtcNow,
                ManagerInfoId = managerInfoSaved.Id,
                UserName = managerDTO.ManagerInfo.ContactInfo.Email
            };
            var result = await _userManager.CreateAsync(manager);

            if (result.Succeeded)
            {
                var setPassword = await _userManager.AddPasswordAsync(manager, managerDTO.ManagerInfo.Password);

                var roleName = await _roleManagerDao.GetRoleNameById(manager.ManagerRoleId);

                var addRole = await _userManager.AddToRoleAsync(manager, roleName);
                if (setPassword.Succeeded && addRole.Succeeded)
                {
                    await _emailService.SendEmailAsync(managerDTO.ManagerInfo.ContactInfo.Email, "Данные для входа",
                         $"Email: {managerDTO.ManagerInfo.ContactInfo.Email}, Password: {managerDTO.ManagerInfo.Password}");
                }
            }
            managerDTO.ManagerInfoId = managerInfoSaved.Id;
            managerDTO.Id = manager.Id;
            return managerDTO;
        }

        public async Task<LoginResult> SignIn(ManagerDTO managerDTO)
        {
            var signInResult = new LoginResult();
            var manager = await _userManager.FindByEmailAsync(managerDTO.ManagerInfo.ContactInfo.Email);
            if (manager == null)
            {
                signInResult.Succeeded = false;
                signInResult.Message = $"Manager with email {managerDTO.ManagerInfo.ContactInfo.Email} was not found";
                return signInResult;
            }
            var checkPassword = await _signInManager.PasswordSignInAsync(manager, managerDTO.ManagerInfo.Password, false, false);
            signInResult.Succeeded = checkPassword.Succeeded;
            if (!checkPassword.Succeeded)
            {
                signInResult.Message = "Не правильный пароль";
            }
            signInResult.Manager = manager;
            return signInResult;
        }

        public async Task<List<ManagerDTO>> GetManagers(ManagersFiltersSortsDTO filtersSort)
        {
            var managers = new List<Manager>();
            var managersResult = await _managersDao.GetManagersAsync(new ManagersFiltersSorts()
            {
                Role = filtersSort.Role.HasValue ? (filtersSort.Role.Value).AsString(EnumFormat.Description) : "",
                FirstName = filtersSort.FirstName,
                LastName = filtersSort.LastName,
                Email = filtersSort.Email,
                PhoneNumber1 = filtersSort.PhoneNumber1,
                PhoneNumber2 = filtersSort.PhoneNumber2,
                Sort = filtersSort.Sort,
                Take = filtersSort.Take,
                Skip = filtersSort.Skip
            });
            managers.AddRange(managersResult);

            var result = new List<ManagerDTO>();

            foreach (var man in managers)
            {
                result.Add(new ManagerDTO()
                {
                    Id = man.Id,
                    ManagerInfo = new ManagerInfoDTO()
                    {
                        ContactInfo = new ContactInfoDTO()
                        {
                            FirstName = man.ManagerInfo.ContactInfo.FirstName,
                            LastName = man.ManagerInfo.ContactInfo.LastName,
                            Email = man.ManagerInfo.ContactInfo.Email,
                            MobilePhone1 = man.ManagerInfo.ContactInfo.MobilePhone1,
                            MobilePhone2 = man.ManagerInfo.ContactInfo.MobilePhone2
                        }
                    },
                    ManagerRole = new ManagerRoleDTO()
                    {
                        Role = EnumHelper.GetEnumValueFromDescription<Roles>(man.ManagerRole.Name)
                    }
                });
            }

            return result;
        }

        public async Task<int> GetManagersCount(ManagersFiltersSortsDTO filtersSort)
        {
            var count = await _managersDao.GetCount(new ManagersFiltersSorts()
            {
                Role = filtersSort.Role.HasValue ? (filtersSort.Role.Value).AsString(EnumFormat.Description) : "",
                FirstName = filtersSort.FirstName,
                LastName = filtersSort.LastName,
                Email = filtersSort.Email,
                PhoneNumber1 = filtersSort.PhoneNumber1,
                PhoneNumber2 = filtersSort.PhoneNumber2,
                Sort = filtersSort.Sort,
                Take = filtersSort.Take,
                Skip = filtersSort.Skip
            });

            return count;
        }

        public async Task<List<ManagerDTO>> GetForDropDown()
        {
            var managers = await _managersDao.GetManagersForDropDown();
            var list = managers.ToList();
            var result = new List<ManagerDTO>();
            foreach (var manager in list)
            {
                result.Add(new ManagerDTO()
                {
                    Id = manager.Id,
                    ManagerInfo = new ManagerInfoDTO()
                    {
                        ContactInfo = new ContactInfoDTO()
                        {
                            FirstName = manager.ManagerInfo.ContactInfo.FirstName,
                            LastName = manager.ManagerInfo.ContactInfo.LastName
                        }
                    }
                });
            }
            return result;
        }

        public async Task<SignOutResult> SignOut()
        {
            var result = new SignOutResult();
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}

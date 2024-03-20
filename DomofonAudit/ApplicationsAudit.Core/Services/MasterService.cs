using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.DataAccess.Interfaces;

namespace ApplicationsAudit.Core.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMastersDao _mastersDao;
        private readonly IMasterInfoDao _masterInfoDao;
        private readonly IContactInfoDao _contactInfoDao;
        private readonly IWorkingTimesDao _workingTimesDao;
        public MasterService(IMastersDao mastersDao, IMasterInfoDao masterInfoDao, IContactInfoDao contactInfoDao,
            IWorkingTimesDao workingTimesDao)
        {
            _mastersDao = mastersDao;
            _masterInfoDao = masterInfoDao;
            _workingTimesDao = workingTimesDao;
            _contactInfoDao = contactInfoDao;
        }

        public async Task<MasterDTO> Create(MasterDTO item, IEnumerable<int> districtIds)
        {
            var contact = await _contactInfoDao.Create(new Domain.Base.ContactInfo()
            {
                FirstName = item.MasterInfo.ContactInfo.FirstName,
                LastName = item.MasterInfo.ContactInfo.LastName,
                Patronymic = item.MasterInfo.ContactInfo.Patronymic,
                MobilePhone1 = item.MasterInfo.ContactInfo.MobilePhone1,
                MobilePhone2 = item.MasterInfo.ContactInfo.MobilePhone2,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            });
            var masterInfo = await _masterInfoDao.Create(new Domain.Masters.MasterInfo()
            {
                StartedWorkAt = DateTime.UtcNow,
                ContactInfoId = contact.Id
            });

            var time = await _workingTimesDao.Create(new Domain.WorkingTimes.WeeklyWorkingTime()
            {
                Wednesday = item.WorkingTime.Wednesday,
                IsTimeActive = true,
                Friday = item.WorkingTime.Friday,
                Monday = item.WorkingTime.Monday,
                Saturday = item.WorkingTime.Saturday,
                Sunday = item.WorkingTime.Sunday,
                Thursday = item.WorkingTime.Thursday,
                Tuesday = item.WorkingTime.Tuesday
            });

            var result = await _mastersDao.Create(new Domain.Masters.Master()
            {
                MasterInfoId = masterInfo.Id,
                WorkingTimeId = time.Id,
                CreatedAt = DateTime.UtcNow,
            });
            foreach (var districtId in districtIds)
            {
                await _mastersDao.AssignDistrict(result.Id, districtId);
            }
            return new MasterDTO {
                Id = result.Id, 
                MasterInfo = item.MasterInfo
            };
        }

        public Task<MasterDTO> Create(MasterDTO item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MasterDTO>> GetAll()
        {
            var masters = await _mastersDao.GetAll();
            var result = new List<MasterDTO>();
            foreach (var item in masters)
            {
                result.Add(new MasterDTO()
                {
                    Id = item.Id,
                    //WorkingTime = new WorkingTimeDTO()
                    //{
                    //    Wednesday = item.WorkingTime.Wednesday,
                    //    Id = item.WorkingTime.Id,
                    //    Friday = item.WorkingTime.Friday,
                    //    IsTimeActive = item.WorkingTime.IsTimeActive,
                    //    Monday = item.WorkingTime.Monday,
                    //    Saturday = item.WorkingTime.Saturday,
                    //    Sunday = item.WorkingTime.Sunday,
                    //    Thursday = item.WorkingTime.Thursday,
                    //    Tuesday = item.WorkingTime.Tuesday
                    //},
                    CreatedAt = item.CreatedAt,
                    MasterInfo = new MasterInfoDTO()
                    {
                        ContactInfo = new ContactInfoDTO()
                        {
                            CreatedDate = item.MasterInfo.ContactInfo.CreatedDate,
                            Email = item.MasterInfo.ContactInfo.Email,
                            FirstName = item.MasterInfo.ContactInfo.FirstName,
                            LastName = item.MasterInfo.ContactInfo.LastName,
                            Patronymic = item.MasterInfo.ContactInfo.Patronymic
                        }
                    }
                });
            }
            return result;
        }

        public Task<MasterDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

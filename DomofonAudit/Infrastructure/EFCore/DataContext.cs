using ApplicationsAudit.Domain.Address;
using ApplicationsAudit.Domain.Applications;
using ApplicationsAudit.Domain.Base;
using ApplicationsAudit.Domain.Clients;
using ApplicationsAudit.Domain.Districts;
using ApplicationsAudit.Domain.Managers;
using ApplicationsAudit.Domain.Masters;
using ApplicationsAudit.Domain.WorkingTimes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore
{
    public class DataContext : IdentityUserContext<Manager, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        {

        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationAddress> ApplicationAddresses { get; set; }
        public DbSet<ApplicationComment> ApplicationComments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ManagerInfo> ManagerInfos { get; set; }
        public DbSet<ManagerRole> ManagerRoles { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<MasterInfo> MasterInfos { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<MasterDistrict> MasterDistricts { get; set; }
        public DbSet<WeeklyWorkingTime> WeeklyWorkingTimes { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<AddressInfo> AddressInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>();



        }

    }
}

using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.Core.Services;
using ApplicationsAudit.Core.Services.Events;
using ApplicationsAudit.DataAccess.DAOs;
using ApplicationsAudit.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationsAudit.Core
{
    public static class ApplicationsDependencyInjection
    {
        public static void AddDashboard(this IServiceCollection services)
        {
            services.AddScoped<IApplicationsDao, ApplicationsDao>();
            services.AddScoped<IDistrictsDao, DistrictsDao>();
            services.AddScoped<IClientsDao, ClientsDao>();
            services.AddTransient<IContactInfoDao, ContactInfoDao>();
            services.AddScoped<IAddressInfoDao, AddressInfoDao>();
            services.AddScoped<IClientAddressDao, ClientAddressDao>();
            services.AddScoped<IMastersDao, MastersDao>();
            services.AddScoped<IMasterInfoDao, MasterInfoDao>();
            services.AddScoped<IWorkingTimesDao, WorkingTimesDao>();
            services.AddScoped<IManagersDao, ManagersDao>();

            services.AddScoped<IClientAddressService, ClientAddressService>();
            services.AddScoped<IAddressInfoService, AddressInfoService>();
            services.AddTransient<IContactInfoService, ContactInfoService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDistrictsService, DistrictsService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IApplicationService, ApplicationService>();

            services.AddSingleton<ApplicationEvents>();
            services.AddSingleton<CreateClientEvent>();
            services.AddSingleton<CreateDistrictEvent>();
            services.AddSingleton<CreateMasterEvent>();
            services.AddSingleton<CreateManagerEvent>();
            services.AddSingleton<SignInEvent>();
            services.AddScoped<UploadProfileImageEvent>();
        }

        public static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IRoleManagerDao, RoleManagerDao>();
            services.AddScoped<IManagerInfoDao, ManagerInfoDao>();
            services.AddScoped<IWorkingTimeService, WorkingTimeService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IManagerInfoService, ManagerInfoService>();
        }
    }
}

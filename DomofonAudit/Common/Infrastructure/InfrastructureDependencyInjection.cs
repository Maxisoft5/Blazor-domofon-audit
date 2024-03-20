using ApplicationsAudit.Domain.Managers;
using Common.Interfaces;
using Common.Services;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
               IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").GetChildren().AsEnumerable();
            var notesConnection = connectionStrings.FirstOrDefault(x => x.Key == "DefaultConnection");
            //services.AddDbContextFactory<DataContext>(options =>
            //{
            //    options.UseSqlServer(notesConnection.Value);
            //    options.EnableDetailedErrors();
            //    options.EnableSensitiveDataLogging();
            //}, lifetime: ServiceLifetime.Transient);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(notesConnection.Value);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }, contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);

            return services;
        }

        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void AddRoles(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var roleManager = provider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<ManagerRole>>();

            var roles = new string[] { "Администратор", "Диспетчер" };

            foreach (var role in roles)
            {
                var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();

                if (!roleExists)
                {
                    roleManager.CreateAsync(new ManagerRole()
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    }).GetAwaiter().GetResult();
                }
            }

        }
    }
}

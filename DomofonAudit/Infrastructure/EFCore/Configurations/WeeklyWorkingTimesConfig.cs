using ApplicationsAudit.Domain.WorkingTimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class WeeklyWorkingTimesConfig : IEntityTypeConfiguration<WeeklyWorkingTime>
    {
        public void Configure(EntityTypeBuilder<WeeklyWorkingTime> builder)
        {
        }
    }
}

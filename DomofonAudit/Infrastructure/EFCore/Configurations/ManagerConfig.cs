using ApplicationsAudit.Domain.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.ManagerRole);
            builder.HasOne(x => x.ManagerInfo);
            builder.HasOne(x => x.WorkingTime);
            builder.HasMany(x => x.Applications);
        }
    }
}

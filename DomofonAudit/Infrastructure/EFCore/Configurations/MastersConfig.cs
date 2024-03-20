using ApplicationsAudit.Domain.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class MastersConfig : IEntityTypeConfiguration<Master>
    {
        public void Configure(EntityTypeBuilder<Master> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.MasterInfo);
            builder.HasMany(x => x.MasterDistricts).WithOne(x => x.Master);
            builder.HasMany(x => x.Applications).WithOne(x => x.AssignedMaster);
            builder.HasOne(x => x.WorkingTime);
        }
    }
}

using ApplicationsAudit.Domain.Districts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class DistrictsConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.Property(x => x.City).IsRequired().HasMaxLength(60);
        }
    }
}

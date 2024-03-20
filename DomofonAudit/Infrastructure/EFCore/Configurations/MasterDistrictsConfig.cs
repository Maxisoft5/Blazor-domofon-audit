using ApplicationsAudit.Domain.Districts;
using ApplicationsAudit.Domain.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class MasterDistrictsConfig : IEntityTypeConfiguration<MasterDistrict>
    {
        public void Configure(EntityTypeBuilder<MasterDistrict> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.Master).WithMany(x => x.MasterDistricts);
            builder.HasOne(x => x.District).WithMany(x => x.MasterDistricts);
        }
    }
}

using ApplicationsAudit.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ApplicationAddressConfig : IEntityTypeConfiguration<ApplicationAddress>
    {
        public void Configure(EntityTypeBuilder<ApplicationAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.District).WithMany(x => x.ApplicationAddresses);
            builder.HasOne(x => x.Address);
        }
    }
}

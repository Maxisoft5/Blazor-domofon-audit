using ApplicationsAudit.Domain.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class MasterInfosConfig : IEntityTypeConfiguration<MasterInfo>
    {
        public void Configure(EntityTypeBuilder<MasterInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ContactInfo);
        }
    }
}

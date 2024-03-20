using ApplicationsAudit.Domain.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ManagerInfoConfig : IEntityTypeConfiguration<ManagerInfo>
    {
        public void Configure(EntityTypeBuilder<ManagerInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ContactInfo);
        }
    }
}

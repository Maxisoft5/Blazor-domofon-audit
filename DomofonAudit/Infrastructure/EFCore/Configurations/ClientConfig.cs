using ApplicationsAudit.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ContactInfo);
            builder.HasMany(x => x.Addresses).WithOne(x => x.Client);
            builder.Property(x => x.AddedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.HasMany(x => x.Applications).WithOne(x => x.RequestedBy);
        }
    }
}

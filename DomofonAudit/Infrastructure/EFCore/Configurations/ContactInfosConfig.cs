using ApplicationsAudit.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ContactInfosConfig : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Patronymic).HasMaxLength(100);
            builder.Property(x => x.MobilePhone1).HasMaxLength(30);
            builder.Property(x => x.MobilePhone2).HasMaxLength(30);
            builder.Property(x => x.HomePhone).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}

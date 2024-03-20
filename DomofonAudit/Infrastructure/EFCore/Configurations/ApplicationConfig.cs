using ApplicationsAudit.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configurations
{
    public class ApplicationConfig : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Address);
            builder.Property(x => x.Id).IsRequired();
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasOne(x => x.AssignedMaster).WithMany(x => x.Applications);
            builder.HasOne(x => x.AssignedManager).WithMany(x => x.Applications);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasMany(x => x.Comments).WithOne(x => x.Application);
            builder.HasOne(x => x.RequestedBy).WithMany(x => x.Applications);
        }
    }
}

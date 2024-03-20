using ApplicationsAudit.Domain.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configurations
{
    public class ManagerRolesConfig : IEntityTypeConfiguration<ManagerRole>
    {
        public void Configure(EntityTypeBuilder<ManagerRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.NormalizedName).HasMaxLength(128);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
        }
    }
}

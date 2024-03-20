using ApplicationsAudit.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class ApplicationCommentConfig : IEntityTypeConfiguration<ApplicationComment>
    {
        public void Configure(EntityTypeBuilder<ApplicationComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Application).WithMany(x => x.Comments);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.Comment).IsRequired();
            builder.Property(x => x.AddedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

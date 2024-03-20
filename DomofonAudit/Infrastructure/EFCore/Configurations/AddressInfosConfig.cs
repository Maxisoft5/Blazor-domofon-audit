using ApplicationsAudit.Domain.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configurations
{
    public class AddressInfosConfig : IEntityTypeConfiguration<AddressInfo>
    {

        public void Configure(EntityTypeBuilder<AddressInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.City).HasMaxLength(AddressInfo.CityMaxLength);
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Street).HasMaxLength(AddressInfo.StreetMaxLength);
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.Region).HasMaxLength(AddressInfo.RegionMaxLength);
            builder.Property(x => x.Region).IsRequired();
            builder.Property(x => x.EntranceNumber).HasMaxLength(AddressInfo.EntranceNumberMaxLength);
            builder.Property(x => x.HomeNumber).HasMaxLength(AddressInfo.HomeNumberMaxLength);
            builder.Property(x => x.FlatNumber).HasMaxLength(AddressInfo.FlatNumberMaxLength);
        }
    }
}

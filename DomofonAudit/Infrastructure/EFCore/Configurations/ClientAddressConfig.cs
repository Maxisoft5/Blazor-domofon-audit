using ApplicationsAudit.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configurations
{
    public class ClientAddressConfig : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Address);
            builder.HasOne(x => x.Client).WithMany(x => x.Addresses);
            builder.HasOne(x => x.District);
        }
    }
}

using Law.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Law.Persistence.EntityConfig
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(x => x.Appointments).WithOne(x => x.Client);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.Email);
            builder.HasQueryFilter(x => x.IsDelete == false);
        }
    }
}

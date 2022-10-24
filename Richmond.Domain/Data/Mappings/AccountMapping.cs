using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richmond.Domain.Entities;

namespace Richmond.Domain.Data.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Postcode)
                .HasMaxLength(10);
            builder.ToTable("Account");
        }
    }
}

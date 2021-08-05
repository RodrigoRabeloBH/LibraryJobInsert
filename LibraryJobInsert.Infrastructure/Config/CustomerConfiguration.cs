using LibraryJobInsert.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryJobInsert.Infrastructure.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).IsRequired().HasColumnType("varchar(40)").HasMaxLength(40);
            builder.Property(c => c.LastName).IsRequired().HasColumnType("varchar(40)").HasMaxLength(40);
            builder.Property(c => c.Email).IsRequired().HasColumnType("varchar(40)").HasMaxLength(40);
        }
    }
}

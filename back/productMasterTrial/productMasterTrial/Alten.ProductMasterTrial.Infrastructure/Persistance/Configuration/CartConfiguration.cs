using Alten.ProductMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.UserId).IsRequired();

            builder.Ignore(c => c.Total);

            builder.HasMany(c => c.Items).WithOne().HasForeignKey("CartId").IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}

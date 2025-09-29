using Alten.ProductMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.Property<Guid>("Id")
                .ValueGeneratedOnAdd();

            builder.HasKey("Id");

            builder.Property(ci => ci.ProductId)
                   .IsRequired();

            builder.Property(ci => ci.Quantity)
                   .IsRequired();
        }
    }
}

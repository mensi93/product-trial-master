using Alten.ProductMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class WishListItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("WishlistItems");

            builder.Property<Guid>("Id")
                .ValueGeneratedOnAdd();

            builder.HasKey("Id");


            builder.Property(wi => wi.ProductId)
                   .IsRequired();

            builder.Property<Guid>("WishlistId");
        }
    }
}

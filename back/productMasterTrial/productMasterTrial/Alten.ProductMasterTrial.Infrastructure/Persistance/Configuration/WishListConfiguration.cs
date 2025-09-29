using Alten.ProductMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("Wishlists");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(w => w.UserId)
                   .IsRequired();

            builder.HasMany(w => w.Items)
                   .WithOne() 
                   .HasForeignKey("WishlistId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

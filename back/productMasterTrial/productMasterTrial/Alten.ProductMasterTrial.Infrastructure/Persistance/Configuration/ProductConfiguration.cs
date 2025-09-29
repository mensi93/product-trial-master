using Alten.ProductMasterTrial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description);
            builder.Property(p => p.Image);
            builder.Property(p => p.Category).IsRequired();
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.Quantity);
            builder.Property(p => p.InternalReference).IsRequired();
            builder.Property(p => p.ShellId);
            builder.Property(p => p.Rating);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt);

            builder.OwnsOne(p => p.InventoryStatus, inv =>
            {
                inv.Property(i => i.Value)
                   .HasColumnName("InventoryStatus")
                   .IsRequired();
            });

            builder.HasIndex(p => p.Code).IsUnique();
            builder.HasIndex(p => p.InternalReference).IsUnique();
            builder.HasIndex(p => p.ShellId).IsUnique();
        }
    }
}

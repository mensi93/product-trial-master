using Alten.ProductMasterTrial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.ProductMaster.Infrastructure.Persistance.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(m => m.Id);


            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.FirstName)
                .IsRequired();

            builder.Property(m => m.Password).IsRequired();

            builder.OwnsOne(m => m.UserName, uname =>
            {
                uname.Property(u => u.Value)
                     .HasColumnName("UserName")
                     .IsRequired();

                uname.HasIndex(u => u.Value).IsUnique();

            });

            builder.OwnsOne(m => m.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });


        }
    }
}

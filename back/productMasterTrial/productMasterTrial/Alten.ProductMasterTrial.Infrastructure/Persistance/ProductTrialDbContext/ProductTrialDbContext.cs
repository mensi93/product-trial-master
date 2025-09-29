using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMaster.Infrastructure.Persistance.Configuration;
using Alten.ProductMasterTrial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alten.ProductMasterTrial.Infrastructure.Persistance
{
    public  class ProductTrialDbContext : DbContext
    {

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<WishList> WishLists => Set<WishList>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();

        public ProductTrialDbContext(DbContextOptions<ProductTrialDbContext> options)
            : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //todo :supprimer la cnx string
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=ProductTrialDb;Integrated Security=True;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new WishListConfiguration());
            modelBuilder.ApplyConfiguration(new WishListItemConfiguration());
        }
    }
}

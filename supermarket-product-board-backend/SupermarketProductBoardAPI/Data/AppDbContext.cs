
using Microsoft.EntityFrameworkCore;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Paper> Papers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=spb-db;User ID=sa;Password=6e8JFp$jSxb*x3;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
              .Property(p => p.Price)
              .HasPrecision(18, 2);

            // Product → Category Relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete here

            // Product → Company Relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(co => co.Products)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete here

            // Product → Paper Relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Paper)
                .WithMany(pa => pa.Products)
                .HasForeignKey(p => p.PaperId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascading paths

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Bootcamp.Repository.Categories;
using Bootcamp.Repository.Identities;
using Bootcamp.Repository.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bootcamp.Repository
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppUser>().Property(x => x.Name).HasMaxLength(100); // Dbde tutulacak max length bilgisi. Nvarchar(100)
            //modelBuilder.Entity<AppUser>().ToTable("CustomUserTableName"); // Identity sınıfının otomatik oluşturduğu tabloların ismini değiştirmek için
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}

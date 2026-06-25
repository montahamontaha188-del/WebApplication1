using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApplication1.Data.models;


namespace WebApplication1.Data
{
    public class AppDbContaxt: DbContext
    {
        public AppDbContaxt(DbContextOptions<AppDbContaxt> options) : base(options)
        {
        }

        public DbSet<Products> Products {get; set; }
        public DbSet<Customers> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(p => p.Category)
                .HasConversion<string>();
        }

    }
}

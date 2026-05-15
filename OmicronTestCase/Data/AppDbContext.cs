using Microsoft.EntityFrameworkCore;
using OmicronTestCase.Models;

namespace OmicronTestCase.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired();
    }
}
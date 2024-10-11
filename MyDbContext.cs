

using Microsoft.EntityFrameworkCore;

namespace Product.Api;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }

    /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Optional: Customize the table names if needed
        modelBuilder.Entity<Item>().ToTable("Item");
        modelBuilder.Entity<Category>().ToTable("Category");
    } */
}

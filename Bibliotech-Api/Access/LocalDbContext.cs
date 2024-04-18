using Bibliotech_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_Api.Access;

public class LocalDbContext : DbContext
{
    protected LocalDbContext()
    {
    }

    public LocalDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Books> books { get; set; }
    public DbSet<Users> users { get; set; }
    public DbSet<Bookings> bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bookings>()
            .HasOne(b => b.user)
            .WithMany()
            .HasForeignKey(b => b.user_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Bookings>()
            .HasOne(b => b.book)
            .WithMany()
            .HasForeignKey(b => b.book_id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
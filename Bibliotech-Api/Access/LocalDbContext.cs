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

    public DbSet<Book> books { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Booking> bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Booking>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
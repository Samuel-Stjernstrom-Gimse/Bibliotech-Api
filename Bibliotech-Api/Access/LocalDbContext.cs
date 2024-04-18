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
    public DbSet<Booking> booking { get; set; }
}
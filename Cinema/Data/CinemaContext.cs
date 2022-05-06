using CinemaClient.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaClient.Data;

public class CinemaContext : DbContext
{
    public DbSet<CinemaRoom> Rooms { get; set; } = null!;
    public DbSet<Spectator> Spectators { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;

    public CinemaContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}

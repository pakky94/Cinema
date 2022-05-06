﻿using CinemaClient.Domain;
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
        var room = modelBuilder.Entity<CinemaRoom>();
        room.HasOne(r => r.Movie).WithMany().HasForeignKey(r => r.MovieId);

        var movie = modelBuilder.Entity<Movie>();
        var spectator = modelBuilder.Entity<Spectator>();
        var ticket = modelBuilder.Entity<Ticket>();
        ticket.HasOne(t => t.Room).WithMany().HasForeignKey(t => t.RoomId);
        ticket.HasOne(t => t.Spectator).WithMany().HasForeignKey(t => t.SpectatorId);

        room.HasData(
            new CinemaRoom { Id = 1, MovieId = 1, Capacity = 3 },
            new CinemaRoom { Id = 2, MovieId = 3, Capacity = 5 }
            );
        movie.HasData(new Movie
        {
            Id = 1,
            Title = "Back to the Future",
            Author = "Robert Zemeckis",
            Producer = "Robert Zemeckis",
            Genre = "Sci-Fi",
            Duration = 116,
        }, new Movie
        {
            Id = 2,
            Title = "The Matrix",
            Author = "Lilly Wachowski",
            Producer = "Lilly Wachowski",
            Genre = "Sci-Fi",
            Duration = 136,
        }, new Movie
        {
            Id = 3,
            Title = "Psyco",
            Author = "Robert Bloch",
            Producer = "Alfred Hitchcock",
            Genre = "Horror",
            Duration = 108,
        });
        ticket.HasData(new Ticket
        {
            Id = 1,
            Seat = "1A",
            RoomId = 1,
            SpectatorId = 1,
            Price = 9.9m,
        });
        spectator.HasData(new Spectator
        {
            Id = 1,
            Name = "Marco",
            Surname = "Pacchialat",
            BirthDate = new DateTime(1994, 2, 14),
        });
    }
}

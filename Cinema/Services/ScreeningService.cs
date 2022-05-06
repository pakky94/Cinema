using CinemaClient.Data;
using CinemaClient.Domain;
using CinemaClient.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CinemaClient.Services;

public interface IScreeningService
{
    Task<Ticket> AddSpectator(int screeningId, int spectatorId);
    Task<MovieScreening?> GetById(int screeningId);
    Task<decimal> TotalEarnings(int screeningId);
    Task<IEnumerable<MovieScreening>> GetAll();
    Task Create(MovieScreening screening);
    Task Update(MovieScreening screening);
    Task Delete(int screeningId);
}

public class ScreeningService : IScreeningService
{
    private readonly CinemaContext _context;

    public ScreeningService(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieScreening>> GetAll()
    {
        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Room)
            .ToListAsync();
        return screening;
    }

    public async Task<MovieScreening?> GetById(int screeningId)
    {
        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Room)
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == screeningId);
        return screening;
    }

    public async Task<Ticket> AddSpectator(int screeningId, int spectatorId)
    {
        var screening = await _context.Screenings
            .Include(s => s.Room)
            .Include(r => r.Tickets)
            .FirstOrDefaultAsync(s => s.Id == screeningId);

        if (screening?.Room is null)
        {
            throw new ArgumentException("Screening/Room not found");
        }

        var ticketCount = screening.Tickets.Count;
        if (ticketCount >= screening.Room.Capacity)
        {
            throw new SalaAlCompletoException();
        }

        var spectator = await _context.Spectators.FirstOrDefaultAsync(s => s.Id == spectatorId);
        if (spectator is null)
        {
            throw new ArgumentException("Spectator not found");
        }

        var ticket = new Ticket
        {
            SpectatorId = spectatorId,
            ScreeningId = screeningId,
            Price = screening.Price * spectator.PriceMultiplier(DateTime.Now),
            Seat = (ticketCount + 1).ToString(),
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<decimal> TotalEarnings(int screeningId)
    {
        var screening = await _context.Screenings
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == screeningId);

        if (screening?.Room is null)
        {
            throw new ArgumentException("Screening not found");
        }

        return screening.Tickets.Sum(t => t.Price);
    }

    public Task Create(MovieScreening screening)
    {
        _context.Add(screening);
        return _context.SaveChangesAsync();
    }

    public Task Update(MovieScreening screening)
    {
        _context.Update(screening);
        return _context.SaveChangesAsync();
    }

    public async Task Delete(int screeningId)
    {
        var screening = await _context.Screenings.FirstAsync(s => s.Id == screeningId);
        _context.Screenings.Remove(screening);
        await _context.SaveChangesAsync();
    }
}

using CinemaClient.Data;
using CinemaClient.Domain;
using CinemaClient.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CinemaClient.Services;

public interface IScreeningService
{
    Task AddSpectator(int screeningId, int spectatorId);
    Task<MovieScreening> GetScreening(int screeningId);
    Task<decimal> TotalEarnings(int screeningId);
}

public class ScreeningService : IScreeningService
{
    private readonly CinemaContext _context;

    public ScreeningService(CinemaContext context)
    {
        _context = context;
    }

    public async Task<decimal> TotalEarnings(int screeningId)
    {
        var screening = await _context.Screenings.Include(s => s.Tickets).FirstOrDefaultAsync(s => s.Id == screeningId);

        if (screening?.Room is null)
        {
            throw new ArgumentException("Screening not found");
        }

        return screening.Tickets.Sum(t => t.Price);
    }

    public async Task<MovieScreening> GetScreening(int screeningId)
    {
        var screening = await _context.Screenings
            .Include(s => s.Room)
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == screeningId);
        return screening;
    }

    public async Task AddSpectator(int screeningId, int spectatorId)
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
            ScreeningId = screeningId,
            Price = screening.Price * spectator.PriceMultiplier(),
            Seat = (ticketCount + 1).ToString(),
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }
}

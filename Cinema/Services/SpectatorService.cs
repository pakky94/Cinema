using CinemaClient.Data;
using CinemaClient.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaClient.Services;

public interface ISpectatorService
{
    Task Create(Spectator spectator);
    Task Delete(int id);
    Task<List<Spectator>> GetAll();
    Task<Spectator> GetById(int spectatorId);
    Task Update(Spectator spectator);
}

public class SpectatorService : ISpectatorService
{
    private readonly CinemaContext _context;

    public SpectatorService(CinemaContext context)
    {
        _context = context;
    }

    public Task<List<Spectator>> GetAll()
    {
        return _context.Spectators.ToListAsync();
    }

    public Task<Spectator?> GetById(int spectatorId)
    {
        return _context.Spectators.FirstOrDefaultAsync(s => s.Id == spectatorId);
    }

    public Task Create(Spectator spectator)
    {
        _context.Spectators.Add(spectator);
        return _context.SaveChangesAsync();
    }

    public Task Update(Spectator spectator)
    {
        _context.Spectators.Update(spectator);
        return _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var spectator = await _context.Spectators.FirstAsync(s => s.Id == id);
        _context.Spectators.Remove(spectator);
        await _context.SaveChangesAsync();
    }
}

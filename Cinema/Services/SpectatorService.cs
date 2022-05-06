using CinemaClient.Data;
using CinemaClient.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaClient.Services;

public interface ISpectatorService
{
    Task<List<Spectator>> GetAll();
    Task<Spectator> GetById(int spectatorId);
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
}

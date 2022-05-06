using CinemaClient.Data;
using CinemaClient.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaClient.Services;

public interface IRoomService
{
    Task Create(CinemaRoom room);
    Task Delete(int id);
    Task<List<CinemaRoom>> GetAll();
    Task<CinemaRoom?> GetById(int id);
    Task Update(CinemaRoom cinemaRoom);
}

public class RoomService : IRoomService
{
    private readonly CinemaContext _context;

    public RoomService(CinemaContext context)
    {
        _context = context;
    }

    public Task<List<CinemaRoom>> GetAll()
    {
        return _context.Rooms
            .Include(r => r.CurrentScreening)
            .ThenInclude(s => s.Movie)
            .ToListAsync();
    }

    public Task<CinemaRoom?> GetById(int id)
    {
        return _context.Rooms
            .Include(r => r.CurrentScreening)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task Create(CinemaRoom room)
    {
        _context.Rooms.Add(room);
        return _context.SaveChangesAsync();
    }

    public Task Delete(int id)
    {
        var room = _context.Rooms.Find(id);
        _context.Rooms.Remove(room);
        return _context.SaveChangesAsync();
    }

    public Task Update(CinemaRoom cinemaRoom)
    {
        _context.Update(cinemaRoom);
        return _context.SaveChangesAsync();
    }
}

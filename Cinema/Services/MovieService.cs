using CinemaClient.Data;
using CinemaClient.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CinemaClient.Services;

public interface IMovieService
{
    Task<List<Movie>> GetAll();
}

public class MovieService : IMovieService
{
    private readonly CinemaContext _context;

    public MovieService(CinemaContext context)
    {
        _context = context;
    }

    public Task<List<Movie>> GetAll()
    {
        return _context.Movies.ToListAsync();
    }
}

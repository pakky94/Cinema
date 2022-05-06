#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaClient.Data;
using CinemaClient.Domain;

namespace CinemaClient.Controllers
{
    public class ScreeningsController : Controller
    {
        private readonly CinemaContext _context;

        public ScreeningsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Screenings
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.Screenings.Include(m => m.Movie).Include(m => m.Room);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Screenings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.Screenings
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // GET: Screenings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Screenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieScreening movieScreening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieScreening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", movieScreening.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", movieScreening.RoomId);
            return View(movieScreening);
        }

        // GET: Screenings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.Screenings.FindAsync(id);
            if (movieScreening == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", movieScreening.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", movieScreening.RoomId);
            return View(movieScreening);
        }

        // POST: Screenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieScreening movieScreening)
        {
            if (id != movieScreening.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieScreening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieScreeningExists(movieScreening.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", movieScreening.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", movieScreening.RoomId);
            return View(movieScreening);
        }

        // GET: Screenings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.Screenings
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // POST: Screenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieScreening = await _context.Screenings.FindAsync(id);
            _context.Screenings.Remove(movieScreening);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieScreeningExists(int id)
        {
            return _context.Screenings.Any(e => e.Id == id);
        }
    }
}

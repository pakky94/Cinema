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

namespace CinemaClient.Controllers;

public class CinemaRoomsController : Controller
{
    private readonly CinemaContext _context;

    public CinemaRoomsController(CinemaContext context)
    {
        _context = context;
    }

    // GET: CinemaRooms
    public async Task<IActionResult> Index()
    {
        var cinemaContext = _context.Rooms.Include(c => c.Movie);
        return View(await cinemaContext.ToListAsync());
    }

    // GET: CinemaRooms/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _context.Rooms
            .Include(c => c.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cinemaRoom == null)
        {
            return NotFound();
        }

        return View(cinemaRoom);
    }

    // GET: CinemaRooms/Create
    public IActionResult Create()
    {
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
        return View();
    }

    // POST: CinemaRooms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Capacity,MovieId")] CinemaRoom cinemaRoom)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cinemaRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaRoom.MovieId);
        return View(cinemaRoom);
    }

    // GET: CinemaRooms/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _context.Rooms.FindAsync(id);
        if (cinemaRoom == null)
        {
            return NotFound();
        }
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaRoom.MovieId);
        return View(cinemaRoom);
    }

    // POST: CinemaRooms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,MovieId")] CinemaRoom cinemaRoom)
    {
        if (id != cinemaRoom.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cinemaRoom);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaRoomExists(cinemaRoom.Id))
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
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaRoom.MovieId);
        return View(cinemaRoom);
    }

    // GET: CinemaRooms/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _context.Rooms
            .Include(c => c.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cinemaRoom == null)
        {
            return NotFound();
        }

        return View(cinemaRoom);
    }

    // POST: CinemaRooms/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cinemaRoom = await _context.Rooms.FindAsync(id);
        _context.Rooms.Remove(cinemaRoom);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CinemaRoomExists(int id)
    {
        return _context.Rooms.Any(e => e.Id == id);
    }
}

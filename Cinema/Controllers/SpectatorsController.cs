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
    public class SpectatorsController : Controller
    {
        private readonly CinemaContext _context;

        public SpectatorsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Spectators
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.Spectators.Include(s => s.Ticket);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Spectators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spectator = await _context.Spectators
                .Include(s => s.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spectator == null)
            {
                return NotFound();
            }

            return View(spectator);
        }

        // GET: Spectators/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id");
            return View();
        }

        // POST: Spectators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthDate,TicketId")] Spectator spectator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spectator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", spectator.TicketId);
            return View(spectator);
        }

        // GET: Spectators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spectator = await _context.Spectators.FindAsync(id);
            if (spectator == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", spectator.TicketId);
            return View(spectator);
        }

        // POST: Spectators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate,TicketId")] Spectator spectator)
        {
            if (id != spectator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spectator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpectatorExists(spectator.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", spectator.TicketId);
            return View(spectator);
        }

        // GET: Spectators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spectator = await _context.Spectators
                .Include(s => s.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spectator == null)
            {
                return NotFound();
            }

            return View(spectator);
        }

        // POST: Spectators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spectator = await _context.Spectators.FindAsync(id);
            _context.Spectators.Remove(spectator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpectatorExists(int id)
        {
            return _context.Spectators.Any(e => e.Id == id);
        }
    }
}

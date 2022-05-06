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
using CinemaClient.Services;
using CinemaClient.Models;

namespace CinemaClient.Controllers
{
    public class ScreeningsController : Controller
    {
        private readonly IScreeningService _screeningService;
        private readonly ISpectatorService _spectatorService;
        private readonly IRoomService _roomService;
        private readonly IMovieService _movieService;

        public ScreeningsController(IScreeningService screeningService, ISpectatorService spectatorService,
            IRoomService roomService,
            IMovieService movieService)
        {
            _screeningService = screeningService;
            _spectatorService = spectatorService;
            _roomService = roomService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var screenings = await _screeningService.GetAll();
            return View(screenings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _screeningService.GetById((int)id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewData["MovieId"] = new SelectList(await _movieService.GetAll(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAll(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieScreening movieScreening)
        {
            if (ModelState.IsValid)
            {
                await _screeningService.Create(movieScreening);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(await _movieService.GetAll(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAll(), "Id", "Id");
            return View(movieScreening);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _screeningService.GetById((int)id);
            if (movieScreening == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(await _movieService.GetAll(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAll(), "Id", "Id");
            return View(movieScreening);
        }

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
                    await _screeningService.Update(movieScreening);
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
            ViewData["MovieId"] = new SelectList(await _movieService.GetAll(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAll(), "Id", "Id");
            return View(movieScreening);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = await _screeningService.GetById((int)id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _screeningService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieScreeningExists(int id)
        {
            var screening = _screeningService.GetById(id);
            return screening is not null;
        }

        [HttpGet]
        public async Task<IActionResult> AddSpectator(int id)
        {
            var screening = await _screeningService.GetById(id);
            var spectators = await _spectatorService.GetAll();
            return View(new AddSpectatorViewModel(screening, spectators, null));
        }

        [HttpPost, ActionName("AddSpectator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSpectatorPost(int id, int spectatorId)
        {
            var screening = await _screeningService.GetById(id);
            var spectator = await _spectatorService.GetById(spectatorId);

            if (screening == null)
                return NotFound("Screening not found");
            if (spectator == null)
                return NotFound("Spectator not found");

            if (!SpectatorCanViewMovie(spectator, screening.Movie)) 
            {
                ModelState.AddModelError("Ticket.SpectatorId", "Spectator cannot view this movie");
            }

            if (ModelState.IsValid)
            {
                await _screeningService.AddSpectator(screening.Id, spectator.Id);
                return RedirectToAction("Details", new { id });
            }

            return View("AddSpectator", new AddSpectatorViewModel(screening, await _spectatorService.GetAll(), spectatorId));
        }

        private bool SpectatorCanViewMovie(Spectator spectator, Movie movie)
        {
            var age = DateTime.Now.Year - spectator.BirthDate.Year;
            return !string.Equals(movie.Genre, "Horror") || age >= 14;
        }
    }
}

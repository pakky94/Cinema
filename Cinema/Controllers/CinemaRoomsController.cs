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

namespace CinemaClient.Controllers;

public class CinemaRoomsController : Controller
{
    private readonly IRoomService _roomService;
    private readonly IMovieService _movieService;
    private readonly IScreeningService _screeningService;

    public CinemaRoomsController(IRoomService roomService, IMovieService movieService, IScreeningService screeningService)
    {
        _roomService = roomService;
        _movieService = movieService;
        _screeningService = screeningService;
    }

    // GET: CinemaRooms
    public async Task<IActionResult> Index()
    {
        var rooms = await _roomService.GetAll();
        return View(rooms);
    }

    // GET: CinemaRooms/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _roomService.GetById((int)id);
        if (cinemaRoom == null)
        {
            return NotFound();
        }
        var screening = await _screeningService.GetById((int)cinemaRoom.CurrentScreeningId);
        cinemaRoom.CurrentScreening = screening;

        return View(cinemaRoom);
    }

    // GET: CinemaRooms/Create
    public async Task<IActionResult> CreateAsync()
    {
        ViewData["ScreeningId"] = new SelectList(await _screeningService.GetAll(), "Id", "Movie.Title");
        return View();
    }

    // POST: CinemaRooms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CinemaRoom cinemaRoom)
    {
        if (ModelState.IsValid)
        {
            await _roomService.Create(cinemaRoom);
            return RedirectToAction(nameof(Index));
        }
        ViewData["ScreeningId"] = new SelectList(await _screeningService.GetAll(), "Id", "Movie.Title", cinemaRoom.CurrentScreeningId);
        return View(cinemaRoom);
    }

    // GET: CinemaRooms/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _roomService.GetById((int)id);
        if (cinemaRoom == null)
        {
            return NotFound();
        }
        ViewData["ScreeningId"] = new SelectList(await _screeningService.GetAll(), "Id", "Movie.Title", cinemaRoom.CurrentScreeningId);
        return View(cinemaRoom);
    }

    // POST: CinemaRooms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CinemaRoom cinemaRoom)
    {
        if (id != cinemaRoom.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _roomService.Update(cinemaRoom);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CinemaRoomExists(cinemaRoom.Id))
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
        ViewData["ScreeningId"] = new SelectList(await _screeningService.GetAll(), "Id", "Movie.Title", cinemaRoom.CurrentScreeningId);
        return View(cinemaRoom);
    }

    private async Task<bool> CinemaRoomExists(int id)
    {
        var room = await _roomService.GetById(id);
        return room is not null;
    }

    // GET: CinemaRooms/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinemaRoom = await _roomService.GetById((int)id);
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
        await _roomService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}

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

    public async Task<IActionResult> Index()
    {
        var rooms = await _roomService.GetAll();
        return View(rooms);
    }

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

    public async Task<IActionResult> CreateAsync()
    {
        var screenings = await _screeningService.GetAll();
        return View(new CinemaRoomEditViewModel(null, screenings));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CinemaRoomEditViewModel vm)
    {
        var cinemaRoom = vm.Room;
        if (ModelState.IsValid)
        {
            await _roomService.Create(cinemaRoom);
            return RedirectToAction(nameof(Index));
        }
        var screenings = await _screeningService.GetAll();
        return View(new CinemaRoomEditViewModel(null, screenings));
    }

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
        var screenings = await _screeningService.GetAll();
        return View(new CinemaRoomEditViewModel(cinemaRoom, screenings));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CinemaRoomEditViewModel vm)
    {
        var cinemaRoom = vm.Room;
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
        var screenings = await _screeningService.GetAll();
        return View(new CinemaRoomEditViewModel(cinemaRoom, screenings));
    }

    private async Task<bool> CinemaRoomExists(int id)
    {
        var room = await _roomService.GetById(id);
        return room is not null;
    }

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

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _roomService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}

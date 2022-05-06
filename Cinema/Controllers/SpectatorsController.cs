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

namespace CinemaClient.Controllers
{
    public class SpectatorsController : Controller
    {
        private readonly ISpectatorService _spectatorService;

        public SpectatorsController(ISpectatorService spectatorService)
        {
            _spectatorService = spectatorService;
        }

        public async Task<IActionResult> Index()
        {
            var spectators = await _spectatorService.GetAll();
            return View(spectators);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spectator = await _spectatorService.GetById((int)id);
            if (spectator == null)
            {
                return NotFound();
            }

            return View(spectator);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spectator spectator)
        {
            if (ModelState.IsValid)
            {
                await _spectatorService.Create(spectator);
                return RedirectToAction(nameof(Index));
            }
            return View(spectator);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spectator = await _spectatorService.GetById((int)id);
            if (spectator == null)
            {
                return NotFound();
            }
            return View(spectator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Spectator spectator)
        {
            if (id != spectator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _spectatorService.Update(spectator);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SpectatorExistsAsync(spectator.Id))
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
            return View(spectator);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var spectator = await _spectatorService.GetById(id);
            if (spectator == null)
            {
                return NotFound();
            }

            return View(spectator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _spectatorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SpectatorExistsAsync(int id)
        {
            var spectator = await _spectatorService.GetById(id);
            return spectator is not null;
        }
    }
}

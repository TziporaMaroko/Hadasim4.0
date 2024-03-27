using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hadasim4._0.Data;
using Hadasim4._0.Models;

namespace Hadasim4._0.Controllers
{
    public class VaccinationsController : Controller
    {
        private readonly Hadasim4_0Context _context;

        public VaccinationsController(Hadasim4_0Context context)
        {
            _context = context;
        }

        // GET: Vaccinations
        public async Task<IActionResult> Index()
        {
              return _context.Vaccination != null ? 
                          View(await _context.Vaccination.ToListAsync()) :
                          Problem("Entity set 'Hadasim4_0Context.Vaccination'  is null.");
        }

        // GET: Vaccinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // GET: Vaccinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaccinations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Producer")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Producer")] Vaccination vaccination)
        {
            if (id != vaccination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.Id))
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
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccination == null)
            {
                return Problem("Entity set 'Hadasim4_0Context.Vaccination'  is null.");
            }
            var vaccination = await _context.Vaccination.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccination.Remove(vaccination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
          return (_context.Vaccination?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Vaccinations/GetVaccinations
        public async Task<IActionResult> GetVaccinations()
        {
            var vaccinations = await _context.Vaccination
                                        .Select(v => new { Id = v.Id, Producer = v.Producer })
                                        .ToListAsync();

            if (vaccinations != null)
            {
                return Ok(vaccinations);
            }
            else
            {
                return Problem("Entity set 'Hadasim4_0Context.Vaccinations' is null.");
            }
        }
    }
}

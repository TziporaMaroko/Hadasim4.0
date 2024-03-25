using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hadasim4._0.Data;
using Hadasim4._0.Models;
using Hadasim4._0.Migrations;

namespace Hadasim4._0.Controllers
{
    public class MembersController : Controller
    {
        private readonly Hadasim4_0Context _context;

        public MembersController(Hadasim4_0Context context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return _context.Member != null ?
                        View(await _context.Member.ToListAsync()) :
                        Problem("Entity set 'Hadasim4_0Context.Member'  is null.");
        }

        // GET: Members/Details/3
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,FirstName,LastName,City,Street,HouseNumber,BirthDate,PhoneNumber,MobilePhone,PositiveDate,RecoveryDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,FirstName,LastName,City,Street,HouseNumber,BirthDate,PhoneNumber,MobilePhone,PositiveDate,RecoveryDate")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            try
            {
                var original = await _context.Member.FindAsync(id);
                if (original == null)
                {
                    return NotFound();
                }

                // Detach the original entity to avoid tracking conflicts
                _context.Entry(original).State = EntityState.Detached;

                // Update individual properties of the original entity
                original.FirstName = member.FirstName;
                original.LastName = member.LastName;
                original.City = member.City;
                original.Street = member.Street;
                original.HouseNumber = member.HouseNumber;
                original.BirthDate = member.BirthDate;
                original.PhoneNumber = member.PhoneNumber;
                original.MobilePhone = member.MobilePhone;
                original.PositiveDate = member.PositiveDate;
                original.RecoveryDate = member.RecoveryDate;

                if (ModelState.IsValid)
                {
                    // Update the entity in the context and save changes
                    _context.Update(original);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'Hadasim4_0Context.Member'  is null.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            try {
                //var memberVaccinationRelations = await _context.MemberVaccinationRelation.Where(mvr => mvr.MemberId == member.MemberId).ToListAsync();
                //_context.MemberVaccinationRelation.RemoveRange(memberVaccinationRelations);
                //await _context.SaveChangesAsync();
                
                _context.Member.Remove(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Problem("An error occurred while deleting the member: " + ex.Message);
            }
        }

        private bool MemberExists(int id)
        {
            return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult>  AddVaccination(int memberId, int vaccinationId, DateTime vaccinationDate)
        {
            var member = _context.Member.Find(memberId);

            if (member == null)
            {
                return NotFound(); // Handle if member is not found
            }

            // Check if member already has four vaccinations
            if (_context.MemberVaccinationRelation.Where(mvr => mvr.MemberId == member.MemberId).Count() >= 4)
            {
                return BadRequest("The member already has four vaccinations."); // Handle if member has already four vaccinations
            }
            
            var memberVaccinationRelation = new MemberVaccinationRelation
            {
                MemberId = member.MemberId,
                VaccinationId = vaccinationId,
                Date = vaccinationDate
            };

            _context.MemberVaccinationRelation.Add(memberVaccinationRelation);
            _context.SaveChanges();

            return Ok(); // Return success status
        }

        // GET: Members/GetMemberVaccinations
        public async Task<IActionResult> GetMemberVaccinations(int memberId)
        {
            try { 
                var memberVaccinations = await _context.MemberVaccinationRelation
                                            .Where(mvr => mvr.MemberId == memberId)
                                            .Join(_context.Vaccination,
                                                  mvr => mvr.VaccinationId,
                                                  v => v.Id,
                                                  (mvr, v) => new { MemberId = mvr.MemberId, VaccinationId = mvr.VaccinationId, Producer = v.Producer, Date = mvr.Date })
                                            .ToListAsync();
                return Json(memberVaccinations);
            }
            catch (Exception ex)
            {
                // Handle any errors and return an error response if needed
                return BadRequest(ex.Message);
            }
        }

    }
}

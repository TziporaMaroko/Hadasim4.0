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
using Microsoft.AspNetCore.Hosting;

namespace Hadasim4._0.Controllers
{
    public class MembersController : Controller
    {
        private readonly Hadasim4_0Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MembersController(Hadasim4_0Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,MemberId,FirstName,LastName,City,Street,HouseNumber,BirthDate,PhoneNumber,MobilePhone,PositiveDate,RecoveryDate")] Member member, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Handle the case where the user uploads an image from the file explorer
                // Generate a unique filename for the image
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{DateTime.Now.Ticks}{Path.GetExtension(ImageFile.FileName)}";

                // Define the path where you want to save the image inside the wwwroot/images folder
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Update the Image_URL property with the new path (relative to wwwroot)
                member.ImageURL = $"{uniqueFileName}";
            }
            
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,FirstName,LastName,City,Street,HouseNumber,BirthDate,PhoneNumber,MobilePhone,PositiveDate,RecoveryDate")] Member member, IFormFile ImageFile)
        {
            if (id != member.Id)
            {
                return NotFound();
            }
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Handle the case where the user uploads an image from the file explorer
                // Generate a unique filename for the image
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{DateTime.Now.Ticks}{Path.GetExtension(ImageFile.FileName)}";

                // Define the path where you want to save the image inside the wwwroot/images folder
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Update the Image_URL property with the new path (relative to wwwroot)
                member.ImageURL = $"{uniqueFileName}";
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
                original.ImageURL= member.ImageURL;

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
                return NotFound(); //if member is not found
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
                                            .Where(mvr => mvr.MemberId == memberId) //Gets all the member's vaccination ids
                                            .Join(_context.Vaccination, //For each vaccination, join to find it's producer
                                                  mvr => mvr.VaccinationId,
                                                  v => v.Id,
                                                  (mvr, v) => new { MemberId = mvr.MemberId, VaccinationId = mvr.VaccinationId, Producer = v.Producer, Date = mvr.Date })
                                            .ToListAsync();
                return Json(memberVaccinations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

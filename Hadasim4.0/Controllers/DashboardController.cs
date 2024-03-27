using Hadasim4._0.Data;
using Hadasim4._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hadasim4._0.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Hadasim4_0Context _context;

        public DashboardController(Hadasim4_0Context context)
        {
            _context = context;
        }
        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            ViewBag.nonVaccinatedMembers = await _context.Member
                                                    .CountAsync(m => !_context.MemberVaccinationRelation.Any(mvr => mvr.MemberId == m.MemberId));
            //ViewBag.sickMembersCountJson = GetDataForDateRange(DateTime.Now.AddDays(-30).Date, DateTime.Now.Date);
            return _context.Member != null ?
                        View(await _context.Member.ToListAsync()) :
                        Problem("Entity set 'Hadasim4_0Context.Member'  is null.");
        }

        // GET: Dashboard/GetDataForDateRange
        [HttpGet]
        public IActionResult GetDataForDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Filter patients who were positive during the date range
                var positiveMembers = _context.Member
                    .Where(p => p.PositiveDate <= endDate && p.RecoveryDate >= startDate)
                    .AsEnumerable(); // Switch to client evaluation

                // Calculate sick days for each patient
                var sickDaysPerMember = positiveMembers
                    .SelectMany(p => GetSickDays(p, startDate, endDate)) // Use SelectMany to flatten results
                    .ToList();

                // Group patients by sick days
                var sickMembersPerDay = sickDaysPerMember
                    .GroupBy(d => d.Date)
                    .Select(g => new
                    {
                        date = g.Key,
                        sickMembersCount = g.Count()
                    })
                    .OrderBy(g => g.date)
                    .ToList();
                var a = Json(sickMembersPerDay);
                return a;
            }
            catch (Exception ex)
            {
                // Handle any errors and return an error response if needed
                return BadRequest(ex.Message);
            }
        }

        // Helper method to calculate sick days for a patient
        private IEnumerable<DateTime> GetSickDays(Member patient, DateTime startDate, DateTime endDate)
        {
            // Define start date for the patient (positive date or start date)
            DateTime patientStartDate = patient.PositiveDate > startDate ? (DateTime)patient.PositiveDate : startDate;

            // Define end date for the patient (recovery date or end date, whichever comes first)
            DateTime patientEndDate = patient.RecoveryDate > endDate ? endDate : (DateTime)patient.RecoveryDate;

            // Iterate through each day within the patient's sick period
            for (DateTime date = patientStartDate; date <= patientEndDate; date = date.AddDays(1)) // Fix: use date.AddDays(1) to update the date
            {
                yield return date; // Yield each sick day for the patient
            }
        }

    }
}

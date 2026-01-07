
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EHRMvcDemo.Data;

namespace EHRMvcDemo.Controllers
{
    public class DoctorSummaryController : Controller
    {
        private readonly EHRDbContext _context;

        public DoctorSummaryController(EHRDbContext context)
        {
            _context = context;
        }
        // 1. Load doctors and appointments into memory
        // 2. For each doctor compute total appointments and next appointment using plain LINQ on the lists
        public async Task<IActionResult> Index()
        {
            // 1. Load data from the database
            var doctors = await _context.Doctors
                .AsNoTracking()
                .ToListAsync();

            var appointments = await _context.Appointments
                .AsNoTracking()
                .ToListAsync();

            var now = DateTime.Now;

            // 2. Build a list of anonymous objects with the required fields
            var DoctorSummary = doctors.Select(d => new
            {
                FullName = d.FullName,
                Specialty = d.Specialty,
                // Count appointments that belong to this doctor
                TotalAppointments = appointments.Count(a => a.DoctorId == d.DoctorId),
                // Find the soonest appointment date that is in the future (or null)
                NextAppointmentDate = appointments
                    .Where(a => a.DoctorId == d.DoctorId && a.AppointmentDate > now)
                    .OrderBy(a => a.AppointmentDate)
                    .Select(a => (DateTime?)a.AppointmentDate)
                    .FirstOrDefault()
            })
            .ToList();

            // 3. Pass the anonymous list to the view (view will use IEnumerable<dynamic>)
            return View(DoctorSummary);
        }
    }
}

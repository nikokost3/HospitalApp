using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly UsersDoctorsPatientDbContext _dbContext;

        public PatientController(UsersDoctorsPatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool AppointmentExists(int id)
        {
            return _dbContext.Appointments.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var doctors = await _dbContext.Users.ToListAsync();
            return View(doctors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var doctorExists = await _dbContext.Doctors.AnyAsync(d => d.Id == appointment.DoctorId);
                if (!doctorExists)
                {
                    ModelState.AddModelError("DoctorID", "The specified doctor does not exist.");
                    return View(appointment);
                }

                _dbContext.Appointments.Add(appointment);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Patient");
            }
            return View(appointment);
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var appointment = await _dbContext.Appointments.ToListAsync();

            return View(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _dbContext.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(appointment);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Doctor");
            }
            return View(appointment);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _dbContext.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Doctor");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _dbContext.Appointments.FindAsync(id);
            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }
    }
}

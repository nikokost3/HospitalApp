using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(UsersDoctorsPatientDbContext context) : base(context)
        {
        }

        public async Task<Doctor?> GetAppointmentDoctorAsync(int id)
        {
            var appointment = await _context.Appointments
                                .Where(c => c.Id == id)
                                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return null;
            }

            if (appointment.Doctor is null)
            {
                return null;
            }

            return appointment.Doctor;
        }

        public async Task<List<Patient>> GetAppointmentPatientAsync(int id)
        {
            List<Patient> appointment;
            appointment = await _context.Appointments
                            .Where(c => c.Id == id)
                            .SelectMany(c => c.Patients!)
                            .ToListAsync();

            return appointment;
        }
    }
}

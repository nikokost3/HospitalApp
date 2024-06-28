using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(UsersDoctorsPatientDbContext context) : base(context)
        {

        }

        public async Task<List<User>> GetAllUsersDoctorsAsync()
        {
            var usersWithDoctorRole = await _context.Users
                                        .Where(u => u.UserRole == UserRole.Doctor)
                                        .Include(u => u.Doctor)
                                        .ToListAsync();

            return usersWithDoctorRole;
        }

        public async Task<List<User>> GetAllUsersDoctorsAsync(int pageNumber, int pageSize)
        {
            int skip = pageSize * pageNumber;
            var usersWithDoctorRole = await _context.Users
                                        .Where(u => u.UserRole == UserRole.Doctor)
                                        .Include(u => u.Doctor)
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToListAsync();

            return usersWithDoctorRole;
        }

        public async Task<Doctor?> GetByPhoneNumber(string? phoneNumber)
        {
            return await _context.Doctors
                            .Where(s => s.PhoneNumber == phoneNumber)
                            .FirstOrDefaultAsync()!;
        }

        public async Task<List<Appointment>> GetDoctorAppointmentAsync(int id)
        {
            List<Appointment> appointments;
            appointments = await _context.Doctors
                            .Where(t => t.Id == id)
                            .SelectMany(t => t.Appointments!)
                            .ToListAsync();

            return appointments;
        }

        public async Task<User?> GetDoctorByUsernameAsync(string username)
        {
            var userDoctor = await _context.Users
                                .Where(u => u.Username == username && u.UserRole == UserRole.Doctor)
                                .SingleOrDefaultAsync();

            return userDoctor;
        }



        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

        }
    }
}

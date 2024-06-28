using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(UsersDoctorsPatientDbContext context) : base(context)
        {
        }

		public async Task<List<Appointment>> GetPatientAppointmentAsync(int id)
		{
			List<Appointment> appointments;
			appointments = await _context.Patients
							.Where(s => s.Id == id)
							.SelectMany(s => s.Appointments!)
							.ToListAsync();

			return appointments;
		}

		public async Task<Patient?> GetByPhoneNumber(string? phoneNumber)
		{
			return await _context.Patients
							.Where(s => s.PhoneNumber == phoneNumber)
							.FirstOrDefaultAsync()!;
		}

		public async Task<List<User>> GetAllUsersPatientAsync()
        {
            var usersWithPatinetRole = await _context.Users
                                            .Where(u => u.UserRole == UserRole.Patient)
                                            .Include(u => u.Patient)
                                            .ToListAsync();

            return usersWithPatinetRole;
        }          
    }
}

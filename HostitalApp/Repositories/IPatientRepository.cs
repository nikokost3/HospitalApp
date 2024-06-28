using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Appointment>> GetPatientAppointmentAsync(int id);
        Task<Patient?> GetByPhoneNumber(string? phoneNumber);
        Task<List<User>> GetAllUsersPatientAsync();
    }
}

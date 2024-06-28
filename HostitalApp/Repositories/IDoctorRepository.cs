using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Appointment>> GetDoctorAppointmentAsync(int id);
        Task<Doctor?> GetByPhoneNumber(string? phoneNumber);
        Task<List<User>> GetAllUsersDoctorsAsync();
        Task<List<User>> GetAllUsersDoctorsAsync(int pageNumber, int pageSize);
        Task<User?> GetDoctorByUsernameAsync(string username);



        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task AddAppointmentAsync(Appointment appointment);
    }
}

using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<User>> GetAllPatientsAsync();
        Task<List<Appointment>> GetPatientAppointmentsAsync(int id);
        Task<Patient?> GetPatientAsync(int id);
        Task<bool> DeletePatientAsync(int id);
        Task<int> GetPatientCountAsync();
    }
}

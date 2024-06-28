using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Doctor?> GetAppointmentDoctorAsync(int id);
        Task<List<Patient>> GetAppointmentPatientAsync(int id);
    }
}

using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Services
{
    public interface IDoctorService
    {
        Task<List<User>> GetAllUsersDoctorsAsync();
        Task<List<User>> GetAllUsersDoctorsAsync(int pageNumber, int pageSize);
        Task<int> GetDoctorCountAsync();
        Task<User?> GetDoctorByUsernameAsync(string? username);
    }
}

using AutoMapper;
using HospitalApp.Data;
using HospitalApp.Models;
using HospitalApp.Repositories;

namespace HospitalApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public PatientService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            bool patientDelete = false;
            try
            {
                patientDelete = await _unitOfWork!.PatientRepository.DeleteAsync(id);
                _logger!.LogInformation("{Message}", "Patient with id: " + id + " deleted, success");
            }
            catch (Exception e) 
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return patientDelete;
        }

        public async Task<IEnumerable<User>> GetAllPatientsAsync()
        {
            List<User> usersPatients = new();
            try
            {
                usersPatients = await _unitOfWork!.PatientRepository.GetAllUsersPatientAsync();
                _logger!.LogInformation("{Message}", "All patients returned with success");
            } 
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return usersPatients;
        }

        public async Task<List<Appointment>> GetPatientAppointmentsAsync(int id)
        {
            List<Appointment> appointment = new();
            try
            {
                appointment = await _unitOfWork!.PatientRepository.GetPatientAppointmentAsync(id);
                _logger!.LogInformation("{Message}", "Patient count retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return appointment; 
        }

        public async Task<Patient?> GetPatientAsync(int id)
        {
            Patient? patient = null; 
            try
            {
                patient = await _unitOfWork!.PatientRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "Patient with id: " + id + " retrieved with success");
            } 
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return patient;
        }

        public async Task<int> GetPatientCountAsync()
        {
            int count = 0;
            try
            {
                count = await _unitOfWork!.PatientRepository.GetCountAsync();
                _logger!.LogInformation("{Message}", "Patient count retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return count;
        }
    }
}

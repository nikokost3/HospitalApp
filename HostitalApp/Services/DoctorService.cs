using AutoMapper;
using HospitalApp.Data;
using HospitalApp.Models;
using HospitalApp.Repositories;

namespace HospitalApp.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public DoctorService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsersDoctorsAsync()
        {
            List<User> usersDoctors = new();
            try
            {
                usersDoctors = await _unitOfWork!.DoctorRepository.GetAllUsersDoctorsAsync();
                _logger!.LogInformation("{Message}", "All teacher returned with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return usersDoctors;
        }

        public async Task<List<User>> GetAllUsersDoctorsAsync(int pageNumber, int pageSize)
        {
            List<User> usersDoctors = new();
            int skip = 1;
            try
            {
                skip = pageNumber * pageSize;
                usersDoctors = await _unitOfWork!.DoctorRepository.GetAllUsersDoctorsAsync(pageNumber, pageSize);
                _logger!.LogInformation("{Message}", "All doctors paginated returned with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return usersDoctors;
        }

        public async Task<User?> GetDoctorByUsernameAsync(string? username)
        {
            return await _unitOfWork!.DoctorRepository.GetDoctorByUsernameAsync(username!);
        }

        public Task<int> GetDoctorCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}

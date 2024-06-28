using AutoMapper;
using HospitalApp.Repositories;

namespace HospitalApp.Services
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;            
        }

        public UserService UserService => new(_unitOfWork, _logger, _mapper);

        public PatientService PatientService => new(_unitOfWork, _logger, _mapper);

        public DoctorService DoctorService => new(_unitOfWork, _logger, _mapper);
    }
}

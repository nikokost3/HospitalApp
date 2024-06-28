using AutoMapper;
using HospitalApp.Data;
using HospitalApp.DTO;
using HospitalApp.Models;
using HospitalApp.Repositories;
using HospitalApp.Security;
using HospitalApp.Services.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public UserService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task SignUpUserAsync(UserSignupDTO signupDTO)
        {
            Patient? patient;
            Doctor? doctor;
            User? user;

            try
            {
                user = ExtractUser(signupDTO);
                User? existingUser = await _unitOfWork!.UserRepository.GetByUsernameAsync(user.Username!);

                if (existingUser != null)
                {
                    throw new UserAlreadyExistsException("UserExists: " + existingUser.Username);
                }

                user.Password = EncryptionUtil.Encrypt(user.Password!);
                await _unitOfWork!.UserRepository.AddAsync(user);

                if (user.UserRole == UserRole.Patient)
                {
                    patient = ExtractPatient(signupDTO);
                    if (await _unitOfWork!.PatientRepository.GetByPhoneNumber(patient.PhoneNumber) is not null)
                    {
                        throw new PatientAlreadyExistsException("PatientPhoneNumberExists");
                    }
                    await _unitOfWork!.PatientRepository.AddAsync(patient);
                    user.Patient = patient;
                }
                else if (user.UserRole == UserRole.Doctor)
                {
                    doctor = ExtractDoctor(signupDTO);
                    if (await _unitOfWork!.DoctorRepository.GetByPhoneNumber(doctor.PhoneNumber) is not null)
                    {
                        throw new DoctorAlreadyExistsException("DoctorPhoneNumberExists");
                    }
                    await _unitOfWork!.DoctorRepository.AddAsync(doctor);
                    user.Doctor = doctor;
                }
                else
                {
                    throw new InvalidRoleException("InvalidRole");
                }

                await _unitOfWork!.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " signup success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }


        public async Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO)
        {
            List<User> users = new();
            List<Func<User, bool>> predicates = new();

            try
            {
                if (!string.IsNullOrEmpty(userFiltersDTO.Username))
                {
                    predicates.Add(u => u.Username == userFiltersDTO.Username);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.Email))
                {
                    predicates.Add(u => u.Email == userFiltersDTO.Email);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.UserRole))
                {
                    predicates.Add(u => u.UserRole!.Value.ToString() == userFiltersDTO.UserRole);
                }
                users = await _unitOfWork!.UserRepository.GetAllUsersFilteredAsync(pageNumber, pageSize, predicates);
                _logger!.LogInformation("{Message}", "User Filtered Returned Success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user;
            try
            {
                user = await _unitOfWork!.UserRepository.GetByUsernameAsync(username);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, UserDTO userDTO)
        {
            User? existingUser;
            User? user;
            try
            {
                existingUser = await _unitOfWork!.UserRepository.GetAsync(userId);
                if (existingUser == null) return null;

                var userToUpdate = _mapper!.Map<User>(userDTO);

                user = await _unitOfWork.UserRepository.UpdateUserAsync(userId, userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }
        public async Task<User?> UpdateUserPatchAsync(int userId, UserPatchDTO userPatchDTO)
        {
            User? existingUser;
            User? user = null;
            try
            {
                existingUser = await _unitOfWork!.UserRepository.GetAsync(userId);
                if (existingUser == null) return null;

                existingUser.Username = userPatchDTO.Username;
                existingUser.Email = userPatchDTO.Email;
                existingUser.Password = EncryptionUtil.Encrypt(userPatchDTO.Password!);

                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO request)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork!.UserRepository.GetUserAsync(request.Username!, request.Password!);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        
        private User ExtractUser(UserSignupDTO signupDTO)
        {
            return new User()
            {
                Username = signupDTO.Username,
                Password = signupDTO.Password,
                Email = signupDTO.Email,
                Firstname = signupDTO.Firstname,
                Lastname = signupDTO.Lastname,
                UserRole = signupDTO.UserRole
            };
        }

        private Patient ExtractPatient(UserSignupDTO? signupDTO)
        {
            return new Patient()
            {
                PhoneNumber = signupDTO!.PhoneNumber!,
                Clinic = signupDTO!.Clinic,
            };
        }

        private Doctor ExtractDoctor(UserSignupDTO? signupDTO)
        {
            return new Doctor()
            {
                PhoneNumber = signupDTO!.PhoneNumber!,
                Clinic = signupDTO!.Clinic,
            };
        }

        public async Task<User?> UpdateUserAsync(int userId, User userDTO)
        {
            User? existingUser;
            User? user;
            try
            {
                existingUser = await _unitOfWork!.UserRepository.GetAsync(userId);
                if (existingUser == null) return null;

                var userToUpdate = _mapper!.Map<User>(userDTO);

                user = await _unitOfWork.UserRepository.UpdateUserAsync(userId, userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> GetUserById(int id)
        {
            User? user;
            try
            {
                user = await _unitOfWork!.UserRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "User with id: " + id + " Success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }
    }
}
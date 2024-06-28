using AutoMapper;
using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDoctorsPatientDbContext _context;

        public UnitOfWork(UsersDoctorsPatientDbContext context) 
        {
            _context = context;
           
        }

        public UserRepository UserRepository => new(_context); 

        public DoctorRepository DoctorRepository => new(_context);

        public PatientRepository PatientRepository => new(_context);

        public AppointmentRepository AppointmentRepository => new(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalApp.Data;
using HospitalApp.DTO;
using HospitalApp.Models;
using HospitalApp.Security;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

		public UserRepository(UsersDoctorsPatientDbContext context) : base(context)
		{
		}


        public async Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicates)
        {
            int skip = pageSize * pageNumber;
            IQueryable<User> query = _context.Users.Skip(skip).Take(pageSize);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            return await query.ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users
                        .Where(x => x.Username == username)
                        .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username || x.Email == username);
            if (user == null)
            {
                return null;
            }
            if (!EncryptionUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, User user)
        {
            var existingUser = await _context.Users
                                .Where(x => x.Id == userId)
                                .FirstOrDefaultAsync();

            if (existingUser is null) return null;
            if (existingUser.Id != userId) return null;

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User> entityEntry = _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;

            return existingUser;
        }


    }
}

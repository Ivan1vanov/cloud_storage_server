using CloudStorage.Contexts;
using CloudStorage.Interfaces;
using CloudStorage.Entity;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MsDatabaseContext _context;

        public UserRepository(MsDatabaseContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == new Guid(id));
        }

        public async Task<User?> GetByEmailAddress(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> DoesUserExistByEmail(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User> CreateUser(User user)
        {
            var some = await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return some.Entity;
        }
    }
}

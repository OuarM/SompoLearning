using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user =
                await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }
    }
}

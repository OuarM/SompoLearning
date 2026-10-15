using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();

        Task<User?> GetUser(int id);

        Task<User> CreateUser(User user);

        Task DeleteUser(int id);
    }
}
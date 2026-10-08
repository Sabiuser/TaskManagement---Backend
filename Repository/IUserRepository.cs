using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User?> GetByIdAsync(int id);
    }
}

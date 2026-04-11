using UserServiceAPI.Models;

namespace UserServiceAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
    }
}

using UserServiceAPI.Models;

namespace UserServiceAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO?> GetByIdAsync(int id);
    }
}

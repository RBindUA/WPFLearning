using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Data;
using UserService.User;

namespace UserServiceAPI.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.BusinessEntityID == id);
        }
    }
}

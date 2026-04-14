using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Data;
using UserServiceAPI.Models;

namespace UserServiceAPI.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            return await (from p in _context.Users
                          join e in _context.EmailAddresses on p.BusinessEntityID equals e.BusinessEntityID
                          join bea in _context.BusinessEntityAddresses on p.BusinessEntityID equals bea.BusinessEntityID
                          join adr in  _context.Addresses on bea.AddressID equals adr.AddressID
                          where p.BusinessEntityID == id
                          select new UserDTO
                          {
                              BusinessEntityID = p.BusinessEntityID,
                              FirstName = p.FirstName,
                              LastName = p.LastName,
                              Email = e.EmailAddress,
                              Address = adr.AddressLine1,
                              City = adr.City,
                              PostalCode = adr.PostalCode

                          }).FirstOrDefaultAsync();
               
        }
    }
}

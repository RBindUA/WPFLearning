using ServiceIdentityAPI.Data;
using ServiceIdentityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceIdentityAPI.Services
{
    public class RegisterService
    {
        private readonly IdentityDbContext _context;

        public RegisterService(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            //Salt generation
            var salt = Guid.NewGuid().ToString().Substring(0, 10);

            var hash = CreateHash(password, salt);

            var newUser = new UserIdentity
            {
                //NOT WORKING. need to create new ID and parse to several tables in AdventureWorks
                //Need to add basic funtions first then do that 
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.UserIdentitiy.Add(newUser);
            return await _context.SaveChangesAsync() > 0;
        }
        private string CreateHash(string password, string salt)
        {
            //Basic  password+salt encryption 
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var combined = password + salt;
            var bytes = System.Text.Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}

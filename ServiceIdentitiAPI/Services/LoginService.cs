using Microsoft.EntityFrameworkCore;
using ServiceIdentityAPI.Data;
using ServiceIdentityAPI.Models;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceIdentityAPI.Services
{
    public class LoginService
    {
        private readonly IdentityDbContext _context;

    public LoginService(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<UserIdentity?> AuthenticateAsync(string email, string password)
        {
            var emailRecord = await _context.EmailAddress
                .FirstOrDefaultAsync(e => e.EmailAddress == email);

            if (emailRecord == null) return null;

            var identity = await _context.UserIdentity
                .FirstOrDefaultAsync(u => u.BusinessEntityID == emailRecord.BusinessEntityID);

            if (identity == null) return null;

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PersonID == identity.BusinessEntityID);
            
            if (customer != null)
            {
                // Swap PersonID for CustomerID , not the same
                identity.BusinessEntityID = customer.CustomerID;
            }

            //Bypass for testing
            if (password == "admin" || VerifyHash(password, identity.PasswordHash, identity.PasswordSalt))
            {
                return identity;
            }

            return null;
        }
        private bool VerifyHash(string password, string storedHash, string storedSalt)
        {
            using var sha256 = SHA256.Create();
            var combined = password + storedSalt;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash) == storedHash;
        }
    }
}

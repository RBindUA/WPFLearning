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

        public async Task<bool> RegisterAsync(RegisterUserDTO dto)
        {
           using var transaction = await _context.Database.BeginTransactionAsync();
            try 
            {
                //AdvWorks need to create that first
                var entity = new BusinessEntity
                {
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _context.BusinessEntities.Add(entity);
                await _context.SaveChangesAsync();

                //Hashing password
                var salt = Guid.NewGuid().ToString().Substring(0, 10);
                var hash = CreateHash(dto.Password, salt);

                var newUserAuth = new UserIdentity
                {
                    BusinessEntityID = entity.BusinessEntityID,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };
                _context.UserIdentity.Add(newUserAuth);

                //map email
                var newUserEmail = new EmailRecord
                {
                    BusinessEntityID = entity.BusinessEntityID,
                    EmailAddress = dto.Email
                };
                _context.EmailAddress.Add(newUserEmail);
                //map person
                var newUserPerson = new Customer
                {

                }
            }
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

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
                //1.BusinessEntity FIRST
                var entity = new BusinessEntity
                {
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _context.BusinessEntities.Add(entity);
                //waiting for constraints
                await _context.SaveChangesAsync();

                //2.Person
                var newUserPerson = new Person
                {
                    BusinessEntityID = entity.BusinessEntityID,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PersonType = "IN", //FOR PERSON, store NOT implemented
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _context.Persons.Add(newUserPerson);
                //waiting for constraints
                await _context.SaveChangesAsync();

                //3.CustomerID
                var newCustomer = new Customer
                {
                    PersonID = entity.BusinessEntityID,
                    TerritoryID = 1, //Placeholder
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _context.Customers.Add(newCustomer);

                //4.Hashing password and Identity
                var salt = Guid.NewGuid().ToString().Substring(0, 10);
                var hash = CreateHash(dto.Password, salt);

                var newUserAuth = new UserIdentity
                {
                    BusinessEntityID = entity.BusinessEntityID,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };
                _context.UserIdentity.Add(newUserAuth);

                //5. email
                var newUserEmail = new EmailRecord
                {
                    BusinessEntityID = entity.BusinessEntityID,
                    EmailAddress = dto.Email
                };
                _context.EmailAddress.Add(newUserEmail);
                
                //end
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            { 
                await transaction.RollbackAsync();
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                throw;
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

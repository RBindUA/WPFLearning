using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceIdentityAPI.Models
{
    public class UserIdentity
    {
        public string Email { get; set; }
        //Person.Password
        public int BusinessEntityID { get; set; }
        // AdventureWorks uses SHA1 or SHA256 + Salt need to try 
        //nvarchar 128
        public string PasswordHash { get; set; }
        //nvarchar 10
        public string PasswordSalt { get; set; }
    }
}

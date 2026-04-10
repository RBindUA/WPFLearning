using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceIdentityAPI.Models
{
    public class AuthResponse
    {
        //JWT
        public string Token { get; set; }
        public int BussinesEntityID { get; set; }
        //probably not going to be tested, but we`ll see
        public DateTime Expiratin {  get; set; }

    }
}

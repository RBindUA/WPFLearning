using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Learning.Services
{
    public static class UserSession
    {
        /*PORT  
         * Order https://localhost:52635
         * User https://localhost:55646
         * Identity https://localhost:55648 
         */
        public static string? Token { get; set; }
        public static int BusinessEntityID { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Token);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Security
{
    public class JwtToken
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Expiration { get; set; }
    }
}

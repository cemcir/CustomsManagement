using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Security
{
    public class Tokens
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; } 

    }
}

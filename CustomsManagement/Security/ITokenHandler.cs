using CustomersManagement.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Security
{
    public interface ITokenHandler
    {
         JwtToken GenerateJwtToken(Users user);
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomersManagement.Models;
using CustomersManagement.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomersManagement.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly Tokens _tokens;

        public TokenController(IOptions<Tokens> tokens)
        {
            _tokens = tokens.Value;
        }

        [HttpPost("new")]
        public IActionResult GetToken([FromBody] LoginResource user)
        {
            Console.WriteLine("User name:{0}", user.Email);
            Console.WriteLine("Password:{0}", user.Password);

            if (IsValidUserAndPassword(user.Email, user.Password))
                return new ObjectResult(GenerateToken(user.Email));

            return Unauthorized();
        }

        private string GenerateToken(string email)
        {
            var someClaims = new Claim[] {
                
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };

            SecurityKey securityKey = new SymmetricSecurityKey  (Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"));
            var token = new JwtSecurityToken(
                //issuer: "west-world.fabrikam.com",
                //audience: "heimdall.fabrikam.com",
                audience:this._tokens.Audience,
                issuer:this._tokens.Issuer,
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsValidUserAndPassword(string userName, string password)
        {
            //Sürekli true dönüyor. Normalde bir Identity mekanizması ile entegre etmemiz lazım.
            return true;
        }
    }
}
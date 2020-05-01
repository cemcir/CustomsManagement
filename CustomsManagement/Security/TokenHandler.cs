using CustomersManagement.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomersManagement.Security
{
    public class TokenHandler:ITokenHandler
    {
        private readonly Tokens _tokens;
        public TokenHandler(IOptions<Tokens> tokens) {
            this._tokens = tokens.Value;
        }

        public JwtToken GenerateJwtToken(Users user) {
            var someClaims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                //new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                //new Claim(JwtRegisteredClaimNames.Email,"heimdall@mail.com"),
                //new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"));
            var token = new JwtSecurityToken(
                audience: this._tokens.Audience,
                issuer: this._tokens.Issuer,
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtToken {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo.ToString(),
                Email = user.Email
            };
        }
    }
}

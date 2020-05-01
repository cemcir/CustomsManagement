using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomersManagement.Domain;
using CustomersManagement.Encryption;
using CustomersManagement.Models;
using CustomersManagement.Models.Reponse;
using CustomersManagement.Models.Resource;
using CustomersManagement.Repository;
using CustomersManagement.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomersManagement.Controllers
{
    [Route("user")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //private readonly Tokens _tokens;
        private readonly IUserRepository _userRepo;
        private readonly ITokenHandler _tokenHandler;
        private readonly ILogger _logger;

        public AuthenticateController(IUserRepository userRepository,ITokenHandler tokenHandler,ILoggerFactory loggerFactory)
        {
            _tokenHandler = tokenHandler;
            _userRepo = userRepository;
            _logger = loggerFactory.CreateLogger(nameof(AuthenticateController));
        }

        [HttpPost("authenticate")]
        public IActionResult UserAuthenticate([FromBody] LoginResource user) {
            //Console.WriteLine("User name:{0}", user.Username);
            //Console.WriteLine("Password:{0}", user.Password);
            try {
                user.Email = EncryptionMD5.EncryptionByMD5(user.Email);
                user.Password = EncryptionMD5.EncryptionByMD5(user.Password);
                var users = this._userRepo.FindByUserNameAndPassword(user.Email, user.Password);
                if (users != null) {
                    var jwtToken = this._tokenHandler.GenerateJwtToken((users));
                    return Ok(new ApiResponse<JwtToken> { Status = true, Entry = jwtToken });
                }
                return BadRequest(new ApiResponse<JwtToken> {Status=false,Entry=null });
            }
            catch(Exception ex) {
                _logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<JwtToken>{ Status = false, Entry = null });
            }
            // if (IsValidUserAndPassword(user.UserName, user.Password))
            //    return new ObjectResult(GenerateToken(users));
            //return Unauthorized();
        }
        
         
        /*
        private JwtToken GenerateToken(User user) {
            var someClaims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
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
            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo.ToString(),
                UserName = user.UserName
            };
        }
        */
        
    }
}
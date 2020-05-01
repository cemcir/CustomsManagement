using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Encryption;
using CustomersManagement.Extensions;
using CustomersManagement.Models.Reponse;
using CustomersManagement.Models.Resource;
using CustomersManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomersManagement.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserRepository userRepo;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, ILoggerFactory loggerFactory,IMapper mapper) {
            this.userRepo = userRepository;
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger(nameof(UserController));
        }

        [HttpPost("adduser")]
        public IActionResult AddUser(UserResource userResource) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.GetErrorMessages());
                }
                else {
                    var users = this.userRepo.FindByEmail(userResource.Email);
                    if (users == null) {
                        Users user = mapper.Map<UserResource, Users>(userResource);
                        user.Password = EncryptionMD5.EncryptionByMD5(user.Password);
                        user.Email = EncryptionMD5.EncryptionByMD5(userResource.Email);
                        var u=this.userRepo.AddUser(user);
                        return Ok(new ApiResponse<Users> {Status=true,Entry=u});
                    }
                    return BadRequest(new ApiResponse<Users> {Status=false,Entry=null });
                }
            }
            catch (Exception ex) {
                logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Users> { Status = false, Entry = null });
            }
        }

        [HttpGet("getusers")]
        [Authorize]
        public IActionResult GetUsers() {
            try {
                IEnumerable<Claim> claims = User.Claims;
                string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

                Users users = this.userRepo.FindById(Int32.Parse(userId));
                if (users != null){
                    return Ok(new ApiResponse<Users> { Status = true, Entry = users });
                }
                return BadRequest(new ApiResponse<Users> { Status = false, Entry = null });
            }
            catch (Exception ex) {
                logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Users> { Status = false, Entry = null });
            }
           
        }
    }
}
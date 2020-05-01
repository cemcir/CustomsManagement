using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersManagement.Controllers
{
    [Authorize()]
    [Route("promotion")]
    public class PromotionController : Controller
    {
        [HttpGet("new")]
        public IActionResult Get()
        {
            foreach (var claim in HttpContext.User.Claims)
            {
                Console.WriteLine("Claim Type: {0}:\nClaim Value:{1}\n", claim.Type, claim.Value);
            }
            var promotionCode = Guid.NewGuid();
            return new ObjectResult($"Your promotion code is {promotionCode}");
        }
    }
}
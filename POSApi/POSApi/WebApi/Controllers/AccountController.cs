using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AccountController(IConfiguration configuration)
        {
            


        }

        private string CreateJWToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {

                new Claim ("id", "" + user.Id),
                new Claim ("role", user.ROLE),

            };
        }
    }
}

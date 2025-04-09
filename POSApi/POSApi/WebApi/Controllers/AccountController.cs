using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POSApi.Domain.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /*
        [HttpGet("TestToken")]
        public IActionResult TestToken()
        {
            User user = new User() {Id = 2, ROLE = "admin"};
            string jwt = CreateJWToken(user);
            var response = new { JWToken = jwt };
            return Ok(response);
        }
        */

        private string CreateJWToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {

                new Claim ("id", "" + user.Id),
                new Claim ("role", user.ROLE),

            };

            string strKey = configuration["Jwt:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds

                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}




// ||
// {}
//  <>
//  []


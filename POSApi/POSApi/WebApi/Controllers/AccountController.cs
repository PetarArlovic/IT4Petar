using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POSApi.Application.DTO.UserDTO;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;
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
        private readonly AppDbContext context;

        public AccountController(IConfiguration configuration, AppDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }


        /*[HttpPost("Register")]
        public IActionResult Register(UserDTO dto)
        {
            var emailCount = context.USER.Count(u => u.EMAIL == dto.EMAIL);
            if (emailCount > 0)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return BadRequest("Email already exists");
            }

            var passwordHasher = new PasswordHasher<User>();
            var encryptedPassword = passwordHasher.HashPassword(new User(), dto.PASSWORD);


            User user = new User()
            {

                NAZIV = dto.NAZIV,
                EMAIL = dto.EMAIL,
                PASSWORD = encryptedPassword,
                ROLE = "client",
                CREATED_AT = DateTime.Now

            };

            context.USER.Add(user);
            context.SaveChanges();

            var jwt = CreateJWToken(user);

            UserProfileDTO userProfileDTO = new UserProfileDTO()
            {
                Id = user.Id,
                NAZIV = user.NAZIV,
                EMAIL = user.EMAIL,
                ROLE = user.ROLE,
                CREATED_AT = user.CREATED_AT
            };

            var reponse = new
            {
                UserProfile = userProfileDTO,
                JWToken = jwt
            };

            return Ok(reponse);
        }


        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {

            var user = context.USER.FirstOrDefault(u => u.EMAIL == email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return BadRequest("Email not found");
            }

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(new Domain.Models.User(), user.PASSWORD, password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return BadRequest("Invalid password");
            }

            var jwt = CreateJWToken(user);

            UserProfileDTO userProfileDTO = new UserProfileDTO()
            {
                Id = user.Id,
                NAZIV = user.NAZIV,
                EMAIL = user.EMAIL,
                ROLE = user.ROLE,
                CREATED_AT = user.CREATED_AT
            };

            var reponse = new
            {
                UserProfile = userProfileDTO,
                JWToken = jwt
            };

            return Ok(reponse);

        }


        [Authorize]
        [HttpGet("AuthoriseAuthentificatedUsers")]
        public IActionResult AuthoriseAuthentificatedUsers()
        {
            return Ok("You are authorised");
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

            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
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
// =>

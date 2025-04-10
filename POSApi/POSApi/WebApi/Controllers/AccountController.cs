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

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var user = new User 
            {
                UserName = dto.EMAIL,
                Email = dto.EMAIL,
                Ime = dto.IME,
                Prezime = dto.PREZIME
            };

            var result = await _userManager.CreateAsync(user, dto.PASSWORD);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok("User registered.");
            }

            return BadRequest(result.Errors);

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.EMAIL);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.PASSWORD))
            {
                var token = CreateJWToken(user);
                return Ok(new { token });
            }

            return Unauthorized("Invalid credentials.");

        }


        private string CreateJWToken(User user)
        {

            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, "User")
        };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }     
    }
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

        





// ||
// {}
//  <>
//  []
// =>

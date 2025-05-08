using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using POSApi.Application.DTO.UserDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace POSApi.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<User> _logger;

        public AccountService(UserManager<User> userManager, IConfiguration config, ILogger<User> logger)
        {

            _userManager = userManager;
            _config = config;
            _logger = logger;

        }


        public async Task<(bool Succeeded, IEnumerable<IdentityError> Errors)> RegisterUserAsync(RegisterDTO dto)
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
            }

            return (result.Succeeded, result.Errors);
        }


        public async Task<string> LogInUserAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.EMAIL);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.PASSWORD))
            {
                var token = await CreateJWTokenAsync(user);
                _logger.LogWarning("User logged in.");
                return token;
            }

            _logger.LogWarning("Invalid credentials for user: {Email}", dto.EMAIL);
            return null;

        }


        private async Task<string> CreateJWTokenAsync(User user)
        {

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}



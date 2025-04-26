using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POSApi.Application.DTO.UserDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSApi.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILogger<User> _logger;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, ILogger<User> logger, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
            _context = context;
        }


        public async Task<(bool Succeeded, IEnumerable<IdentityError> Errors)> RegisterUserAsync([FromBody] RegisterDTO dto)
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

                var kupac = new Kupac
                {
                    NAZIV = $"{dto.IME} {dto.PREZIME}",
                    ADRESA = "_",
                    MJESTO = "_",
                    SIFRA = new Random().Next(1000, 9999),
                    UserId = user.Id
                };

                _context.KUPAC.Add(kupac);
                await _context.SaveChangesAsync();
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



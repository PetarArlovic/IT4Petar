using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using POSApi.Application.DTO.UserDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {

            var user = await _accountService.RegisterUserAsync(dto);

            if (user.Succeeded)
                return Ok(new { message = "User registered successfully." });

            return BadRequest(user.Errors);

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {

            var token = await _accountService.LogInUserAsync(dto);

            if (token == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(new { token });

        }   
    }
}        


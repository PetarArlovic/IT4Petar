using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.UserDTO;
using POSApi.Application.Services.Interfaces;


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


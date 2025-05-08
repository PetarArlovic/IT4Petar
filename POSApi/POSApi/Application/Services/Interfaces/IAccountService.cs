using Microsoft.AspNetCore.Identity;
using POSApi.Application.DTO.UserDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IAccountService
    {

        Task<(bool Succeeded, IEnumerable<IdentityError> Errors)> RegisterUserAsync(RegisterDTO dto);
        Task<string> LogInUserAsync(LoginDTO dto);

    }
}

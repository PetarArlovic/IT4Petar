using System.ComponentModel.DataAnnotations;


namespace POSApi.Application.DTO.UserDTO
{
    public class LoginDTO
    {

        [Required]
        [MaxLength(100)]
        public string EMAIL { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string PASSWORD { get; set; } = "";

    }
}

using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.UserDTO
{
    public class UserDTO
    {

        [Required]
        [MaxLength(100)]
        public string NAZIV { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string EMAIL { get; set; } = "";

        [Required]
        [MaxLength(100), MinLength(8)]
        public string PASSWORD { get; set; } = "";

    }
}

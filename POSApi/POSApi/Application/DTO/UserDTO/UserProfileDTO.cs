using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.UserDTO
{
    public class UserProfileDTO
    {

        public int Id { get; set; }

        public string NAZIV { get; set; } = "";

        public string EMAIL { get; set; } = "";

        public string ROLE { get; set; } = "";

        public DateTime CREATED_AT { get; set; }

    }
}

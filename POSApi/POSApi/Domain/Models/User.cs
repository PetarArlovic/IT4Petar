using System.ComponentModel.DataAnnotations;

namespace POSApi.Domain.Models
{
    public class User
    {

        public int Id { get; set; }

        [MaxLength (100)]
        public string NAZIV { get; set; } = "";

        [MaxLength(100)]
        public string EMAIL { get; set; } = "";

        [MaxLength(100)]
        public string PASSWORD { get; set; } = "";

        [MaxLength(30)]
        public string ROLE { get; set; } = "";

        public DateTime CREATED_AT { get; set; }

    }
}

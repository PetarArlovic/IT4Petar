using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.KupacDTO
{
    public class GetKupacDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int sifra { get; set; }

        [MaxLength(100)]
        [Required]
        public string naziv { get; set; }

        [MaxLength(100)]
        [Required]
        public string adresa { get; set; }

        [MaxLength(100)]
        [Required]
        public string mjesto { get; set; }

        public string UserId { get; set; }

    }
}

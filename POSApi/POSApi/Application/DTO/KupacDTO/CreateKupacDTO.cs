using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.KupacDTO
{
    public class CreateKupacDTO
    {

        [Required]
        public int SIFRA { get; set; }

        [MaxLength(100)]
        [Required]
        public string NAZIV { get; set; }

        [MaxLength(100)]
        [Required]
        public string ADRESA { get; set; }

        [MaxLength(100)]
        [Required]
        public string MJESTO { get; set; }

    }
}

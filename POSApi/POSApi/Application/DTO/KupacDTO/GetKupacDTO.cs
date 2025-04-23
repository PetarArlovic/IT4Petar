using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.KupacDTO
{
    public class GetStavke_racuanDTO
    {

        [Required]
        public int Id { get; set; }

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

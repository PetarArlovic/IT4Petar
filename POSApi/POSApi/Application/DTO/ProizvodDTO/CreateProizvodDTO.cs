using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace POSApi.Application.DTO.ProizvodDTO
{
    public class CreateProizvodDTO
    {

        [Required]
        public int sifra { get; set; }

        [MaxLength(100)]
        [Required]
        public string naziv { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [MaxLength(30)]
        [Required]
        public string jedinica_mjere { get; set; }

        [Precision(20, 2)]
        [Required]
        public decimal cijena { get; set; }

        [Required]
        public int stanje { get; set; }

        [Precision(20, 2)]
        public decimal popust { get; set; }

        [Required]
        public string proizvodSlikaUrl { get; set; }

    }
}

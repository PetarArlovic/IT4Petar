using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.ProizvodDTO
{
    public class UpdateProizvodDTO
    {

        [Required]
        public int SIFRA { get; set; }

        [MaxLength(100)]
        [Required]
        public string NAZIV { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [MaxLength(30)]
        [Required]
        public string JEDINICA_MJERE { get; set; }

        [Precision(20, 2)]
        [Required]
        public decimal CIJENA { get; set; }

        [Required]
        public int STANJE { get; set; }

        [Required]
        public string PROIZVODSlikaUrl { get; set; }

    }
}

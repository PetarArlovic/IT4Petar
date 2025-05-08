using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace POSApi.Domain.Models
{
    public class Proizvod
    {

        [Key]
        public int Id { get; set; }

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

        [Precision(20, 2)]
        public decimal POPUST { get; set; }

        [Required]
        public string PROIZVODSlikaUrl { get; set; }


        public ICollection<Stavke_racuna> STAVKE_RACUNA { get; set; }

    }
}

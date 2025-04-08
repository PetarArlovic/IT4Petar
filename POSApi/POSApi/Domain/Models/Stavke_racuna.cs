using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Domain.Models
{
    public class Stavke_racuna
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int KOLICINA { get; set; }

        [Required]
        [Precision(20, 2)]
        public decimal CIJENA { get; set; }

        [Precision(20, 2)]
        public decimal POPUST { get; set; }

        [Precision(20, 2)]
        public decimal IZNOS_POPUSTA => CIJENA * KOLICINA * (POPUST / 100);

        [Precision(20, 2)]
        public decimal VRIJEDNOST => (CIJENA * KOLICINA) - IZNOS_POPUSTA;

        [ForeignKey("PROIZVODId")]
        public int PROIZVODId { get; set; }

        public Proizvod PROIZVOD { get; set; }

        public int ZAGLAVLJE_RACUNAId { get; set; }

        [ForeignKey("ZAGLAVLJE_RACUNAId")]
        public Zaglavlje_racuna ZAGLAVLJE_RACUNA { get; set; }

        public string BROJ => ZAGLAVLJE_RACUNA != null ? ZAGLAVLJE_RACUNA.BROJ.ToString() : string.Empty;

    }
}

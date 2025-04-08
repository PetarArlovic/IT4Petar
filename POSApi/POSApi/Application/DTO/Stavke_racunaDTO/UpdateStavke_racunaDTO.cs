using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Stavke_racunaDTO
{
    public class UpdateStavke_racunaDTO
    {

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

        public int PROIZVODId { get; set; }

        public int ZAGLAVLJE_RACUNAId { get; set; }

        public int BROJ { get; set; }

    }
}

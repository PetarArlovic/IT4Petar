using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Stavke_racunaDTO
{
    public class CreateStavke_racunaDTO
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

        public int BROJ { get; set; }

        public int SIFRA { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using POSApi.Application.DTO.ProizvodDTO;
using System.ComponentModel.DataAnnotations;


namespace POSApi.Application.DTO.Stavke_racunaDTO
{
    public class GetStavke_racunaDTO
    {

        [Required]
        public int kolicina { get; set; }

        [Required]
        [Precision(20, 2)]
        public decimal cijena { get; set; }

        [Precision(20, 2)]
        public decimal popust { get; set; }

        [Precision(20, 2)]
        public decimal iznos_popusta => cijena * kolicina * (popust / 100);

        [Precision(20, 2)]
        public decimal vrijednost => (cijena * kolicina) - iznos_popusta;

        public int proizvodId { get; set; }

        public int broj { get; set; }

        public GetProizvodDTO proizvod { get; set; }

    }
}

using POSApi.Application.DTO.KupacDTO;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Zaglavlje_racunaDTO
{
    public class GetZaglavlje_racunaDTO
    {

        [Required]
        public int broj { get; set; }

        [Required]
        public DateTime datum { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string napomena { get; set; }

        public int kupacId { get; set; }

        public GetKupacDTO kupac { get; set; }

    }
}

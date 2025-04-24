using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Zaglavlje_racunaDTO
{
    public class UpdateZaglavlje_racunaDTO
    {

        [Required]
        public int broj { get; set; }

        [MaxLength(100)]
        public string napomena { get; set; }

        public int kupacId { get; set; }

    }
}

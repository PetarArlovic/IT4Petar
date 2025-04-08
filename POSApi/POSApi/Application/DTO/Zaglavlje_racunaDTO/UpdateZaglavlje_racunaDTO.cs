using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Zaglavlje_racunaDTO
{
    public class UpdateZaglavlje_racunaDTO
    {

        [Required]
        public int BROJ { get; set; }

        [Required]
        public DateTime DATUM { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string NAPOMENA { get; set; }

        public int KUPACId { get; set; }

    }
}

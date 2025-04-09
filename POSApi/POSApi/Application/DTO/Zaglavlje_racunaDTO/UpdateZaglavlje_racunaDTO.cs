using System.ComponentModel.DataAnnotations;

namespace POSApi.Application.DTO.Zaglavlje_racunaDTO
{
    public class UpdateZaglavlje_racunaDTO
    {

        [Required]
        public int BROJ { get; set; }

        [MaxLength(100)]
        public string NAPOMENA { get; set; }

        public int KUPACId { get; set; }

    }
}

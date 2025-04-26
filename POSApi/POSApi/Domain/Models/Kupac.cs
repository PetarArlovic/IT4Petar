using System.ComponentModel.DataAnnotations;

namespace POSApi.Domain.Models
{
    public class Kupac
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int SIFRA { get; set; }

        [MaxLength(100)]
        [Required]
        public string NAZIV { get; set; }

        [MaxLength(100)]
        [Required]
        public string ADRESA { get; set; }

        [MaxLength(100)]
        [Required]
        public string MJESTO { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Zaglavlje_racuna> ZAGLAVLJE_RACUNA { get; set; }

    }
}

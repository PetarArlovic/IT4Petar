using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Domain.Models
{
    public class Zaglavlje_racuna
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int BROJ { get; set; }

        [Required]
        public DateTime DATUM { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string NAPOMENA { get; set; }

        [ForeignKey("KUPAC")]
        public int KUPACId { get; set; }

        public Kupac KUPAC { get; set; }

        public ICollection<Stavke_racuna> STAVKE_RACUNA { get; set; }

    }
}

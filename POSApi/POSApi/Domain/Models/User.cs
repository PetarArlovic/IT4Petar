using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace POSApi.Domain.Models
{
    public class User : IdentityUser
    {

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public ICollection<Kupac> Kupci { get; set; }

    }
}

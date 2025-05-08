using Microsoft.AspNetCore.Identity;


namespace POSApi.Domain.Models
{
    public class User : IdentityUser
    {

        public string Ime { get; set; }

        public string Prezime { get; set; }

    }
}

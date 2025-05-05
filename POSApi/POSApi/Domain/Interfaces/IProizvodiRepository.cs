using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Domain.Models;

namespace POSApi.Domain.Interfaces
{
    public interface IProizvodiRepository
    {
        Task<Proizvod?> FindPBySIFRA(int sifra);
        Task<Proizvod?> FindProizvodByNaziv(string naziv);

    }
}

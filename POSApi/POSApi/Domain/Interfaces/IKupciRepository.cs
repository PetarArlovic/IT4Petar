using POSApi.Domain.Models;

namespace POSApi.Domain.Interfaces
{
    public interface IKupciRepository
    {
        Task<Kupac?> FindKBySIFRA(int sifra);

    }
}

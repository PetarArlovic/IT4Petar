using POSApi.Domain.Models;

namespace POSApi.Domain.Interfaces
{
    public interface IZaglavlje_racunaRepository
    {
        Task<Zaglavlje_racuna?> FindZByBROJ(int broj);
        Task<List<Zaglavlje_racuna>> GetAllZaglavljaAsync(); 
    }
}

using POSApi.Domain.Models;

namespace POSApi.Domain.Interfaces
{
    public interface IStavke_racunaRepository
    {
        Task<List<Stavke_racuna?>> GetStavkeByBROJ(int broj);

    }
}

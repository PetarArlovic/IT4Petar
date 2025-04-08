using POSApi.Domain.Models;

namespace POSApi.Domain.Interfaces
{
    public interface IGenericRepository
    {

        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<Kupac?> FindKBySIFRA(int sifra);
        Task<Proizvod?> FindPBySIFRA(int sifra);
        Task<Zaglavlje_racuna?> FindZByBROJ(int broj);
        Task<List<Stavke_racuna?>> GetStavkeByBROJ(int broj);
        Task<bool> SaveChangesAsync();

    }
}

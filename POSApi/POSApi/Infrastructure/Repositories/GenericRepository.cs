using POSApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;

namespace POSApi.Infrastructure.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<Kupac?> FindKBySIFRA(int sifra)
        {
            return await _context.KUPAC.FirstOrDefaultAsync(k => k.SIFRA == sifra);
        }

        public async Task<Proizvod?> FindPBySIFRA(int sifra)
        {
            return await _context.PROIZVOD.FirstOrDefaultAsync(k => k.SIFRA == sifra);
        }

        public async Task<Zaglavlje_racuna?> FindZByBROJ(int broj)
        {
            return await _context.ZAGLAVLJE_RACUNA.FirstOrDefaultAsync(k => k.BROJ == broj);
        }

        public async Task<List<Stavke_racuna?>> GetStavkeByBROJ(int broj)
        {

            return await _context.Set<Stavke_racuna>()
                .Include(s => s.ZAGLAVLJE_RACUNA)
                .Where(s => s.ZAGLAVLJE_RACUNA.BROJ == broj)
                .ToListAsync();
        }

    }
}

//  =>
// ||
// {}
//  <>
//  []
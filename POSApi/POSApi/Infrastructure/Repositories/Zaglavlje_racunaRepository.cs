using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;

namespace POSApi.Infrastructure.Repositories
{
    public class Zaglavlje_racunaRepository : IZaglavlje_racunaRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Zaglavlje_racuna> _dbSet;

        public Zaglavlje_racunaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Zaglavlje_racuna?> FindZByBROJ(int broj)
        {
            return await _context.ZAGLAVLJE_RACUNA.FirstOrDefaultAsync(k => k.BROJ == broj);
        }

        public async Task<List<Zaglavlje_racuna>> GetAllZaglavljaAsync()
        {
            return await _context.Set<Zaglavlje_racuna>()
                .Include(z => z.KUPAC)
                .ToListAsync();
        }
    }
}
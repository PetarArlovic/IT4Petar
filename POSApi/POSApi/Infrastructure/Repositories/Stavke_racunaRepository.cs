using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;


namespace POSApi.Infrastructure.Repositories
{
    public class Stavke_racunaRepository : IStavke_racunaRepository
    {
        private readonly AppDbContext _context;

        public Stavke_racunaRepository(AppDbContext context)
        {

            _context = context;

        }

        public async Task<List<Stavke_racuna?>> GetStavkeByBROJ(int broj)
        {

            return await _context.Set<Stavke_racuna>()
                .Include(s => s.ZAGLAVLJE_RACUNA)
                .Where(s => s.ZAGLAVLJE_RACUNA.BROJ == broj)
                .ToListAsync();

        }

        public async Task<List<Stavke_racuna>> GetAllStavkeAsync()
        {

            return await _context.Set<Stavke_racuna>()
                .Include(i => i.PROIZVOD)
                .ToListAsync();

        }
    }
}

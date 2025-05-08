using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;


namespace POSApi.Infrastructure.Repositories
{
    public class KupciRepository : IKupciRepository
    {
        private readonly AppDbContext _context;

        public KupciRepository(AppDbContext context)
        {

            _context = context;

        }

        public async Task<Kupac?> FindKBySIFRA(int sifra)
        {

            return await _context.KUPAC.FirstOrDefaultAsync(k => k.SIFRA == sifra);

        }
    }
}
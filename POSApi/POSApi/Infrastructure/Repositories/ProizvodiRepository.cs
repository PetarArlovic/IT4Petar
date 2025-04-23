using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;

namespace POSApi.Infrastructure.Repositories
{
    public class ProizvodiRepository : IProizvodiRepository
    {
        private readonly AppDbContext _context;

        public async Task<Proizvod?> FindPBySIFRA(int sifra)
        {
            return await _context.PROIZVOD.FirstOrDefaultAsync(k => k.SIFRA == sifra);
        }
    }
}

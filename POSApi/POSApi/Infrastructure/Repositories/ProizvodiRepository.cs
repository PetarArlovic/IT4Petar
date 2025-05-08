using Microsoft.EntityFrameworkCore;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Domain.Interfaces;
using POSApi.Domain.Models;
using POSApi.Infrastructure.Data;


namespace POSApi.Infrastructure.Repositories
{
    public class ProizvodiRepository : IProizvodiRepository
    {
        private readonly AppDbContext _context;

        public ProizvodiRepository(AppDbContext context)
        {

            _context = context;

        }

        public async Task<Proizvod?> FindPBySIFRA(int sifra)
        {

            return await _context.PROIZVOD.FirstOrDefaultAsync(k => k.SIFRA == sifra);

        }

        public async Task<Proizvod?> FindProizvodByNaziv(string naziv)
        {

            return await _context.PROIZVOD.FirstOrDefaultAsync(k => k.NAZIV == naziv);

        }
    }
}

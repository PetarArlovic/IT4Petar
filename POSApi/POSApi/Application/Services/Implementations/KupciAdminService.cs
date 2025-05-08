using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;


namespace POSApi.Application.Services.Implementations
{
    public class KupciAdminService : IKupciAdminService
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepository<Kupac> _repo;
        private readonly ILogger<KupciAdminService> _logger;
        private readonly IKupciRepository _kupacRepo;

        public KupciAdminService(IGenericRepository<Kupac> repo, IMapper mapper, ILogger<KupciAdminService> logger, IKupciRepository kupacRepo)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _kupacRepo = kupacRepo;

        }

        public async Task<GetKupacDTO> GetByIdAsync(int id)
        {
            try
            {

                var kupac = await _repo.GetByIdAsync(id);
                if (kupac == null)
                {
                    _logger.LogWarning("Kupac sa id-em: " + id + " ne postoji.");
                    throw new Exception("Kupac sa id-em: " + id + " ne postoji");
                }
                
                return _mapper.Map<GetKupacDTO>(kupac);
            }
            
            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja kupca");
                throw;

            }
        }


        public async Task<bool> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {
            try
            {

                var kupac = await _kupacRepo.FindKBySIFRA(sifra);

                if (kupac == null)
                {
                    _logger.LogWarning("Kupac sa sifrom: " + sifra + " ne postoji.");
                    throw new Exception("Kupac sa sifrom: " + sifra + " ne postoji");
                }

                _mapper.Map(dto, kupac);
                await _repo.UpdateAsync(kupac);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom ažuriranja kupca");
                throw;

            }

        }


        public async Task<bool> DeleteAsync(int sifra)
        {
            try
            {

                var kupac = await _repo.GetByIdAsync(sifra);

                if (kupac == null)
                {
                    _logger.LogWarning("Kupac sa id-em: " + sifra + " ne postoji.");
                    throw new Exception("Kupac sa id-em: " + sifra + " ne postoji");
                }

                await _repo.DeleteAsync(kupac);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom brisanja kupca");
                throw;

            }
        }


        public async Task<GetKupacDTO> FindKBySIFRA(int sifra)
        {
            try
            {

                var kupac = await _kupacRepo.FindKBySIFRA(sifra);

                if (kupac == null)
                {
                    _logger.LogWarning("Kupac sa sifrom-em: " + sifra + " ne postoji.");
                    throw new Exception("Kupac sa šifrom: " + sifra + " ne postoji");
                }
            
                return _mapper.Map<GetKupacDTO>(kupac);
            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja kupca");
                throw;

            }
        }

    }
}

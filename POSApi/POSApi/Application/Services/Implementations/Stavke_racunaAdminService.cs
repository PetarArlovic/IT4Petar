using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;


namespace POSApi.Application.Services.Implementations
{
    public class Stavke_racunaAdminService : IStavke_racunaAdminService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Stavke_racuna> _repo;
        private readonly ILogger<Stavke_racunaAdminService> _logger;

        public Stavke_racunaAdminService(IGenericRepository<Stavke_racuna> repo, IMapper mapper, ILogger<Stavke_racunaAdminService> logger)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<GetStavke_racunaDTO> GetByIdAsync(int id)
        {
            try
            {

                var stavke = await _repo.GetByIdAsync(id);

                if (stavke == null)
                {
                    _logger.LogWarning("Stavka sa id-jem: " + id + " ne postoji");
                    throw new Exception("Stavka sa id-jem: " + id + " ne postoji");
                }

                return _mapper.Map<GetStavke_racunaDTO>(stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja stavki");
                throw;

            }
        }
    }
}

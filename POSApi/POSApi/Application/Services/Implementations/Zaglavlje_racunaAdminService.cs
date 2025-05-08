using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;

namespace POSApi.Application.Services.Implementations
{
    public class Zaglavlje_racunaAdminService : IZaglavlje_racunaAdminService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Zaglavlje_racuna> _repo;
        private readonly ILogger<Zaglavlje_racunaAdminService> _logger;

        public Zaglavlje_racunaAdminService(IGenericRepository<Zaglavlje_racuna> repo, IMapper mapper, ILogger<Zaglavlje_racunaAdminService> logger)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<GetZaglavlje_racunaDTO> GetByIdAsync(int id)
        {
            try
            {

                var zaglavlje = await _repo.GetByIdAsync(id);
                if (zaglavlje == null)
                {
                    _logger.LogInformation("Zaglavlje_racuna sa id-jem:" + id +  "ne postoji");
                    throw new Exception("Zaglavlje_racuna sa id-jem: " + id + " ne postoji");
                }

                return _mapper.Map<GetZaglavlje_racunaDTO>(zaglavlje);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja zaglavlja:");
                throw;

            }
        }
    }
}

// ||
// {}
//  <>
//  []

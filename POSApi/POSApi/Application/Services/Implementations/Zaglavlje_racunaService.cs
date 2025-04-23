using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;

namespace POSApi.Application.Services.Implementations
{
    public class Zaglavlje_racunaService : IZaglavlje_racunaService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Zaglavlje_racuna> _repo;
        private readonly ILogger<Zaglavlje_racunaService> _logger;

        public Zaglavlje_racunaService(IGenericRepository<Zaglavlje_racuna> repo, IMapper mapper, ILogger<Zaglavlje_racunaService> logger)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<List<GetZaglavlje_racunaDTO>> GetAllAsync()
        {
            try
            {

                var zaglavlja = await _repo.GetAllAsync();
                _logger.LogInformation("Zaglavlja su uspješno učitana.");
                return _mapper.Map<List<GetZaglavlje_racunaDTO>>(zaglavlja);

            }

            catch (Exception ex) {

                _logger.LogError(ex, "Greška prilikom učitavanja zaglavlja:");
                throw;

            }
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


        public async Task<CreateZaglavlje_racunaDTO> AddAsync(CreateZaglavlje_racunaDTO dto)
        {

            try
            {

                var existingzaglavlje = await _repo.FindZByBROJ(dto.BROJ);

                if (existingzaglavlje != null)
                {
                    _logger.LogInformation("Zaglavlje racuna sa brojem" + dto.BROJ + " vec postoji");
                    throw new InvalidOperationException("Zaglavlje racuna sa brojem" + dto.BROJ + " vec postoji");
                }

                var zaglavlje = _mapper.Map<Zaglavlje_racuna>(dto);
                await _repo.AddAsync(zaglavlje);
                return _mapper.Map<CreateZaglavlje_racunaDTO>(zaglavlje);

            }

            catch (Exception ex) 
            {

                _logger.LogError(ex, "Greška prilikom dodavanja zaglavlja:");
                throw;

            }
        }


        public async Task<bool> UpdateAsync(int broj, UpdateZaglavlje_racunaDTO dto)
        {

            try
            {

                var zaglavlje = await _repo.FindZByBROJ(broj);

                if (zaglavlje == null)
                {
                    _logger.LogInformation("Zaglavlje racuna sa brojem" + broj + " ne postoji");
                    throw new Exception("Zaglavlje_racuna sa brojem: " + broj + " ne postoji");
                }

                _mapper.Map(dto, zaglavlje);
                await _repo.UpdateAsync(zaglavlje);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom ažuriranja zaglavlja:");
                throw;

            }
        }


        public async Task<bool> DeleteAsync(int broj)
        {

            try
            {

                var zaglavlje = await _repo.FindZByBROJ(broj);
                if (zaglavlje == null)
                {
                    _logger.LogInformation("Zaglavlje racuna sa brojem" + broj + " ne postoji");
                    throw new Exception("Zaglavlje_racuna sa brojem: " + broj + " ne postoji");
                }

                await _repo.DeleteAsync(zaglavlje);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja zaglavlja:");
                throw;

            }
        }


        public async Task<GetZaglavlje_racunaDTO> FindZByBROJ(int broj)
        {
            try
            {

                var zaglavlje = await _repo.FindZByBROJ(broj);
                if (zaglavlje == null)
                {
                    _logger.LogInformation("Zaglavlje racuna sa brojem" + broj + " ne postoji");
                    throw new Exception("Zaglavlje sa brojem: " + broj + " ne postoji");
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

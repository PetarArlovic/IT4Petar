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
        private readonly IZaglavlje_racunaRepository _zaglavljeRepo;

        public Zaglavlje_racunaService(IGenericRepository<Zaglavlje_racuna> repo, IMapper mapper, ILogger<Zaglavlje_racunaService> logger, IZaglavlje_racunaRepository zaglavljeRepo)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _zaglavljeRepo = zaglavljeRepo;

        }


        public async Task<List<GetZaglavlje_racunaDTO>> GetAllZaglavljaAsync()
        {
            try
            {

                var zaglavlja = await _zaglavljeRepo.GetAllZaglavljaAsync();
                _logger.LogInformation("Zaglavlja su uspješno učitana.");
                return _mapper.Map<List<GetZaglavlje_racunaDTO>>(zaglavlja);

            }

            catch (Exception ex) {

                _logger.LogError(ex, "Greška prilikom učitavanja zaglavlja:");
                throw;

            }
        }


        public async Task<GetZaglavlje_racunaDTO> AddAsync(CreateZaglavlje_racunaDTO dto)
        {

            try
            {

                var newBroj = await GenerateNewBroj();

                var existingzaglavlje = await _zaglavljeRepo.FindZByBROJ(newBroj);

                if (existingzaglavlje != null)
                {
                    _logger.LogInformation("Zaglavlje racuna sa brojem" + dto.broj + " vec postoji");
                    throw new InvalidOperationException("Zaglavlje racuna sa brojem" + dto.broj + " vec postoji");
                }

                dto.broj = newBroj;
                var zaglavlje = _mapper.Map<Zaglavlje_racuna>(dto);

                await _repo.AddAsync(zaglavlje);
                return _mapper.Map<GetZaglavlje_racunaDTO>(zaglavlje);

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

                var zaglavlje = await _zaglavljeRepo.FindZByBROJ(broj);

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

                var zaglavlje = await _zaglavljeRepo.FindZByBROJ(broj);
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

                var zaglavlje = await _zaglavljeRepo.FindZByBROJ(broj);
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

        private async Task<int> GenerateNewBroj()
        {
            var allZaglavlja = await _repo.GetAllAsync();
            var maxSifra = allZaglavlja.Any() ? allZaglavlja.Max(z => z.BROJ) : 0;
            return maxSifra + 1;
        }
    }
}

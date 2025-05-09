using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;


namespace POSApi.Application.Services.Implementations
{
    public class Stavke_racunaService : IStavke_racunaService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Stavke_racuna> _repo;
        private readonly ILogger<Stavke_racunaService> _logger;
        private readonly IStavke_racunaRepository _stavkeRepo;
        private readonly IZaglavlje_racunaRepository _zaglavljeRepo;
        private readonly IProizvodiRepository _proizvodiRepo;

        public Stavke_racunaService(IGenericRepository<Stavke_racuna> repo, IMapper mapper, ILogger<Stavke_racunaService> logger, IStavke_racunaRepository stavkeRepo, IZaglavlje_racunaRepository zaglavljeRepo, IProizvodiRepository proizvodiRepo)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _stavkeRepo = stavkeRepo;
            _zaglavljeRepo = zaglavljeRepo;
            _proizvodiRepo = proizvodiRepo;

        }


        public async Task<List<GetStavke_racunaDTO>> GetAllStavkeAsync()
        {
            try
            {

                var stavke = await _stavkeRepo.GetAllStavkeAsync();
                return _mapper.Map<List<GetStavke_racunaDTO>>(stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja stavki");
                throw;

            }
        }


        public async Task<List<GetStavke_racunaDTO>> GetStavkeByBROJ(int broj)
        {
            try
            {

                var stavke = await _stavkeRepo.GetStavkeByBROJ(broj);

                if (stavke == null)
                {
                    _logger.LogWarning("Stavka sa brojem: " + broj + " ne postoji");
                    throw new Exception("Stavka s brojem " + broj + " ne postoji");
                }

                return _mapper.Map<List<GetStavke_racunaDTO>>(stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja stavki");
                throw;

            }
        }


        public async Task<CreateStavke_racunaDTO> AddAsync(CreateStavke_racunaDTO dto)
        {
            try
            {

                var proizvod = await _proizvodiRepo.FindPBySIFRA(dto.sifra);
                if (proizvod == null)
                {
                    _logger.LogWarning("Proizvod sa sifrom " + dto.sifra + " ne postoji.");
                    throw new Exception("Proizvod sa sifrom " + dto.sifra + " ne postoji.");
                }

                var zaglavlje = await _zaglavljeRepo.FindZByBROJ(dto.broj);
                if (zaglavlje == null)
                {
                    _logger.LogWarning("Zaglavlje sa brojem " + dto.broj + " ne postoji.");
                    throw new Exception("Zaglavlje sa brojem " + dto.broj + " ne postoji.");
                }

                var stavke = _mapper.Map<Stavke_racuna>(dto);
                stavke.ZAGLAVLJE_RACUNAId = zaglavlje.Id;
                stavke.PROIZVODId = proizvod.Id;

                await _repo.AddAsync(stavke);
                return _mapper.Map<CreateStavke_racunaDTO>(stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom dodavanja stavki");
                throw;

            }
        }


        public async Task<bool> UpdateAsync(int broj, UpdateStavke_racunaDTO dto)
        {
            try
            {

                var stavke = await _stavkeRepo.GetStavkeByBROJ(broj);

                if (stavke == null || stavke.Count == 0)
                {
                    _logger.LogWarning("Stavka sa brojem: " + broj + " ne postoji");
                    throw new Exception("Stavka sa brojem: " + broj + " ne postoji");
                }

                foreach (var stavka in stavke)
                {
                    _mapper.Map(dto, stavka);
                    await _repo.UpdateAsync(stavka);
                }

                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom ažuriranja stavki");
                throw;

            }
        }


        public async Task<bool> DeleteAsync(int broj)
        {
            try
            {

                var stavke = await _stavkeRepo.GetStavkeByBROJ(broj);
                if (stavke == null)
                {
                    _logger.LogWarning("Stavka sa brojem: " + broj + " ne postoji");
                    throw new Exception("Stavka sa brojem: " + broj + " ne postoji");
                }

                foreach (var stavka in stavke)
                {
                    await _repo.DeleteAsync(stavka);
                }

                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom brisanja stavki");
                throw;

            }
        }
    }
}

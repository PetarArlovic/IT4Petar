using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;


namespace POSApi.Application.Services.Implementations
{
    public class ProizvodiService : IProizvodiService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Proizvod> _repo;
        private readonly ILogger<ProizvodiService> _logger;
        private readonly IProizvodiRepository _proizvodiRepo;

        public ProizvodiService(IGenericRepository<Proizvod> repo, IMapper mapper, ILogger<ProizvodiService> logger, IProizvodiRepository proizvodiRepo)
        {

            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _proizvodiRepo = proizvodiRepo;

        }


        public async Task<List<GetProizvodDTO>> GetAllAsync()
        {
            try
            {

                var proizvodi = await _repo.GetAllAsync();
                return _mapper.Map<List<GetProizvodDTO>>(proizvodi);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja proizvoda.");
                throw;

            }

        }


        public async Task<GetProizvodDTO> GetByIdAsync(int id)
        {
            try
            {
                var proizvod = await _repo.GetByIdAsync(id);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa id-em: " + id + " ne postoji");
                    throw new Exception("Proizvod sa id-em: " + id + " ne postoji");
                }

                var proizvodDTO = _mapper.Map<GetProizvodDTO>(proizvod);

                proizvodDTO.proizvodSlikaUrl = Path.GetFileName(proizvod.PROIZVODSlikaUrl);

                return proizvodDTO;
            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom učitavanja proizvoda");
                throw;

            }
        }



        public async Task<bool> UpdateStanjeProizvodaAsync(int sifra, int novoStanje)
        {
            try
            {

                var proizvod = await _proizvodiRepo.FindPBySIFRA(sifra);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa id-em: " + sifra + " ne postoji");
                    throw new Exception("Proizvod sa id-em: " + sifra + " ne postoji");
                }

                _logger.LogInformation($"Ažuriranje stanja za proizvod {proizvod.NAZIV}. Stara količina: {proizvod.STANJE}, nova količina: {novoStanje}");

                if (novoStanje < 0)
                {
                    _logger.LogError($"Pokušaj postavljanja negativnog stanja za proizvod {sifra}");
                    return false;
                }

                proizvod.STANJE = novoStanje;
                await _repo.UpdateAsync(proizvod);

                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom ažuriranja proizvoda");
                throw;

            }
        }


        public async Task<GetProizvodDTO> FindPBySIFRA(int sifra)
        {
            try
            {

                var proizvod = await _proizvodiRepo.FindPBySIFRA(sifra);
                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa sifrom: " + sifra + " ne postoji");
                    throw new Exception("Proizvod sa šifrom: " + sifra + " ne postoji");
                }

                return _mapper.Map<GetProizvodDTO>(proizvod);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom pretrage proizvoda");
                throw;

            }
        }

        public async Task<GetProizvodDTO> FindProizvodByNaziv(string naziv)
        {
            try
            {

                var proizvod = await _proizvodiRepo.FindProizvodByNaziv(naziv);
                if (proizvod == null)
                {
                    _logger.LogError("Proizvod s nazivom " + naziv + " ne postoji");
                    throw new Exception("Proizvod s nazivom " + naziv + " ne postoji");
                }

                return _mapper.Map<GetProizvodDTO>(proizvod);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom pretrage proizvoda");
                throw;

            }
        }
    }
}
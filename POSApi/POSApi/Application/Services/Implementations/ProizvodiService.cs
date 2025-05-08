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
                _logger.LogInformation("Proizvodi su uspješno učitani.");
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


        public async Task<CreateProizvodDTO> AddAsync(CreateProizvodDTO dto)
        {
            try
            {

                var existingproizvod = await _proizvodiRepo.FindPBySIFRA(dto.sifra);

                if (existingproizvod != null)
                {
                    _logger.LogInformation("Proizvod sa sifrom" + dto.sifra + " vec postoji");
                    throw new InvalidOperationException("Proizvod sa sifrom" + dto.sifra + " vec postoji");
                }

                var proizvod = _mapper.Map<Proizvod>(dto);
                await _repo.AddAsync(proizvod);
                return _mapper.Map<CreateProizvodDTO>(proizvod);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom dodavanja proizvoda");
                throw;

            }
        }


        public async Task<bool> UpdateAsync(int sifra, UpdateProizvodDTO dto)
        {
            try
            {


                var proizvod = await _proizvodiRepo.FindPBySIFRA(sifra);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa id-em: " + sifra + " ne postoji");
                    throw new Exception("Proizvod sa id-em: " + sifra + " ne postoji");
                }

                _mapper.Map(dto, proizvod);
                await _repo.UpdateAsync(proizvod);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom ažuriranja proizvoda");
                throw;

            }
        }


        public async Task<bool> DeleteAsync(int sifra)
        {
            try
            {

                var proizvod = await _proizvodiRepo.FindPBySIFRA(sifra);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa id-em: " + sifra + " ne postoji");
                    throw new Exception("Proizvod sa id-em: " + sifra + " ne postoji");
                }

                await _repo.DeleteAsync(proizvod);
                return true;

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Greška prilikom brisanja proizvoda");
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
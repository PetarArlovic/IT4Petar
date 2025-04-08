using AutoMapper;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;

namespace POSApi.Application.Services.Implementations
{
    public class ProizvodiService : IProizvodiService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Proizvod> _repo;

        public ProizvodiService(IGenericRepository<Proizvod> repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;

        }


        public async Task<List<GetProizvodDTO>> GetAllAsync()
        {

            var proizvodi = await _repo.GetAllAsync();
            return _mapper.Map<List<GetProizvodDTO>>(proizvodi);

        }


        public async Task<GetProizvodDTO> GetByIdAsync(int id)
        {

            var proizvod = await _repo.GetByIdAsync(id);

            if (proizvod == null)
            {
                throw new Exception("Kupac sa id-em: " + id + " ne postoji");
            }

            return _mapper.Map<GetProizvodDTO>(proizvod);

        }


        public async Task<CreateProizvodDTO> AddAsync(CreateProizvodDTO dto)
        {

            var existingproizvod = await _repo.FindPBySIFRA(dto.SIFRA);

            if (existingproizvod != null)
            {
                throw new InvalidOperationException("Proizvod sa sifrom" + dto.SIFRA + " vec postoji");
            }

            var proizvod = _mapper.Map<Proizvod>(dto);
            await _repo.AddAsync(proizvod);
            return _mapper.Map<CreateProizvodDTO>(proizvod);

        }


        public async Task<bool> UpdateAsync(int sifra, UpdateProizvodDTO dto)
        {

            var proizvod = await _repo.FindPBySIFRA(sifra);

            if (proizvod == null)
            {
                throw new Exception("Proizvod sa id-em: " + sifra + " ne postoji");
            }

            _mapper.Map(dto, proizvod);
            await _repo.UpdateAsync(proizvod);
            return true;

        }


        public async Task<bool> DeleteAsync(int id)
        {

            var proizvod = await _repo.GetByIdAsync(id);
            if (proizvod == null)
            {
                throw new Exception("Kupac sa id-em: " + id + " ne postoji");
            }

            await _repo.DeleteAsync(proizvod);
            return true;

        }

        public async Task<GetProizvodDTO> FindPBySIFRA(int sifra)
        {
            var kupac = await _repo.FindPBySIFRA(sifra);
            if (kupac == null)
            {
                throw new Exception("Kupac sa šifrom: " + sifra + " ne postoji");
            }
            return _mapper.Map<GetProizvodDTO>(kupac);

        }

    }
}


// ||
// {}
//  <>
//  []
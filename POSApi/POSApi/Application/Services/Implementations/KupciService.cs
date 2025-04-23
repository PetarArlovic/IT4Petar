using AutoMapper;
using POSApi.Domain.Interfaces;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;

namespace POSApi.Application.Services.Implementations
{
    public class KupciService : IKupciService
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepository<Kupac> _repo;

        public KupciService(IGenericRepository<Kupac> repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;

        }


        public async Task<List<GetStavke_racuanDTO>> GetAllAsync()
        {

            var kupci = await _repo.GetAllAsync();
            return _mapper.Map<List<GetStavke_racuanDTO>>(kupci);

        }


        public async Task<GetStavke_racuanDTO> GetByIdAsync(int id)
        {

            var kupac = await _repo.GetByIdAsync(id);
            if (kupac == null)
            {
                throw new Exception("Kupac sa id-em: " + id + " ne postoji");
            }

            return _mapper.Map<GetStavke_racuanDTO>(kupac);

        }


        public async Task<CreateKupacDTO> AddAsync(CreateKupacDTO dto)
        {

            var existingKupac = await _repo.FindKBySIFRA(dto.SIFRA);

            if (existingKupac != null)
            {
                throw new InvalidOperationException("Kupac sa sifrom " + dto.SIFRA + " već postoji.");
            }

            var kupac = _mapper.Map<Kupac>(dto);
            await _repo.AddAsync(kupac);
            return _mapper.Map<CreateKupacDTO>(kupac);

        }


        public async Task<bool> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _repo.FindKBySIFRA(sifra);

            if (kupac == null)
            {
                throw new Exception("Kupac sa id-em: " + sifra + " ne postoji");
            }

            _mapper.Map(dto, kupac);
            await _repo.UpdateAsync(kupac);
            return true;

        }


        public async Task<bool> DeleteAsync(int id)
        {

            var kupac = await _repo.GetByIdAsync(id);

            if (kupac == null)
            {
                throw new Exception("Kupac sa id-em: " + id + " ne postoji");
            }

            await _repo.DeleteAsync(kupac);
            return true;

        }

        public async Task<GetStavke_racuanDTO> FindKBySIFRA(int sifra)
        {

            var kupac = await _repo.FindKBySIFRA(sifra);

            if (kupac == null)
            {
                throw new Exception("Kupac sa šifrom: " + sifra + " ne postoji");
            }

            return _mapper.Map<GetStavke_racuanDTO>(kupac);

        }
    }
}

// ||
// {}
//  <>
//  []

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

        public Stavke_racunaService(IGenericRepository<Stavke_racuna> repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;

        }


        public async Task<List<GetStavke_racunaDTO>> GetAllAsync()
        {

            var stavke = await _repo.GetAllAsync();
            return _mapper.Map<List<GetStavke_racunaDTO>>(stavke);

        }


        public async Task<List<GetStavke_racunaDTO>> GetStavkeByBROJ(int broj)
        {

            var stavke = await _repo.GetStavkeByBROJ(broj);

            if (stavke == null)
            {
                throw new Exception("Stavka s brojem " + broj + " ne postoji");
            }

            return _mapper.Map<List<GetStavke_racunaDTO>>(stavke);

        }


        public async Task<GetStavke_racunaDTO> GetByIdAsync(int id)
        {

            var stavke = await _repo.GetByIdAsync(id);

            if (stavke == null)
            {
                throw new Exception("Stavka sa id-jem: " + id + " ne postoji");
            }

            return _mapper.Map<GetStavke_racunaDTO>(stavke);

        }


        public async Task<CreateStavke_racunaDTO> AddAsync(CreateStavke_racunaDTO dto)
        {

            var proizvod = await _repo.FindPBySIFRA(dto.SIFRA);
            if (proizvod == null)
            {
                throw new Exception("Proizvod sa sifrom " + dto.SIFRA + " ne postoji.");
            }

            var zaglavlje = await _repo.FindZByBROJ(dto.BROJ);
            if (zaglavlje == null)
            {
                throw new Exception("Zaglavlje sa brojem " + dto.BROJ + " ne postoji.");
            }

            var stavke = _mapper.Map<Stavke_racuna>(dto);
            stavke.ZAGLAVLJE_RACUNAId = zaglavlje.Id;
            stavke.PROIZVODId = proizvod.Id;

            await _repo.AddAsync(stavke);
            return _mapper.Map<CreateStavke_racunaDTO>(stavke);

        }


        public async Task<bool> UpdateAsync(int id, UpdateStavke_racunaDTO dto)
        {

            var stavke = await _repo.GetByIdAsync(id);
            if (stavke == null)
            {
                throw new Exception("Stavka sa id-jem: " + id + " ne postoji");
            }

            _mapper.Map(dto, stavke);
            await _repo.UpdateAsync(stavke);
            return true;

        }


        public async Task<bool> DeleteAsync(int id)
        {

            var stavke = await _repo.GetByIdAsync(id);
            if (stavke == null)
            {
                throw new Exception("Stavka sa id-jem: " + id + " ne postoji");
            }

            await _repo.DeleteAsync(stavke);
            return true;

        }



    }
}

// ||
// {}
//  <>
//  []

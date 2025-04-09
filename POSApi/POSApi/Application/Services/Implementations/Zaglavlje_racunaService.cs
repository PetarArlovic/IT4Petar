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

        public Zaglavlje_racunaService(IGenericRepository<Zaglavlje_racuna> repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;

        }


        public async Task<List<GetZaglavlje_racunaDTO>> GetAllAsync()
        {

            var zaglavlja = await _repo.GetAllAsync();
            return _mapper.Map<List<GetZaglavlje_racunaDTO>>(zaglavlja);

        }


        public async Task<GetZaglavlje_racunaDTO> GetByIdAsync(int id)
        {

            var zaglavlje = await _repo.GetByIdAsync(id);
            if (zaglavlje == null)
            {
                throw new Exception("Zaglavlje_racuna sa id-jem: " + id + " ne postoji");
            }

            var stavke = await _repo.GetAllAsync();
            return _mapper.Map<GetZaglavlje_racunaDTO>(zaglavlje);

        }


        public async Task<CreateZaglavlje_racunaDTO> AddAsync(CreateZaglavlje_racunaDTO dto)
        {

            var existingzaglavlje = await _repo.FindZByBROJ(dto.BROJ);

            if (existingzaglavlje != null)
            {
                throw new InvalidOperationException("Zaglavlje racuna sa brojem" + dto.BROJ + " vec postoji");
            }

            var zaglavlje = _mapper.Map<Zaglavlje_racuna>(dto);
            await _repo.AddAsync(zaglavlje);
            return _mapper.Map<CreateZaglavlje_racunaDTO>(zaglavlje);

        }


        public async Task<bool> UpdateAsync(int id, UpdateZaglavlje_racunaDTO dto)
        {

            var zaglavlje = await _repo.GetByIdAsync(id);

            if (zaglavlje == null)
            {
                throw new Exception("Zaglavlje_racuna sa id-jem: " + id + " ne postoji");
            }

            _mapper.Map(dto, zaglavlje);
            await _repo.UpdateAsync(zaglavlje);
            return true;

        }


        public async Task<bool> DeleteAsync(int id)
        {

            var zaglavlje = await _repo.GetByIdAsync(id);
            if (zaglavlje == null)
            {
                throw new Exception("Zaglavlje_racuna sa id-jem: " + id + " ne postoji");
            }

            await _repo.DeleteAsync(zaglavlje);
            return true;

        }

        public async Task<GetZaglavlje_racunaDTO> FindZByBROJ(int broj)
        {
            var zaglavlje = await _repo.FindZByBROJ(broj);
            if (zaglavlje == null)
            {
                throw new Exception("Zaglavlje sa šifrom: " + broj + " ne postoji");
            }
            return _mapper.Map<GetZaglavlje_racunaDTO>(zaglavlje);

        }
    }
}

// ||
// {}
//  <>
//  []

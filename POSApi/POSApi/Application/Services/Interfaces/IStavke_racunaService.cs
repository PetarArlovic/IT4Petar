using POSApi.Application.DTO.Stavke_racunaDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IStavke_racunaService
    {

        Task<List<GetStavke_racunaDTO>> GetAllAsync();
        Task<GetStavke_racunaDTO?> GetByIdAsync(int id);
        Task<CreateStavke_racunaDTO> AddAsync(CreateStavke_racunaDTO entity);
        Task<bool> UpdateAsync(int id, UpdateStavke_racunaDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<GetStavke_racunaDTO>> GetStavkeByBROJ(int id);

    }
}

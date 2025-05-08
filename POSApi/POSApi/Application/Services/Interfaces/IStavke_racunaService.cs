using POSApi.Application.DTO.Stavke_racunaDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IStavke_racunaService
    {

        Task<List<GetStavke_racunaDTO>> GetAllStavkeAsync();
        Task<CreateStavke_racunaDTO> AddAsync(CreateStavke_racunaDTO entity);
        Task<bool> UpdateAsync(int broj, UpdateStavke_racunaDTO entity);
        Task<bool> DeleteAsync(int broj);
        Task<List<GetStavke_racunaDTO>> GetStavkeByBROJ(int id);

    }
}

using POSApi.Application.DTO.Zaglavlje_racunaDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IZaglavlje_racunaService
    {

        Task<List<GetZaglavlje_racunaDTO>> GetAllZaglavljaAsync();
        Task<GetZaglavlje_racunaDTO> AddAsync(CreateZaglavlje_racunaDTO entity);
        Task<bool> UpdateAsync(int broj, UpdateZaglavlje_racunaDTO entity);
        Task<bool> DeleteAsync(int broj);
        Task<GetZaglavlje_racunaDTO> FindZByBROJ(int broj);

    }
}

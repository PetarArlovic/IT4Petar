using POSApi.Application.DTO.Zaglavlje_racunaDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IZaglavlje_racunaService
    {

        Task<List<GetZaglavlje_racunaDTO>> GetAllAsync();
        Task<GetZaglavlje_racunaDTO?> GetByIdAsync(int id);
        Task<CreateZaglavlje_racunaDTO> AddAsync(CreateZaglavlje_racunaDTO entity);
        Task<bool> UpdateAsync(int id, UpdateZaglavlje_racunaDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<GetZaglavlje_racunaDTO> FindZByBROJ(int sifra);

    }
}

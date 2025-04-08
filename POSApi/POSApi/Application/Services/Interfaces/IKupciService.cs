using POSApi.Application.DTO.KupacDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IKupacService
    {

        Task<List<GetKupacDTO>> GetAllAsync();
        Task<GetKupacDTO?> GetByIdAsync(int id);
        Task<CreateKupacDTO> AddAsync(CreateKupacDTO entity);
        Task<bool> UpdateAsync(int id, UpdateKupacDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<GetKupacDTO> FindKBySIFRA(int sifra);

    }
}

using POSApi.Application.DTO.KupacDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IKupciService
    {

        Task<List<GetStavke_racuanDTO>> GetAllAsync();
        Task<GetStavke_racuanDTO?> GetByIdAsync(int id);
        Task<CreateKupacDTO> AddAsync(CreateKupacDTO entity);
        Task<bool> UpdateAsync(int id, UpdateKupacDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<GetStavke_racuanDTO> FindKBySIFRA(int sifra);

    }
}

using POSApi.Application.DTO.ProizvodDTO;

namespace POSApi.Application.Services.Interfaces
{
    public interface IProizvodService
    {

        Task<List<GetProizvodDTO>> GetAllAsync();
        Task<GetProizvodDTO?> GetByIdAsync(int id);
        Task<CreateProizvodDTO> AddAsync(CreateProizvodDTO entity);
        Task<bool> UpdateAsync(int id, UpdateProizvodDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<GetProizvodDTO> FindPBySIFRA(int sifra);

    }
}

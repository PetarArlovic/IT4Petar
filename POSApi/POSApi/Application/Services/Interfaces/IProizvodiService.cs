using POSApi.Application.DTO.ProizvodDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IProizvodiService
    {

        Task<List<GetProizvodDTO>> GetAllAsync();
        Task<GetProizvodDTO?> GetByIdAsync(int id);
        Task<CreateProizvodDTO> AddAsync(CreateProizvodDTO entity);
        Task<bool> UpdateAsync(int sifra, UpdateProizvodDTO entity);
        Task<bool> DeleteAsync(int sifra);
        Task<GetProizvodDTO> FindPBySIFRA(int sifra);
        Task<GetProizvodDTO> FindProizvodByNaziv(string naziv);

    }
}

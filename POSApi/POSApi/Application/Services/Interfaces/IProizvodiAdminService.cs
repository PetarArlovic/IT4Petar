using POSApi.Application.DTO.ProizvodDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IProizvodiAdminService
    {

        Task<GetProizvodDTO?> GetByIdAsync(int id);
        Task<CreateProizvodDTO> AddAsync(CreateProizvodDTO entity);
        Task<bool> UpdateAsync(int sifra, UpdateProizvodDTO entity);
        Task<bool> DeleteAsync(int sifra);
        Task<GetProizvodDTO> FindPBySIFRA(int sifra);

    }
}

using POSApi.Application.DTO.ProizvodDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IProizvodiService
    {

        Task<List<GetProizvodDTO>> GetAllAsync();
        Task<GetProizvodDTO?> GetByIdAsync(int id);
        Task<GetProizvodDTO> FindPBySIFRA(int sifra);
        Task<GetProizvodDTO> FindProizvodByNaziv(string naziv);
        Task<bool> UpdateStanjeProizvodaAsync(int sifra, int novoStanje);
    }
}

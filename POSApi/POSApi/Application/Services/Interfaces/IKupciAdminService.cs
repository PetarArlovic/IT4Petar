using POSApi.Application.DTO.KupacDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IKupciAdminService
    {

        Task<GetKupacDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int sifra, UpdateKupacDTO entity);
        Task<bool> DeleteAsync(int sifra);

    }
}

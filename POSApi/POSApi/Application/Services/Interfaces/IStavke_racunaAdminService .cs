using POSApi.Application.DTO.Stavke_racunaDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IStavke_racunaAdminService
    {

        Task<GetStavke_racunaDTO?> GetByIdAsync(int id);

    }
}

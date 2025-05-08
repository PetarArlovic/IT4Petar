using POSApi.Application.DTO.Zaglavlje_racunaDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IZaglavlje_racunaAdminService
    {

        Task<GetZaglavlje_racunaDTO?> GetByIdAsync(int id);

    }
}

﻿using POSApi.Application.DTO.KupacDTO;


namespace POSApi.Application.Services.Interfaces
{
    public interface IKupciService
    {

        Task<List<GetKupacDTO>> GetAllAsync();
        Task<GetKupacDTO> AddAsync(CreateKupacDTO entity);
        Task<bool> UpdateAsync(int sifra, UpdateKupacDTO entity);
        Task<bool> DeleteAsync(int sifra);
        Task<GetKupacDTO> FindKBySIFRA(int sifra);
        Task<GetKupacDTO?> GetByIdAsync(int id);

    }
}

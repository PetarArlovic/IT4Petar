using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KupciAdminController : ControllerBase
    {

        private readonly IKupciAdminService _service;

        public KupciAdminController(IKupciAdminService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets "Kupac" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetKupacDTO>> GetByIdAsync(int id)
        {

            var kupac = await _service.GetByIdAsync(id);
            return Ok(kupac);

        }


        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _service.UpdateAsync(sifra, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        public async Task<ActionResult> DeleteAsync(int sifra)
        {

            var kupac = await _service.DeleteAsync(sifra);
            return NoContent();

        }
    }
}


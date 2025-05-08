using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Application.DTO.ProizvodDTO;
using Microsoft.AspNetCore.Authorization;
using POSApi.Application.Services.Implementations;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProizvodiAdminController : ControllerBase
    {

        private readonly IProizvodiAdminService _service;

        public ProizvodiAdminController(IProizvodiAdminService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets "Proizvod" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProizvodDTO>> GetByIdAsync(int id)
        {

            var proizvod = await _service.GetByIdAsync(id);
            return Ok(proizvod);

        }


        //////////////
        /// <summary>
        /// Adds "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateProizvodDTO dto)
        {

            var proizvod = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(_service.FindPBySIFRA), new { sifra = proizvod.sifra }, proizvod);

        }


        //////////////
        /// <summary>
        /// Updates "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateProizvodDTO dto)
        {

                var proizvod = await _service.UpdateAsync(sifra, dto);
                return NoContent();

        }


        //////////////
        /// <summary>
        /// Deletes "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        public async Task<ActionResult> DeleteAsync(int sifra)
        {

                var proizvod = await _service.DeleteAsync(sifra);
                return NoContent();

        }
    }
}








// ||
// {}
//  <>
//  []


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Application.Services;
using NLog;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KupciController : ControllerBase
    {

        private readonly IKupciService _service;

        public KupciController(IKupciService service)
        {

            _service = service;

        }


        /// <summary>
        /// Gets the list of all "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetKupacDTO>>> GetAllAsync()
        {

            var kupci = await _service.GetAllAsync();
            return Ok(kupci);

        }


        /// <summary>
        /// Gets "Kupac" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetKupacDTO>> GetByIdAsync(int id)
        {

            var kupac = await _service.GetByIdAsync(id);
            return Ok(kupac);

        }


        /// <summary>
        /// Adds "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(CreateKupacDTO dto)
        {

            var kupac = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(FindKBySIFRA), new { sifra = kupac.SIFRA }, kupac);

        }

        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _service.UpdateAsync(sifra, dto);
            return NoContent();

        }

        /// <summary>
        /// Deletes "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var kupac = await _service.DeleteAsync(id);
            return NoContent();

        }

        /// <summary>
        /// Gets "Kupac" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        [Authorize]
        public async Task<ActionResult<GetKupacDTO>> FindKBySIFRA(int sifra)
        {

            var kupac = await _service.FindKBySIFRA(sifra);
            return Ok(kupac);

        }
    }
}


    // ||
    // {}
    //  <>
    //  []


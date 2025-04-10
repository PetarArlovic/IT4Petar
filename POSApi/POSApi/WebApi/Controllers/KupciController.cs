using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Application.Services;
using NLog;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KupciController : ControllerBase
    {

        private readonly ILogger<KupciController> _logger;
        private readonly IKupciService _service;

        public KupciController(IKupciService service, ILogger<KupciController> logger)
        {

            _service = service;
            _logger = logger;

        }


        /// <summary>
        /// Gets the list of all "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetKupacDTO>>> GetAllAsync()
        {
            try
            {

                var kupci = await _service.GetAllAsync();
                _logger.LogInformation("Kupci su uspješno učitani.");
                return Ok(kupci);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja kupaca: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja kupaca: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Kupac" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetKupacDTO>> GetByIdAsync(int id)
        {
            try
            {

                var kupac = await _service.GetByIdAsync(id);

                if (kupac == null)
                {
                    _logger.LogWarning("Kupac sa id-em: " + id + " ne postoji.");
                    return NotFound("Kupac sa id-em: " + id + " ne postoji.");
                }

                return Ok(kupac);
            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja kupca: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja kupca: " + ex.Message);

            }
        }

        /// <summary>
        /// Adds "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(CreateKupacDTO dto)
        {

            try
            {

                var kupac = await _service.AddAsync(dto);

                _logger.LogInformation("Kupac je uspješno dodan.");
                return CreatedAtAction(nameof(FindKBySIFRA), new { sifra = kupac.SIFRA }, kupac);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom dodavanja kupca: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom dodavanja kupca: " + ex.Message);

            }
        }

        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateKupacDTO dto)
        {
            try
            {
                var kupac = await _service.UpdateAsync(id, dto);

                if (!kupac)
                {
                    _logger.LogWarning("Kupac sa id-em: " + id + " ne postoji.");
                    return NotFound("Kupac sa id-em: " + id + " ne postoji.");
                }

                _logger.LogInformation("Kupac je uspješno ažuriran.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom ažuriranja kupca: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom ažuriranja kupca: " + ex.Message);

            }
        }

        /// <summary>
        /// Deletes "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {

                var kupac = await _service.DeleteAsync(id);

                if (!kupac)

                {
                    _logger.LogWarning("Kupac sa id-em: " + id + " ne postoji.");
                    return NotFound("Kupac sa id-em: " + id + " ne postoji.");

                }

                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom brisanja kupca: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom brisanja kupca: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Kupac" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        public async Task<ActionResult<GetKupacDTO>> FindKBySIFRA(int sifra)
        {

            var kupac = await _service.FindKBySIFRA(sifra);
            if (kupac == null)
            {
                return NotFound($"Kupac sa šifrom {sifra} nije pronađen.");
            }

            return Ok(kupac);

        }
    }
}


    // ||
    // {}
    //  <>
    //  []


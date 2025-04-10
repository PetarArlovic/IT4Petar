using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Application.Services;
using NLog;
using System.Linq.Expressions;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.ProizvodDTO;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {

        private readonly ILogger<ProizvodiController> _logger;
        private readonly IProizvodiService _service;

        public ProizvodiController(IProizvodiService service, ILogger<ProizvodiController> logger)
        {

            _service = service;
            _logger = logger;

        }

        /// <summary>
        /// Gets the list of all "Proizvodi"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetProizvodDTO>>> GetAllAsync()
        {
            try
            {

                var proizvodi = await _service.GetAllAsync();

                _logger.LogInformation("Proizvodi su uspješno učitani.");
                return Ok(proizvodi);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja proizvoda: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Proizvod" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetProizvodDTO>> GetByIdAsync(int id)
        {
            try
            {

                var proizvod = await _service.GetByIdAsync(id);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa id-em: " + id + " ne postoji");
                    return NotFound("Proizvod sa id-em: " + id + " ne postoji");
                }

                return Ok(proizvod);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja proizvoda: " + ex.Message);

            }
        }

        /// <summary>
        /// Adds "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(CreateProizvodDTO dto)
        {

            try
            {

                var proizvod = await _service.AddAsync(dto);

                _logger.LogInformation("Proizvod je uspješno dodan.");
                return CreatedAtAction(nameof(FindPBySIFRA), new { sifra = proizvod.SIFRA }, proizvod);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom dodavanja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom dodavanja proizvoda: " + ex.Message);

            }
        }

        /// <summary>
        /// Updates "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateProizvodDTO dto)
        {

            try
            {
                var proizvod = await _service.UpdateAsync(id, dto);

                if (!proizvod)
                {
                    _logger.LogError("Greška prilikom ažuriranja proizvoda");
                    return NotFound("Greška prilikom ažuriranja proizvoda");
                }

                _logger.LogInformation("Proizvod je uspješno ažuriran.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom ažuriranja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom ažuriranja proizvoda: " + ex.Message);

            }
        }

        /// <summary>
        /// Deletes "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {

                var proizvod = await _service.DeleteAsync(id);

                if (!proizvod)
                {
                    _logger.LogError("Greška prilikom brisanja proizvoda");
                    return NotFound("Greška prilikom brisanja proizvoda");
                }

                _logger.LogInformation("Proizvod je uspješno obrisan.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom brisanja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom brisanja proizvoda: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Proizvod" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        [Authorize]
        public async Task<ActionResult<GetKupacDTO>> FindPBySIFRA(int sifra)
        {
            try
            {

                var proizvod = await _service.FindPBySIFRA(sifra);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa sifrom: " + sifra + " ne postoji");
                    return NotFound($"Proizvod sa šifrom {sifra} nije pronađen.");
                }

                return Ok(proizvod);
            }

            catch(Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja proizvoda: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja proizvoda: " + ex.Message);

            }
        }
    }
}








// ||
// {}
//  <>
//  []


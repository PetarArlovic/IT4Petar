using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Application.Services.Interfaces;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Zaglavlje_racunaController : ControllerBase
    {

        private readonly ILogger<Zaglavlje_racunaController> _logger;
        private readonly IZaglavlje_racunaService _service;

        public Zaglavlje_racunaController(IZaglavlje_racunaService service, ILogger<Zaglavlje_racunaController> logger)
        {

            _service = service;
            _logger = logger;

        }

        /// <summary>
        /// Gets the list of all "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetZaglavlje_racunaDTO>>> GetAllAsync()
        {

            try
            {

                var zaglavlja = await _service.GetAllAsync();
                _logger.LogInformation("Zaglavlja su uspješno učitana.");
                return Ok(zaglavlja);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja zaglavlja: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Zaglavlje racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetZaglavlje_racunaDTO>> GetByIdAsync(int id)
        {

            try
            {

                var zaglavlje = await _service.GetByIdAsync(id);

                if (zaglavlje == null)
                {
                    return NotFound("Zaglavlje sa id-jem: " + id + " ne postoji");
                }

                return Ok(zaglavlje);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja zaglavlja: " + ex.Message);

            }
        }

        /// <summary>
        /// Adds "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(CreateZaglavlje_racunaDTO dto)
        {

            try
            {

                var zaglavlje = await _service.AddAsync(dto);
                _logger.LogInformation("Zaglavlje je uspješno dodano.");
                return CreatedAtAction(nameof(FindZByBROJ), new { broj = zaglavlje.BROJ }, zaglavlje);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom dodavanja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom dodavanja zaglavlja: " + ex.Message);

            }
        }

        /// <summary>
        /// Updates "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateZaglavlje_racunaDTO dto)
        {

            try
            {

                var zaglavlje = await _service.UpdateAsync(id, dto);
                if (!zaglavlje)
                {
                    return NotFound("Zaglavlje sa id-jem: " + id + " ne postoji");
                }

                _logger.LogInformation("Zaglavlje je uspješno ažurirano.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom ažuriranja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom ažuriranja zaglavlja: " + ex.Message);

            }
        }

        /// <summary>
        /// Deletes "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {

                var zaglavlje = await _service.DeleteAsync(id);
                if (!zaglavlje)
                {
                    return NotFound("Zaglavlje sa id-jem: " + id + " ne postoji");
                }

                _logger.LogInformation("Zaglavlje je uspješno obrisano.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom brisanja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom brisanja zaglavlja: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Zaglavlje racuna" by "BROJ"
        /// </summary>
        /// <returns></returns>
        [HttpGet("broj/{broj}")]
        [Authorize]
        public async Task<ActionResult<GetZaglavlje_racunaDTO>> FindZByBROJ(int broj)
        {

            try
            {

                var proizvod = await _service.FindZByBROJ(broj);

                if (proizvod == null)
                {
                    _logger.LogError("Proizvod sa brojem: " + broj + " ne postoji");
                    return NotFound($"Proizvod sa šifrom {broj} nije pronađen.");
                }

                return Ok(proizvod);
            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja zaglavlja: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja zaglavlja: " + ex.Message);

            }
        }
    }
}


// ||
// {}
//  <>
//  []


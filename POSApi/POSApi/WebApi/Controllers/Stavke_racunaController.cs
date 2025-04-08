using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.Services.Interfaces;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stavke_racunaController : ControllerBase
    {

        private readonly ILogger<Stavke_racunaController> _logger;
        private readonly IStavke_racunaService _service;

        public Stavke_racunaController(IStavke_racunaService service, ILogger<Stavke_racunaController> logger)
        {

            _service = service;
            _logger = logger;

        }

        /// <summary>
        /// Gets the list of all "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetKupacDTO>>> GetAllAsync()
        {

            try
            {

                var stavke = await _service.GetAllAsync();
                _logger.LogInformation("Stavke su uspješno učitane.");
                return Ok(stavke);
            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja stavki: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja stavki: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Stavke racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStavke_racunaDTO>> GetByIdAsync(int id)
        {
            try
            {

                var stavke = await _service.GetByIdAsync(id);

                if (stavke == null)
                {
                    return NotFound("Stavka sa id-jem: " + id + " Ne postoji");
                }

                return Ok(stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom učitavanja stavke: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja stavke: " + ex.Message);

            }
        }

        /// <summary>
        /// Gets "Stavke racuna" by its "broj"
        /// </summary>
        /// <returns></returns>
        [HttpGet("BROJ {broj}")]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetStavkeByBROJ(int broj)
        {
            try
            {
                var stavke = await _service.GetStavkeByBROJ(broj);

                if (stavke == null)
                {
                    return NotFound("Stavka sa id-jem: " + broj + " Ne postoji");
                }

                return Ok(stavke);

            }

            catch (Exception ex)
            {
                _logger.LogError("Greška prilikom učitavanja stavke: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom učitavanja stavke: " + ex.Message);
            }
        }


        /// <summary>
        /// Adds "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateStavke_racunaDTO dto)
        {

            try
            {

                var stavke = await _service.AddAsync(dto);

                _logger.LogInformation("Stavka je uspješno dodana.");
                return CreatedAtAction(nameof(GetStavkeByBROJ), new { broj = stavke.BROJ }, stavke);

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom dodavanja stavke: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom dodavanja stavke: " + ex.Message);

            }
        }

        /// <summary>
        /// Updates "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateStavke_racunaDTO dto)
        {
            try
            {

                var stavka = await _service.UpdateAsync(id, dto);

                if (!stavka)
                {
                    return NotFound("Stavka sa id-jem: " + id + " ne postoji");
                }

                _logger.LogInformation("Stavka je uspješno ažurirana.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom ažuriranja stavke: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom ažuriranja stavke: " + ex.Message);

            }
        }

        /// <summary>
        /// Deletes "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {
                var proizvod = await _service.DeleteAsync(id);

                if (!proizvod)
                {
                    return NotFound("Stavka sa id-jem: " + id + " ne postoji");
                }

                _logger.LogInformation("Stavka je uspješno obrisana.");
                return NoContent();

            }

            catch (Exception ex)
            {

                _logger.LogError("Greška prilikom brisanja stavke: " + ex.Message + " InnerException: " + ex.InnerException?.Message);
                return BadRequest("Greška prilikom brisanja stavke: " + ex.Message);

            }
        }
    }
}



// ||
// {}
//  <>
//  []

using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetAllAsync()
        {

            var stavke = await _service.GetAllStavkeAsync();
            return Ok(stavke);

        }


        /// <summary>
        /// Gets "Stavke racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetStavke_racunaDTO>> GetByIdAsync(int id)
        {

            var stavke = await _service.GetByIdAsync(id);
            return Ok(stavke);

        }


        /// <summary>
        /// Gets "Stavke racuna" by its "broj"
        /// </summary>
        /// <returns></returns>
        [HttpGet("BROJ/{broj}")]
        [Authorize]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetStavkeByBROJ(int broj)
        {

                var stavke = await _service.GetStavkeByBROJ(broj);
                return Ok(stavke);

        }


        /// <summary>
        /// Adds "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAsync(CreateStavke_racunaDTO dto)
        {

            var stavke = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetStavkeByBROJ), new { broj = stavke.broj }, stavke);

        }


        /// <summary>
        /// Updates "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{broj}")]
        [Authorize]
        public async Task<ActionResult> UpdateAsync(int broj, UpdateStavke_racunaDTO dto)
        {

            var stavka = await _service.UpdateAsync(broj, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{broj}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int broj)
        {

                var proizvod = await _service.DeleteAsync(broj);
                return NoContent();

        }
    }
}



// ||
// {}
//  <>
//  []

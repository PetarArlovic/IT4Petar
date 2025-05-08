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

            var zaglavlja = await _service.GetAllZaglavljaAsync();
            return Ok(zaglavlja);

        }


        /// <summary>
        /// Gets "Zaglavlje racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetZaglavlje_racunaDTO>> GetByIdAsync(int id)
        {

            var zaglavlje = await _service.GetByIdAsync(id);
            return Ok(zaglavlje);

        }


        /// <summary>
        /// Adds "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAsync(CreateZaglavlje_racunaDTO dto)
        {

            var zaglavlje = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(FindZByBROJ), new { broj = zaglavlje.broj }, zaglavlje);

        }


        /// <summary>
        /// Updates "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{broj}")]
        [Authorize]
        public async Task<ActionResult> UpdateAsync(int broj, UpdateZaglavlje_racunaDTO dto)
        {

            var zaglavlje = await _service.UpdateAsync(broj, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{broj}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int broj)
        {

                var zaglavlje = await _service.DeleteAsync(broj);
                return NoContent();

        }


        /// <summary>
        /// Gets "Zaglavlje racuna" by "BROJ"
        /// </summary>
        /// <returns></returns>
        [HttpGet("broj/{broj}")]
        [Authorize]
        public async Task<ActionResult<GetZaglavlje_racunaDTO>> FindZByBROJ(int broj)
        {

                var proizvod = await _service.FindZByBROJ(broj);
                return Ok(proizvod);

        }
    }
}


// ||
// {}
//  <>
//  []


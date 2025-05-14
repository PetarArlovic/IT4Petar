using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.Services.Interfaces;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Stavke_racunaController : ControllerBase
    {

        private readonly IStavke_racunaService _service;

        public Stavke_racunaController(IStavke_racunaService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets the list of all "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetAllAsync()
        {

            var stavke = await _service.GetAllStavkeAsync();
            return Ok(stavke);

        }


        //////////////
        /// <summary>
        /// Gets "Stavke racuna" by its "broj"
        /// </summary>
        /// <returns></returns>
        [HttpGet("BROJ/{broj}")]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetStavkeByBROJ(int broj)
        {

            var stavke = await _service.GetStavkeByBROJ(broj);
            return Ok(stavke);

        }


        //////////////
        /// <summary>
        /// Adds "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateStavke_racunaDTO dto)
        {

            var stavke = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetStavkeByBROJ), new { broj = stavke.broj }, stavke);

        }


        //////////////
        /// <summary>
        /// Updates "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{broj}")]
        public async Task<ActionResult> UpdateAsync(int broj, UpdateStavke_racunaDTO dto)
        {

            var stavka = await _service.UpdateAsync(broj, dto);
            return NoContent();

        }


        //////////////
        /// <summary>
        /// Deletes "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{broj}")]
        public async Task<ActionResult> DeleteAsync(int broj)
        {

            var proizvod = await _service.DeleteAsync(broj);
            return NoContent();

        }
    }
}

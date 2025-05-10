using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Application.DTO.ProizvodDTO;
using Microsoft.AspNetCore.Authorization;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProizvodiController : ControllerBase
    {
        private readonly IProizvodiService _proizvodiService;

        public ProizvodiController(IProizvodiService proizvodiService)
        {

            _proizvodiService = proizvodiService;

        }

        //////////////
        /// <summary>
        /// Gets the list of all "Proizvodi"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetProizvodDTO>>> GetAllAsync()
        {

            var proizvodi = await _proizvodiService.GetAllAsync();
            return Ok(proizvodi);

        }


        //////////////
        /// <summary>
        /// Updates "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateStanje/{sifra}")]
        public async Task<ActionResult> UpdateAsync(int sifra, [FromBody] int novoStanje)
        {

            await _proizvodiService.UpdateStanjeProizvodaAsync(sifra, novoStanje);
            return NoContent();

        }


        //////////////
        /// <summary>
        /// Gets "Proizvod" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        public async Task<ActionResult<GetProizvodDTO>> FindPBySIFRA(int sifra)
        {

            var proizvod = await _proizvodiService.FindPBySIFRA(sifra);
            return Ok(proizvod);

        }


        //////////////
        /// <summary>
        /// Gets "Proizvod" by "Naziv"
        /// </summary>
        /// <returns></returns>
        [HttpGet("naziv/{naziv}")]
        public async Task<ActionResult<GetProizvodDTO>> FindProizvodByNaziv(string naziv)
        {

            var proizvod = await _proizvodiService.FindProizvodByNaziv(naziv);
            return Ok(proizvod);

        }
    }
}


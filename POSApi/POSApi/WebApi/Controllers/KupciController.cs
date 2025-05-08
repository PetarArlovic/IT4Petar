using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KupciController : ControllerBase
    {

        private readonly IKupciService _kupciService;

        public KupciController(IKupciService service)
        {

            _kupciService = service;

        }


        //////////////
        /// <summary>
        /// Gets the list of all "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetKupacDTO>>> GetAllAsync()
        {

            var kupci = await _kupciService.GetAllAsync();
            return Ok(kupci);

        }


        //////////////
        /// <summary>
        /// Adds "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateKupacDTO dto)
        {

            var kupac = await _kupciService.AddAsync(dto);
            return Ok(kupac);

        }


        //////////////
        /// <summary>
        /// Gets "Kupac" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        public async Task<ActionResult<GetKupacDTO>> FindKBySIFRA(int sifra)
        {

            var kupac = await _kupciService.FindKBySIFRA(sifra);
            return Ok(kupac);

        }


        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _kupciService.UpdateAsync(sifra, dto);
            return NoContent();

        }


        //////////////
        /// <summary>
        /// Gets "Kupac" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetKupacDTO>> GetByIdAsync(int id)
        {

            var kupac = await _kupciService.GetByIdAsync(id);
            return Ok(kupac);

        }
    }
}


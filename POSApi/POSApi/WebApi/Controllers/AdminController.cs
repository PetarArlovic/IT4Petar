using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Application.Services.Interfaces;
/*
namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IKupciService _kupciService;
        private readonly IProizvodiService _proizvodiService;
        private readonly IStavke_racunaService _stavkeService;
        private readonly IZaglavlje_racunaService _zaglavljeService;

        public AdminController(IKupciService kupciService, IProizvodiService proizvodiService, IStavke_racunaService stavkeService, IZaglavlje_racunaService zaglavljeService)
        {

            _kupciService = kupciService;
            _proizvodiService = proizvodiService;
            _stavkeService = stavkeService;
            _zaglavljeService = zaglavljeService;

        }


        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateKupacAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _kupciService.UpdateAsync(sifra, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteKupacAsync(int sifra)
        {

            var kupac = await _kupciService.DeleteAsync(sifra);
            return NoContent();

        }






        /// <summary>
        /// Adds "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddProizvodAsync(CreateProizvodDTO dto)
        {

            var proizvod = await _proizvodiService.AddAsync(dto);
            return CreatedAtAction(nameof(FindPBySIFRA), new { sifra = proizvod.sifra }, proizvod);

        }


        /// <summary>
        /// Updates "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProizvodAsync(int sifra, UpdateProizvodDTO dto)
        {

            var proizvod = await _proizvodiService.UpdateAsync(sifra, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProizvodAsync(int sifra)
        {

            var proizvod = await _proizvodiService.DeleteAsync(sifra);
            return NoContent();

        }


        /// <summary>
        /// Gets "Proizvod" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        [Authorize]
        public async Task<ActionResult<GetProizvodDTO>> FindPBySIFRA(int sifra)
        {

            var proizvod = await _proizvodiService.FindPBySIFRA(sifra);
            return Ok(proizvod);

        }







        /// <summary>
        /// Adds "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddStavkeAsync(CreateStavke_racunaDTO dto)
        {

            var stavke = await _stavkeService.AddAsync(dto);
            return CreatedAtAction(nameof(GetStavkeByBROJ), new { broj = stavke.broj }, stavke);

        }


        /// <summary>
        /// Updates "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{broj}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateStavkeAsync(int broj, UpdateStavke_racunaDTO dto)
        {

            var stavka = await _stavkeService.UpdateAsync(broj, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{broj}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteStavkeAsync(int broj)
        {

            var proizvod = await _stavkeService.DeleteAsync(broj);
            return NoContent();

        }


        /// <summary>
        /// Gets "Stavke racuna" by its "broj"
        /// </summary>
        /// <returns></returns>
        [HttpGet("BROJ/{broj}")]
        [Authorize]
        public async Task<ActionResult<List<GetStavke_racunaDTO>>> GetStavkeByBROJ(int broj)
        {

            var stavke = await _stavkeService.GetStavkeByBROJ(broj);
            return Ok(stavke);

        }








        /// <summary>
        /// Adds "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddZaglavljeAsync(CreateZaglavlje_racunaDTO dto)
        {

            var zaglavlje = await _zaglavljeService.AddAsync(dto);
            return CreatedAtAction(nameof(FindZByBROJ), new { broj = zaglavlje.broj }, zaglavlje);

        }


        /// <summary>
        /// Updates "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{broj}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateZaglavljeAsync(int broj, UpdateZaglavlje_racunaDTO dto)
        {

            var zaglavlje = await _zaglavljeService.UpdateAsync(broj, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{broj}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteZaglavljeAsync(int broj)
        {

            var zaglavlje = await _zaglavljeService.DeleteAsync(broj);
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

            var proizvod = await _zaglavljeService.FindZByBROJ(broj);
            return Ok(proizvod);

        }
    }
}
*/
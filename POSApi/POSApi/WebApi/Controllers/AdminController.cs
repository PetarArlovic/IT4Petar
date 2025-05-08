using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
/*
namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        /// <summary>
        /// Updates "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateKupacDTO dto)
        {

            var kupac = await _service.UpdateAsync(sifra, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Kupac"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int sifra)
        {

            var kupac = await _service.DeleteAsync(sifra);
            return NoContent();

        }






        /// <summary>
        /// Adds "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(CreateProizvodDTO dto)
        {

            var proizvod = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(FindPBySIFRA), new { sifra = proizvod.sifra }, proizvod);

        }


        /// <summary>
        /// Updates "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpPut("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateProizvodDTO dto)
        {

            var proizvod = await _service.UpdateAsync(sifra, dto);
            return NoContent();

        }


        /// <summary>
        /// Deletes "Proizvod"
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sifra}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int sifra)
        {

            var proizvod = await _service.DeleteAsync(sifra);
            return NoContent();

        }







        /// <summary>
        /// Adds "Stavke racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int broj)
        {

            var proizvod = await _service.DeleteAsync(broj);
            return NoContent();

        }








        /// <summary>
        /// Adds "Zaglavlje racuna"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(int broj)
        {

            var zaglavlje = await _service.DeleteAsync(broj);
            return NoContent();

        }
    }
}
*/
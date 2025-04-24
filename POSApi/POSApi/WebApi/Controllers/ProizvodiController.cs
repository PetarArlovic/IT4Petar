using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Application.Services;
using NLog;
using System.Linq.Expressions;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {

        private readonly IProizvodiService _service;

        public ProizvodiController(IProizvodiService service)
        {

            _service = service;

        }


        /// <summary>
        /// Gets the list of all "Proizvodi"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetProizvodDTO>>> GetAllAsync()
        {

            var proizvodi = await _service.GetAllAsync();
            return Ok(proizvodi);

        }


        /// <summary>
        /// Gets "Proizvod" by his Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetProizvodDTO>> GetByIdAsync(int id)
        {

            var proizvod = await _service.GetByIdAsync(id);
            return Ok(proizvod);

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
        /// Gets "Proizvod" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        [Authorize]
        public async Task<ActionResult<GetProizvodDTO>> FindPBySIFRA(int sifra)
        {

                var proizvod = await _service.FindPBySIFRA(sifra);
                return Ok(proizvod);

        }
    }
}








// ||
// {}
//  <>
//  []


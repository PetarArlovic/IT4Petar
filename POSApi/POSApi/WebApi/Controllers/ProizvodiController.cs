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
        [HttpPut("{sifra}")]
        public async Task<ActionResult> UpdateAsync(int sifra, UpdateProizvodDTO dto)
        {

            var proizvod = await _proizvodiService.UpdateAsync(sifra, dto);
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








// ||
// {}
//  <>
//  []


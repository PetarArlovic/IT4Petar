using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.Services.Interfaces;
using POSApi.Domain.Models;
using POSApi.Application.Services;
using NLog;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using POSApi.Application.DTO.KupacDTO;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KupciController : ControllerBase
    {

        private readonly IKupciService _service;

        public KupciController(IKupciService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets the list of all "Kupci"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetKupacDTO>>> GetAllAsync()
        {

            var kupci = await _service.GetAllAsync();
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

            var kupac = await _service.AddAsync(dto);
            return CreatedAtRoute(
            routeName: "GetKupacById",
            routeValues: new { id = kupac.Id },     
            value: kupac);


        }


        //////////////
        /// <summary>
        /// Gets "Kupac" by "Sifra"
        /// </summary>
        /// <returns></returns>
        [HttpGet("sifra/{sifra}")]
        public async Task<ActionResult<GetKupacDTO>> FindKBySIFRA(int sifra)
        {

            var kupac = await _service.FindKBySIFRA(sifra);
            return Ok(kupac);

        }
    }
}


    // ||
    // {}
    //  <>
    //  []


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.Services.Interfaces;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class Stavke_racunaAdminController : ControllerBase
    {

        private readonly IStavke_racunaAdminService _service;

        public Stavke_racunaAdminController(IStavke_racunaAdminService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets "Stavke racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStavke_racunaDTO>> GetByIdAsync(int id)
        {

            var stavke = await _service.GetByIdAsync(id);
            return Ok(stavke);

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Application.Services.Interfaces;


namespace POSApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class Zaglavlje_racunaAdminController : ControllerBase
    {

        private readonly IZaglavlje_racunaAdminService _service;

        public Zaglavlje_racunaAdminController(IZaglavlje_racunaAdminService service)
        {

            _service = service;

        }


        //////////////
        /// <summary>
        /// Gets "Zaglavlje racuna" by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetZaglavlje_racunaDTO>> GetByIdAsync(int id)
        {

            var zaglavlje = await _service.GetByIdAsync(id);
            return Ok(zaglavlje);

        }
    }
}


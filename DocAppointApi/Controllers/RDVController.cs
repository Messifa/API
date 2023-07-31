using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocAppointApi.Models;
using DocAppointApi.Services;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RDVController : ControllerBase
    {
        private readonly RDVService _rdvService;

        public RDVController(RDVService rdvService)
        {
            _rdvService = rdvService;
        }

        [HttpPost("appointments")]
        public async Task<IActionResult> CreateAppointment([FromBody] RDVM rdv)
        {
            try
            {
                var createdAppointment = await _rdvService.CreateAppointment(rdv);
                return Ok("rendez-vous enregistrer");
            }
            catch (Exception ex)
            {
                return BadRequest("le rendez-vous n'est pas valider desole");
            }
        }
    }
}
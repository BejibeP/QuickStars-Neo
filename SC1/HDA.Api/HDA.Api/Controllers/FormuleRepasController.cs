using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HDA.Business.Interfaces;
using HDA.Business.Models.FormuleRepas;
using HDA.Domain.Identity;
using System.Data;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.AdminName)]
    public class FormuleRepasController : ControllerBase
    {
        private readonly IFormuleRepasService _service;
        public FormuleRepasController(IFormuleRepasService service)
        {
            _service=service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFormulesRepas()
        {
            var sortie = await _service.GetAllFormulesRepas();
            return Ok(sortie);
        }

        [HttpGet("Dispo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAvailableRepas()
        {
            var sortie = await _service.GetAllAvailableRepas();
            return Ok(sortie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormuleRepas([FromBody] CreateFormuleRepasDTO createFormuleRepas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = await _service.CreateFormuleRepas(createFormuleRepas);
            if (dto == null) { return BadRequest(); }
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormuleRepas(int id)
        {
            bool b = await _service.DeleteFormuleRepas(id);
            if (!b) { return NotFound(); }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormuleRepas(int id, [FromBody] FormuleRepasDTO formuleRepas)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var response = await _service.UpdateFormuleRepas(formuleRepas, id);
            if (response == false) return NotFound();
            return NoContent();
        }
    }
}

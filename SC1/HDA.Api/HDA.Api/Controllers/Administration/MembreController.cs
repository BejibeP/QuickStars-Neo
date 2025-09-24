using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Business.Services;
using HDA.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.AdminName)]
    public class MembreController : ControllerBase
    {
        private readonly IMembreService _membreService;
        private readonly IMembreZeroService _membreZeroService;
        public MembreController(IMembreService membreService, IMembreZeroService membreZeroService)
        {
            _membreService = membreService;
            _membreZeroService = membreZeroService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllMembers()
        {
            var list = await _membreService.GetAllMembres();
            return Ok(list);
        }

        [HttpGet("Zero")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllMembresZero()
        {
            var list = await _membreZeroService.GetAllMembres();
            return Ok(list);
        }

        [HttpGet("Zero/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMemberZero(long id)
        {
            var dto = await _membreZeroService.GetMembreById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMember(long id)
        {
            var dto = await _membreService.GetMembreById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddJoueur(MembreDto dto)
        {
            var result = await _membreService.AddMembre(dto);
            if(result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateJoueur(MembreDto dto)
        {
            var result = await _membreService.UpdateMembre(dto);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteJoueur(long id)
        {
            var b = await _membreService.DeleteMembre(id);
            if (!b) return NotFound();
            return NoContent();
        }

    }
}
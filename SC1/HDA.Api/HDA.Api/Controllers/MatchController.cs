using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Business.Services;
using HDA.Domain.Identity;
using System.Data;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.AdminName)]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dto.Createur = HttpContext.User.Identity?.Name;
            var match = await _matchService.CreateMatch(dto);
            if (match == null) return BadRequest();

            var uri = $"https://localhost:7212/api/Match/{match.Id}";
            return Created(uri, match);
        }

        [HttpPut("OuvreMatch")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> OpenMatch([FromBody] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var match = await _matchService.OpenMatch(id);
            if (match == null) return BadRequest();
            return Ok(match);
        }

        [HttpGet("MatchOuvert")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetOpenMatch()
        {
            var matchOuvert = await _matchService.GetOrAssignOpenMatch();
            return Ok(matchOuvert);
        }

        [HttpGet("MatchsFermes")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllClosedMatch()
        {
            var listMatchs = await _matchService.GetAllClosedMatch();
            return Ok(listMatchs);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllMatch()
        {
            var listMatchs = await _matchService.GetAllMatch();
            return Ok(listMatchs);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            bool b = await _matchService.DeleteMatch(id);
            if (!b) return NotFound();
            return NoContent();
        }
    }
}

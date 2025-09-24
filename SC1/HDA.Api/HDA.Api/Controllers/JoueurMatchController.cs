using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Identity;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurMatchController : ControllerBase
    {
        private readonly IJoueurMatchService _joueurMatchService;
        private readonly IMatchService _matchService;
        public JoueurMatchController(IJoueurMatchService joueurMatchService, IMatchService matchService)
        {
            _joueurMatchService = joueurMatchService;
            _matchService = matchService;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetJoueursFromMatch()
        {
            var match = await _matchService.GetOrAssignOpenMatch();
            var list = await _joueurMatchService.GetJoueursFromMatch(match.Id);
            return Ok(list);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddJoueurToMatch([FromBody] AddJoueurDTO add)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = await _matchService.GetOrAssignOpenMatch();
            var dto = new AddJoueurToMatchDTO() { Mail = add.Mail, IdSoccer =  match.Id, FormuleRepas = add.FormuleRepas };
            var joueur = await _joueurMatchService.AddJoueurToMatch(dto);
            return joueur switch
            {
                0 => Ok(dto),
                1 => BadRequest("Le joueur saisi n'existe pas"),
                2 => NoContent(),
                _ => BadRequest("Une erreur est survenue"),
            };
        }

        [HttpDelete("{idJoueur}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = IdentityRoles.AdminName)]
        public async Task<IActionResult> RemoveJoueurToMatch(int idJoueur)
        {
            bool b = await _joueurMatchService.RemoveJoueurToMatch(idJoueur);

            if (!b) return NotFound();
            return NoContent();
        }

        [HttpPut("changeRepas")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = IdentityRoles.AdminName)]
        public async Task<IActionResult> UpdateRepasOfJoueurMatch(UpdateRepasOfJoueurMatchDTO joueurMatchDTO)
        {
            var b = await _joueurMatchService.UpdateRepasOfJoueurMatch(joueurMatchDTO);
            if (!b) return BadRequest();
            return NoContent();
        }
    }
}

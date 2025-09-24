using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Identity;
using System.Data;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.AdminName)]
    public class JoueurController : ControllerBase
    {
        private readonly IJoueurService _joueurService;
        public JoueurController(IJoueurService joueurService)
        {
            _joueurService = joueurService;
        }

        [HttpGet("All")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllJoueurs()
        {
            var list = await _joueurService.GetAllJoueurs();
            return Ok(list);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetJoueurParMail(string mail)
        {
            var id = await _joueurService.GetJoueurByMail(mail);
            if (id == 0) return NotFound(mail);
            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddJoueur(AddJoueurToBDDDTO dto)
        {
            var sortie = await _joueurService.AddJoueur(dto);
            return sortie switch
            {
                0 => Ok(dto),
                1 => NoContent(),
                -1 => BadRequest(),
                _ => BadRequest()
            };
        }

        [HttpPost("List")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddListJoueurs(AddListJoueursDTO joueurs)
        {
            var sortie = await _joueurService.AddListJoueurs(joueurs.List);

            if (!sortie) return BadRequest();

            return Ok(joueurs);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateJoueur(UpdateJoueurDTO dto)
        {
            var sortie = await _joueurService.UpdateJoueur(dto);

            return sortie switch
            {
                0 => NoContent(),
                1 => BadRequest("Adresse email déjà utilisée."),
                -1 => NotFound(),
                _ => BadRequest()
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteJoueur(int id)
        {
            var b = await _joueurService.DeleteJoueur(id);
            if (!b) return NotFound();
            return NoContent();
        }


    }
}

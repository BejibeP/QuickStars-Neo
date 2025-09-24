using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HDA.Business.Interfaces;
using HDA.Business.Models.Authentication;
using HDA.Domain.Identity;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var tokenString = await _authenticationService.Login(dto);
            if (tokenString == null) return BadRequest("Le mot de passe et/ou le login est incorrect");
            return Ok(new { Token = tokenString });
        }

        [HttpGet("connected")]
        [Authorize(Roles = IdentityRoles.AdminName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetConnectedUser()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (userName == null) return NoContent();
            return Ok(userName);
        }

        [HttpGet("connectedMail")]
        [Authorize(Roles = IdentityRoles.AdminName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetConnectedUserMail()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (userName == null) return NoContent();

            var mail = await _authenticationService.GetConnectedUserMail(userName);
            return Ok(mail);
        }

        [HttpPost("register")]
        [Authorize(Roles = IdentityRoles.AdminName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var register = await _authenticationService.Register(dto);

            switch (register)
            {
                case 0: return Ok(dto);
                case -1: return BadRequest("Ce nom d'utilisateur est déjà utilisé");
                case -2: return BadRequest("Not created");
                default: return BadRequest();
            }
        }

        [HttpPost("resetPassword")]
        [Authorize(Roles = IdentityRoles.AdminName)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            //regarde si le nom renseigné est le même que celui de l'utilisateur connecté
            if (dto.Name != (string?)User.Identity?.Name) return Forbid();

            var reset = await _authenticationService.ResetPassword(dto);
            if (!reset) return BadRequest();
            return NoContent();
        }

        [HttpPut("modifyProfil")]
        [Authorize(Roles = IdentityRoles.AdminName)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ModifyProfil(ModifyProfilDTO dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var name = (string?)User.Identity?.Name;
            
            var b = await _authenticationService.ModifyProfil(dto, name);

            if (!b) return BadRequest();
            return NoContent();
        }

    }
}

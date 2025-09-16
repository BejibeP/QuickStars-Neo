// Example .NET 8 minimal controller + service for external redirect SSO
// This is a simplified example; adapt to your real infra (IdentityServer / ASP.NET Identity).

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExternalSso.Controllers
{
    [ApiController]
    [Route("api/external")]
    public class ExternalController : ControllerBase
    {
        private readonly IOtkService _otkService;

        public ExternalController(IOtkService otkService)
        {
            _otkService = otkService;
        }

        // POST /api/external/create-redirect
        // Body: { app: "foo" }
        // Requires authentication (cookie or bearer)
        [HttpPost("create-redirect")]
        public IActionResult CreateRedirect([FromBody] CreateRedirectRequest req)
        {
            // Ensure user is authenticated
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();

            var userId = User.FindFirst("sub")?.Value ?? User.Identity.Name;
            var otk = _otkService.CreateOneTimeToken(userId!, req.App, TimeSpan.FromMinutes(1));

            // Example: return a URL containing the short-lived token
            var redirectUrl = $"https://external.app.example/sso?otk={otk.Token}";
            return Ok(new { url = redirectUrl });
        }

        // POST /api/external/validate-otk
        // Called by external app server-to-server to validate and obtain user info
        [HttpPost("/external/validate-otk")]
        public IActionResult ValidateOtk([FromBody] ValidateOtkRequest req)
        {
            var info = _otkService.ValidateAndConsume(req.Otk);
            if (info == null) return BadRequest(new { error = "invalid or used otk" });

            return Ok(new { userId = info.UserId, claims = info.Claims });
        }
    }

    public record CreateRedirectRequest(string App);
    public record ValidateOtkRequest(string Otk);
}

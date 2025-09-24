using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HDA.Business.Identity;
using HDA.Business.Interfaces;
using HDA.Business.Models.Authentication;
using HDA.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HDA.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentitySettings _identitySettings;
        public AuthenticationService(UserManager<IdentityUser> userManager, IOptions<IdentitySettings> identitySettings)
        {
            _userManager = userManager;
            _identitySettings = identitySettings.Value;
        }
        public async Task<string?> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Name);
            if (user == null) return null;
            var log = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!log) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateJwtToken(user, roles);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <returns>-1 si nom existe déjà, -2 si la création n'a pas aboutit, 0 sinon</returns>
        public async Task<int> Register(RegisterDTO dto)
        {
            //si un utilisateur existe déjà avec ce nom, ne pas permettre l'inscription
            if (_userManager.FindByNameAsync(dto.Name) == null) return -1;

            var userInfo = new IdentityUser
            {
                UserName = dto.Name,
                Email = dto.Email
            };

            var userResult = await _userManager.CreateAsync(userInfo, dto.Password);
            if (!userResult.Succeeded) return -2;

            //donne le rôle d'admin à l'utilisateur
            await _userManager.AddToRoleAsync(userInfo, IdentityRoles.AdminName);
            return 0;
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Name);
            if (user == null) return false;
            var reset = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!reset.Succeeded) return false;
            return true;
        }

        public async Task<string?> GetConnectedUserMail(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return "";
            string email = user.Email;

            return email;
        }
        public async Task<bool> ModifyProfil(ModifyProfilDTO dto, string? name)
        {
            if (dto == null) return false;

            var user = await _userManager.FindByNameAsync(name); 
            if (user == null) return false; 

            if (!String.IsNullOrEmpty(dto.UserName)) user.UserName = dto.UserName;
            if (!String.IsNullOrEmpty(dto.Mail)) user.Email = dto.Mail;

            await _userManager.UpdateAsync(user);

            return true;
        }
        private JwtSecurityToken GenerateJwtToken(IdentityUser user, IEnumerable<string> userRoles)
        {
            var authClaims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.UserName),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            DateTime expires = DateTime.Now.AddMinutes(_identitySettings.ExpirationInMinutes);

            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identitySettings.Secret));
            var credentials = new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _identitySettings.Issuer,
                audience: _identitySettings.Audience,
                claims: authClaims,
                expires: expires,
                signingCredentials: credentials
                );

            return tokenOptions;
        }
    }
}

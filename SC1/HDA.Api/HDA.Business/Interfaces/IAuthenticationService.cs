using Microsoft.AspNetCore.Identity;
using HDA.Business.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> Login(LoginDTO dto);
        /// <returns>-1 si nom existe déjà, -2 si la création n'a pas abouti, 0 sinon</returns>
        Task<int> Register(RegisterDTO dto);
        Task<bool> ResetPassword(ResetPasswordDTO dto);
        Task<string?> GetConnectedUserMail(string userName);

        Task<bool> ModifyProfil(ModifyProfilDTO dto, string? name);
    }
}

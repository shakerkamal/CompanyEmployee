using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAutheticationService
    {
        Task<IdentityResult> RegisterUser(UserRegistrationDto userRegistration);
        Task<bool> AuthenticateUser(UserAuthenticationDto userAuthentication);
        Task<TokenDto> CreateToken(bool Expired);
        Task<TokenDto> RefreshToken(TokenDto token);
    }
}
